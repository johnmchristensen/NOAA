using NOAA.GHCND.Parser;
using System;

namespace NOAA.GHCND.EXE
{
    class Program
    { 
        protected static StationFileParser _fileParser = new StationFileParser();

        static void Main(string[] args)
        {
            //var station = _fileParser.LoadStation(@"C:\Users\netbard\Dropbox\WeatherStationData\20201211\ghcnd_all\ghcnd_all", "USW00014768");
            var allStations = _fileParser.LoadAllStations(@"C:\Users\netbard\Dropbox\WeatherStationData\20201211\ghcnd_all\ghcnd_all");
            Console.Out.WriteLine("Loaded!");
            Console.In.Read();
        }
    }
}
