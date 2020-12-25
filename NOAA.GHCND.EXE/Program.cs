using NOAA.GHCND.Parser;
using System;

namespace NOAA.GHCND.EXE
{
    class Program
    { 
        protected static StationFileParser _fileParser = new StationFileParser();

        static void Main(string[] args)
        {
            var startDate = DateTime.Now;
            var allStations = _fileParser.LoadAllStationsParallel(@"C:\Users\netbard\Dropbox\WeatherStationData\20201211\ghcnd_all\ghcnd_all");
            Console.Out.WriteLine($"Loaded {allStations.Count} in {(DateTime.Now - startDate)}");
            Console.In.Read();
        }
    }
}
