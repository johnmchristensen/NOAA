using NOAA.GHCND.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NOAA.GHCND.Data
{
    public class StationData : IStationData
    {
        protected readonly DayData<short> _shortData = new DayData<short>(short.MinValue);
        protected readonly DayData<int> _intData = new DayData<int>(int.MinValue);

        public StationData(string stationId)
        {
            Id = stationId;
        }

        public string Id { get; }

        public void AddDay(string dataType, DateTime day, int data)
        {
            if (short.MinValue < data && data < short.MaxValue)
            {
                _shortData.AddDay(dataType, day, (short)data);
            }
            else
            {
                _intData.AddDay(dataType, day, data);
            }
        }

        public bool ContainsDataForType(string dataType)
        {
            return this._shortData.ContainsDataForType(dataType) || this._intData.ContainsDataForType(dataType);
        }

        public DateTime GetMinimumDateWithData(string dataType)
        {
            if (false == this.ContainsDataForType(dataType))
            {
                return DateTime.MaxValue;
            }

            var collection = (this._shortData.ContainsDataForType(dataType) ? (IDayData) this._shortData : this._intData);
            return collection.GetMinimumDateWithData(dataType);
        }

        public IEnumerable<int> GetData(string dataType, DateTime startDate, DateTime endDate)
        {
            for (var i = startDate; i <= endDate; i = i.AddDays(1))
            {
                yield return GetData(dataType, i);
            }
        }

        public string[] GetAvailableData()
        {
            return this._intData.GetAvailableData().Concat(this._shortData.GetAvailableData()).ToArray();
        }

        public int GetData(string dataType, DateTime date)
        {
            if (_shortData.ContainsDataForDate(dataType, date))
            {
                return _shortData.GetData(dataType, date);
            }

            if (_intData.ContainsDataForDate(dataType, date))
            {
                return _intData.GetData(dataType, date);
            }

            return int.MinValue;
        }
    }
}
