using NOAA.GHCND.Search.Enums;
using System;

namespace NOAA.GHCND.Data
{
    public class SearchQueryParameter<T>
    {
        public T Parameter { get; set; }
        public QueryOperator Operator { get; set; }
        public string Value { get; set; }
    }
}