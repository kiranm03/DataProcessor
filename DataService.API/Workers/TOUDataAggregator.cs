using System;
using System.Collections.Generic;
using System.Linq;
using DataService.Extensions;
using DataService.Model;
using Microsoft.Extensions.Logging;

namespace DataService.Workers
{
    public class TOUDataAggregator : IDataAggregator<TOUData>
    {
        public IEnumerable<EnergyResponse> Aggregate(IEnumerable<TOUData> lPData)
        {
            return lPData
                .GroupBy(d => new { d.MeterCode, d.DataType, d.Date.Date })
                .Select(g => new EnergyResponse
                {
                    Date = g.Key.Date.ToShortDateString(),
                    MeterCode = g.Key.MeterCode,
                    DataType = g.Key.DataType,
                    Minimum = g.Min(m => m.Energy),
                    Maximum = g.Max(m => m.Energy),
                    Median = g.Median(m => m.Energy)
                });
        }
    }
}
