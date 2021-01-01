using NOAA.GHCND.Parser;
using System;
using System.Linq;

namespace NOAA.GHCND.EXE
{
    class Program
    { 
        protected static StationFileParser _fileParser = new StationFileParser();

        static void Main(string[] args)
        {
            var startDate = DateTime.Now;

            var allStationInfos = _fileParser.LoadStationInfo(@"C:\Users\netbard\Dropbox\WeatherStationData").ToArray();
            Console.Out.WriteLine($"Loaded {allStationInfos.Count()} in {DateTime.Now - startDate}");
            Console.In.Read();

            startDate = DateTime.Now;
            var allStations = _fileParser.LoadAllStationDataParallel(@"C:\Users\netbard\Dropbox\WeatherStationData\20201211\ghcnd_all\ghcnd_all");
            Console.Out.WriteLine($"Loaded {allStations.Count} in {(DateTime.Now - startDate)}");
            Console.In.Read();
        }
    }
}
