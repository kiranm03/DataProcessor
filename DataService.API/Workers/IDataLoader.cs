using System;
using System.Collections.Generic;

namespace DataService.Workers
{
    public interface IDataLoader<T>
    {
        IEnumerable<T> Load();
    }
}
