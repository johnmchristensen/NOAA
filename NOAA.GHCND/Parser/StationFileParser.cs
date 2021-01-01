using NOAA.GHCND.DataStructures;
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
        protected StationInfoParser _stationInfoParser = new StationInfoParser();

        public StationData LoadStationData(string directory, string stationId)
        {
            Console.Out.WriteLine($"Loading {stationId}");
            var station = new StationData(stationId);

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

        public IReadOnlyDictionary<string, StationData> LoadAllStationData(string directory)
        {
            var stations = new Dictionary<string, StationData>();
            foreach (var file in Directory.GetFiles(directory))
            {
                var stationId = Path.GetFileNameWithoutExtension(file);
                var station = this.LoadStationData(directory, stationId);
                stations.Add(stationId, station);
            }
            return stations;
        }

        public IEnumerable<StationInfo> LoadStationInfo(string directory)
        {
            using (var fileStream = new StreamReader(directory + "/ghcnd-stations.txt"))
            {
                string line;
                while ((line = fileStream.ReadLine()) != null)
                {
                    if (this._stationInfoParser.TryParseStationInfoLine(line, out var stationInfo))
                    {
                        yield return stationInfo;
                    }
                    else
                    {
                        Console.Out.WriteLine(line);
                    }
                }
            }
        }

        public IReadOnlyDictionary<string, StationData> LoadAllStationDataParallel(string directory)
        {
            return Directory.GetFiles(directory)
                .AsParallel()
                .Select(x =>
                {
                    var stationId = Path.GetFileNameWithoutExtension(x);
                    return new {
                        stationId, 
                        station = this.LoadStationData(directory, stationId)
                        };
                })
                .ToDictionary((x) => x.stationId, (x) => x.station);
        }
    }
}
