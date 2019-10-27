using System;
namespace DataService.Model
{
    public class BaseData
    {
        public long MeterCode { get; set; }
        public DateTime Date { get; set; }
        public string DataType { get; set; }
        public long SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public string Units { get; set; }
    }
}
