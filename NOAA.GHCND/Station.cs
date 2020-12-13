using System;
using System.Collections.Generic;

namespace NOAA.GHCND
{
    public class Station
    {
        protected readonly Dictionary<DateTime, StationDay> _data = new Dictionary<DateTime, StationDay>();

        public Station(string stationId)
        {
            this.Id = stationId;
        }

        public string Id { get; }

        public StationDay GetOrAddDay(DateTime date)
        {
            if (false == this._data.ContainsKey(date))
            {
                this._data.Add(date, new StationDay(date));
            }

            return this._data[date];
        }
    }
}
