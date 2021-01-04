using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrawlerBase.DataAccess
{
    public class CrawlerContext: DbContext
    {
        private readonly string _connectionString;

        public CrawlerContext(string connectionString = null)
        {
            this._connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<RawData> RawData { get; set; }
        public DbSet<ContentRegister> ContentRegister { get; set; }

        public void RegisterDownloadContent(string parentUrl, string url)
        {
            this.ContentRegister.Add(
                    new ContentRegister
                    {
                        DateDefined = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow,
                        State = ContentRegisterStateEnum.Registered,
                        ParentUrl = RemoveSpecialCharacters(parentUrl),
                        Url = RemoveSpecialCharacters(url)
                    }
                );
            this.SaveChanges();
        }

        public void SetDownloadedContent(string sourceUrl)
        {
            var url = RemoveSpecialCharacters(sourceUrl);
            var item = this.ContentRegister.Single(cr => cr.Url == url);
            item.State = ContentRegisterStateEnum.Downloaded;
            item.DateModified = DateTime.UtcNow;
            this.SaveChanges();
            SetParents(item.ParentUrl, new List<int> {
                    (int)ContentRegisterStateEnum.Finished,
                    (int)ContentRegisterStateEnum.Downloaded
                }, ContentRegisterStateEnum.Downloaded);
        }

        public void FinishDownloadedContent(string sourceUrl)
        {
            var url = RemoveSpecialCharacters(sourceUrl);
            var item = this.ContentRegister.Single(cr => cr.Url == url);
            item.State = ContentRegisterStateEnum.Finished;
            item.DateModified = DateTime.UtcNow;
            this.SaveChanges();
            SetParents(item.ParentUrl, new List<int> { (int)ContentRegisterStateEnum.Finished}, ContentRegisterStateEnum.Finished);
        }

        private void SetParents(string parentUrl, List<int> acceptableStateIds, ContentRegisterStateEnum newState)
        {
            if (parentUrl == string.Empty)
            {
                return;
            }

            var url = RemoveSpecialCharacters(parentUrl);
            var siblings = this.ContentRegister.Where(cr => cr.ParentUrl == url);
            if (siblings.All(s => acceptableStateIds.Contains(s.StateId)))
            {
                var parent = this.ContentRegister.Single(cr => cr.Url == url);
                parent.State = newState;
                parent.DateModified = DateTime.UtcNow;
                this.SaveChanges();
                SetParents(parent.ParentUrl, acceptableStateIds, newState);
            }
        }

        private string RemoveSpecialCharacters(string baseValue)
        {
            var bytes = Encoding.Default.GetBytes(baseValue);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
