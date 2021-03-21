using System;
using System.Collections.Generic;

namespace NOAA.GHCND.Extensions
{
    public static class IDictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            return (dictionary.ContainsKey(key)) ? dictionary[key] : defaultValue;
        }
    }
}
