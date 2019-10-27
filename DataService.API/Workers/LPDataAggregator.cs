using System;
using System.Collections.Generic;
using System.Linq;
using DataService.Model;
using DataService.Extensions;
using Microsoft.Extensions.Logging;

namespace DataService.Workers
{
    public class LPDataAggregator : IDataAggregator<LPData>
    {
        public IEnumerable<EnergyResponse> Aggregate(IEnumerable<LPData> lPData)
        {
            return lPData
                .GroupBy(d => new { d.MeterCode, d.DataType, d.Date.Date })
                .Select(g => new EnergyResponse
                {
                    Date = g.Key.Date.ToShortDateString(),
                    MeterCode = g.Key.MeterCode,
                    DataType = g.Key.DataType,
                    Minimum = g.Min(m => m.DataValue),
                    Maximum = g.Max(m => m.DataValue),
                    Median = g.Median(m => m.DataValue)
                });
        }
    }
}
