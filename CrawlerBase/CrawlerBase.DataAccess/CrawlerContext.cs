using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    
        public DbSet<RawData> RawData { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
