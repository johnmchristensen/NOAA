using NOAA.GHCND.Data;
using NOAA.GHCND.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using NOAA.GHCND.Exceptions;

namespace NOAA.GHCND
{
    public class HistoricClimateDatabase
    {
        protected readonly IStationSourceRule _stationSourceRule;
        protected readonly IDictionary<string, StationInfo> _stationInfoMap;

        public StationInfo[] StationInfos => this._stationInfoMap.Values.ToArray();

        public HistoricClimateDatabase(IConfiguration configuration, IStationSourceRule stationSourceRule)
        {
            _stationSourceRule = stationSourceRule;
            this._stationInfoMap = stationSourceRule.LoadStationInfo().ToDictionary(x => x.Id.FullId, x => x);
        }

        public IStationData GetStationData(string stationId)
        {
            if (false == this._stationInfoMap.ContainsKey(stationId))
            {
                throw new NotFoundException(stationId);
            }

            return this._stationSourceRule.LoadStationData(stationId);
        }
    }
}
