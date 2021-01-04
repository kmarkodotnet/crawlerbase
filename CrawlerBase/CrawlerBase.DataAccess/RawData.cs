using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrawlerBase.DataAccess
{
    public class RawData : IEntity<int>
    {
        public int Id { get; set; }
        public int RawDataTypeId { get; set; }

        [NotMapped]
        public RawDataTypeEnum RawDataType
        {
            get
            {
                return (RawDataTypeEnum)RawDataTypeId;
            }
            set
            {
                RawDataTypeId = (int)value;
            }
        }
        public string SourceUrl { get; set; }
        public string Data { get; set; }
        public DateTime DateDefined { get; set; }
    }
}
