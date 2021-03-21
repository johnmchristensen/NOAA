using System;
using System.Collections.Generic;

namespace NOAA.GHCND.Data
{
    public class DataPointDTO : IDataPointDTO
    {
        public DateTime Date { get; set; }
        public IReadOnlyDictionary<string, float?> Data { get; set; }
    }
}
