using NOAA.GHCND.Data;
using NOAA.GHCND.Parser;
using System;
using System.Collections.Generic;
using System.IO;

namespace NOAA.GHCND.Sources
{
    public class StationFileSourceRule : IStationSourceRule
    {
        public const string FILE_EXTENSIONS = ".dly";

        protected IConfiguration _configuration;
        protected StationParserRule _stationParser = new StationParserRule();
        protected StationInfoParserRule _stationInfoParser = new StationInfoParserRule();


        public StationFileSourceRule(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public IStationData LoadStationData(string stationId)
        {
            Console.Out.WriteLine($"Loading {stationId}");
            var station = new StationData(stationId);

            var path = $"{this._configuration.FileSystemReader_BaseDirectory}/{this._configuration.FileSystemReader_DataDirectory}/{stationId}{FILE_EXTENSIONS}";
            using (var fileStream = new StreamReader(path))
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

        public IEnumerable<StationInfo> LoadStationInfo()
        {
            using (var fileStream = new StreamReader(this._configuration.FileSystemReader_BaseDirectory + "/ghcnd-stations.txt"))
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
    }
}
