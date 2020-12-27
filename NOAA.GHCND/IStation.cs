using System;
using System.Collections.Generic;

namespace NOAA.GHCND
{
    public interface IStation
    {
        string Id { get; }

        void AddDay(string dataType, DateTime day, int data);
        IEnumerable<int> GetData(string dataType, DateTime startDate, DateTime endDate);
    }
}