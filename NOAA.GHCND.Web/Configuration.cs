using System;
using NOAA.GHCND.Data;

namespace NOAA.GHCND.Web
{
    public class Configuration : IConfiguration
    {
        public string FileSystemReader_BaseDirectory => "/Users/johnchristensen/dropbox/WeatherStationData";
        public string FileSystemReader_DataDirectory => "/20201211/ghcnd_all/ghcnd_all";

        public string FTP_Server => "ftp.ncdc.noaa.gov";
        public string FTP_BaseDirectory => "pub/data/ghcn/daily";
    }
}
