using System;
using System.Collections.Generic;
using NOAA.GHCND.Data;

namespace NOAA.GHCND.Rules
{
    public interface IStationDatasetRule
    {
        IEnumerable<DataPointDTO> GetDataSet(IStationData stationData, string dataType, DateTime start, DateTime end);

        IEnumerable<DataPointDTO> GetDataSet(IStationData stationData, string[] dataTypes, DateTime start,
            DateTime end);
    }
}