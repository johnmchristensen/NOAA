using NOAA.GHCND.Data;
using NOAA.GHCND.Parser;
using NOAA.GHCND.Search.Enums;
using NOAA.GHCND.Search.Rules;
using System;
using System.Linq;

namespace NOAA.GHCND
{
    public class HistoricClimateDatabase
    {
        protected readonly StationFileParser _stationFileParser = new StationFileParser();
        protected readonly IStationInfoSearchQueryRule _stationInfoSearchQueryRule;

        public StationInfo[] StationInfos { get; }

        public HistoricClimateDatabase(IStationInfoSearchQueryRule stationInfoSearchQueryRule)
        {
            this.StationInfos = this._stationFileParser.LoadStationInfo(@"C:\Users\Shadow\Dropbox\WeatherStationData").ToArray();
            this._stationInfoSearchQueryRule = stationInfoSearchQueryRule;
        }

        public StationInfo[] FindStationInfos(SearchQueryParameter<StationInfoSearchFields>[] parameters)
        {
            return this._stationInfoSearchQueryRule.GetFilteredStationInfoQueryable(this.StationInfos.AsQueryable(), parameters).ToArray();
        }
    }
}