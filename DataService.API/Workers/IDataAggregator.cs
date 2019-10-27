using System;
using System.Collections.Generic;
using DataService.Model;

namespace DataService.Workers
{
    public interface IDataAggregator<T>
    {
        IEnumerable<EnergyResponse> Aggregate(IEnumerable<T> t);
    }
}
