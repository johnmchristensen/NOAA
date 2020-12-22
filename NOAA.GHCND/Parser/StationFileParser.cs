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
                fileStream.Close();
            }

            return station;
        }

        public IReadOnlyDictionary<string, Station> LoadAllStations(string directory)
        {
            var stations = new Dictionary<string, Station>();
            foreach (var file in Directory.GetFiles(directory))
            {
                var stationId = Path.GetFileNameWithoutExtension(file);
                var station = this.LoadStation(directory, stationId);
                stations.Add(stationId, station);
            }
            return stations;
        }
    }
}
