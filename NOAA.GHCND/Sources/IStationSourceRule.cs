using System.Collections.Generic;
using NOAA.GHCND.Data;

namespace NOAA.GHCND.Sources 
{
    public interface IStationSourceRule
    {
        IStationData LoadStationData(string stationId);
        IEnumerable<StationInfo> LoadStationInfo();
    }
}