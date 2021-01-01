using System;
using System.Collections.Generic;

namespace NOAA.GHCND.DataStructures
{
    public class StationData 
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

        public IEnumerable<int> GetData(string dataType, DateTime startDate, DateTime endDate)
        {
            for (var i = startDate; i <= endDate; i = i.AddDays(1))
            {
                yield return GetData(dataType, i);
            }
        }

        protected int GetData(string dataType, DateTime date)
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
