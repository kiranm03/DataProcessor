using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataService.Extensions
{
    public static class LINQExtension
    {
        public static decimal Median(this IEnumerable<decimal> source)
        {
            if (!source.Any())
            {
                throw new InvalidOperationException("Cannot compute median for an empty set.");
            }

            var sortedList = source.OrderBy(s => s);

            int itemIndex = sortedList.Count() / 2;

            return sortedList.Count() % 2 == 0
                ? (sortedList.ElementAt(itemIndex) + sortedList.ElementAt(itemIndex - 1)) / 2
                : sortedList.ElementAt(itemIndex);
        }

        public static decimal Median<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            return source
                .Select(selector)
                .Median();
        }
    }
}
