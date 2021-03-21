using System;
using System.Collections.Generic;

namespace NOAA.GHCND.Data
{
    public interface IStationData
    {
        void AddDay(string dataType, DateTime day, int data);
        bool ContainsDataForType(string dataType);
        DateTime GetMinimumDateWithData(string dataType);
        IEnumerable<int> GetData(string dataType, DateTime startDate, DateTime endDate);
        int GetData(string dataType, DateTime date);
        string[] GetAvailableData();
    }
}