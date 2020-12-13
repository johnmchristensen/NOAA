using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NOAA.GHCND.Parser
{
    public class StationFileParser
    {
        public const string FILE_EXTENSIONS = ".dly";

        protected StationParser _stationParser = new StationParser();

        public Station LoadStation(string directory, string stationId)
        {
            Console.Out.WriteLine($"Loading {stationId}");
            var station = new Station(stationId);

            using (var fileStream = new StreamReader(directory + "/" + stationId + ".dly"))
            {
                string line;
                while ((line = fileStream.ReadLine()) != null)
                {
                    this._stationParser.ParseStationLine(line, station);
                }
            }

            return station;
        }

        public IReadOnlyDictionary<string, Station> LoadAllStations(string directory)
        {
            return new DirectoryInfo(directory).GetFiles()
                .Select(x => new { Key = x.Name.Replace(x.Extension, ""), Station = this.LoadStation(directory, x.Name.Replace(x.Extension, "")) })
                .ToDictionary(x => x.Key, x => x.Station);
        }
    }
}
