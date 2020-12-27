using System;
using System.Collections.Generic;

namespace NOAA.GHCND.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> loopAction)
        {
            foreach (var x in collection)
            {
                loopAction(x);
            }
        }
    }
}
