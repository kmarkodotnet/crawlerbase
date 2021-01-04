using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrawlerBase.DataAccess
{
    public class ContentRegister : IEntity<int>
    {
        public int Id { get; set; }
        public string ParentUrl { get; set; }
        public string Url { get; set; }
        public DateTime DateDefined { get; set; }
        public DateTime DateModified { get; set; }
        public int StateId { get; set; }

        [NotMapped]
        public ContentRegisterStateEnum State {
            get
            {
                return (ContentRegisterStateEnum)StateId;
            }
            set
            {
                StateId = (int)value;
            }
        }
    }
    public enum ContentRegisterStateEnum
    {
        Registered = 0,
        Downloaded = 1,
        Finished = 2
    }
}
