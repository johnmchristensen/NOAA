using System;
using System.Collections.Generic;

namespace NOAA.GHCND
{
    public class StationDay
    {
        protected readonly Dictionary<string, int> _data = new Dictionary<string, int>();

        public StationDay(DateTime date)
        {
            this.Date = date;
        }

        public DateTime Date { get; }

        public IReadOnlyDictionary<string, int> Data => this._data;

        public void AddData(string dataType, int value)
        {
            this._data.Add(dataType, value);
        }
    }
}
