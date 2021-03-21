using System;

namespace NOAA.GHCND.Collections
{
    public interface IDayData
    {
        bool ContainsDataForType(string dataType);
        DateTime GetMinimumDateWithData(string dataType);
        bool ContainsDataForDate(string dataType, DateTime date);
        string[] GetAvailableData();
    }

    public interface IDayData<T> : IDayData where T : IEquatable<T>
    {
        void AddDay(string dataType, DateTime day, T data);
        T GetData(string dataType, DateTime date);
        
    }
}