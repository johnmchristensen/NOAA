using NOAA.GHCND.Data;
using NOAA.GHCND.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NOAA.GHCND
{
    public class HistoricClimateDatabase
    {
        protected StationFileParser _stationFileParser = new StationFileParser();

        public StationInfo[] StationInfos { get; }

        public HistoricClimateDatabase()
        {
            this.StationInfos = this._stationFileParser.LoadStationInfo(@"C:\Users\netbard\Dropbox\WeatherStationData").ToArray();
        }
    }
}
