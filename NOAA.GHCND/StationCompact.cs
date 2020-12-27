using NOAA.GHCND.DataStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace NOAA.GHCND
{
    public class StationCompact : IStation
    {
        protected readonly DayData<short> _shortData = new DayData<short>(short.MinValue);
        protected readonly DayData<int> _intData = new DayData<int>(int.MinValue);

        public StationCompact(string stationId)
        {
            this.Id = stationId;
        }

        public string Id { get; }

        public void AddDay(string dataType, DateTime day, int data)
        {
            if (short.MinValue < data && data < short.MaxValue)
            {
                this._shortData.AddDay(dataType, day, (short)data);
            }
            else
            {
                this._intData.AddDay(dataType, day, data);
            }
        }

        public IEnumerable<int> GetData(string dataType, DateTime startDate, DateTime endDate)
        {
        }

        protected int GetData(string dataType, DateTime date)
        {
            if (this._shortData.ContainsDataForDate(dataType, date))
            {
                return this._shortData.GetData(dataType, date, date);
            }


        }
    }
}
