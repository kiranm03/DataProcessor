using System;
namespace DataService.Model
{
    public class TOUData : BaseData
    {
        public string Quality { get; set; }
        public string Stream { get; set; }
        public decimal Energy { get; set; }
    }
}
