using System;
using DataService.Model;
using TinyCsvParser.Mapping;

namespace DataService.Mappers
{
    public class CsvLPDataMapping : CsvMapping<LPData>
    {
        public CsvLPDataMapping()
        {
            MapProperty(0, x => x.MeterCode);
            MapProperty(1, x => x.SerialNumber);
            MapProperty(2, x => x.PlantCode);
            MapProperty(3, x => x.Date);
            MapProperty(4, x => x.DataType);
            MapProperty(5, x => x.DataValue);
            MapProperty(6, x => x.Units);
            MapProperty(7, x => x.Status);
        }
    }
}
