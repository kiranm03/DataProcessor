using System;
namespace DataService.Model
{
    public class EnergyResponse
    {
        public long MeterCode { get; set; }
        public string Date { get; set; }
        public string DataType { get; set; }
        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }
        public decimal Median { get; set; }
    }
}
