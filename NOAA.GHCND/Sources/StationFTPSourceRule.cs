using System.Collections.Generic;
using NOAA.GHCND.Data;
using System.Net;
using System;
using System.IO;
using NOAA.GHCND.Parser;

namespace NOAA.GHCND.Sources
{
    public class StationFTPSourceRule : IStationSourceRule
    {
        protected readonly IConfiguration _configuration;
        protected readonly StationInfoParserRule _stationInfoParserRule;

        public StationFTPSourceRule(IConfiguration configuration, StationInfoParserRule stationInfoParserRule) 
        {
            this._configuration = configuration;
            this._stationInfoParserRule = stationInfoParserRule;
        }

        public IStationData LoadStationData(string stationId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<StationInfo> LoadStationInfo()
        {
            var request = (FtpWebRequest)WebRequest.Create(this.GetUri("ghcnd-stations.txt"));
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            var response = (FtpWebResponse) request.GetResponse();
            using (var stream = new StreamReader(response.GetResponseStream())) 
            {
                if (this._stationInfoParserRule.TryParseStationInfoLine(stream.ReadLine(), out var stationInfo)) 
                {
                    yield return stationInfo;
                }
            }
        }

        protected Uri GetUri(string path) 
        {
            return new Uri($"ftp://{this._configuration.FTP_Server}/{this._configuration.FTP_BaseDirectory}/{path}");
        }
    }
}