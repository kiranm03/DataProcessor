using System;
using DataService.Model;
using TinyCsvParser.Mapping;

namespace DataService.Mappers
{
    public class CsvTOUDataMapping : CsvMapping<TOUData>
    {
        public CsvTOUDataMapping()
        {
            MapProperty(0, x => x.MeterCode);
            MapProperty(1, x => x.SerialNumber);
            MapProperty(2, x => x.PlantCode);
            MapProperty(3, x => x.Date);
            MapProperty(4, x => x.Quality);
            MapProperty(5, x => x.Stream);
            MapProperty(6, x => x.DataType);
            MapProperty(7, x => x.Energy);
            MapProperty(8, x => x.Units);
        }
    }
}
