using FluentAssertions;
using NOAA.GHCND.Data;
using NOAA.GHCND.Parser;
using NUnit.Framework;
using System;

namespace NOAA.GHCND.Tests.Parser
{
    [TestFixture]
    public class StationInfoParserTest
    {
        [Test]
        public void CanParseStationInfo()
        {
            var parser = new StationInfoParserRule();
            StationInfo stationInfo;

            var stationLine = "AG000060680 143.1167 -177.6767 1362.0    TAMANRASSET                    GSN     60680";
            parser.TryParseStationInfoLine(stationLine, out stationInfo).Should().BeTrue();
            stationInfo.Id.CountryCode.Should().Be("AG");
            stationInfo.Id.StationType.Should().Be(StationTypes.Unspecified);
            stationInfo.Id.Id.Should().Be("00060680");
            stationInfo.Latitude.Should().Be(143.1167m);
            stationInfo.Longitude.Should().Be(-177.6767m);
            stationInfo.Elevation.Should().Be(1362.0m);
            stationInfo.Name.Should().Be("TAMANRASSET");
            stationInfo.IsCRN.Should().BeFalse();
            stationInfo.IsHCN.Should().BeFalse();
            stationInfo.IsGSN.Should().BeTrue();
            stationInfo.WMOId.Should().Be("6068");

            stationLine = "ACW00011604  17.1167  -61.7833   10.1    ST JOHNS COOLIDGE FLD                       ";
            parser.TryParseStationInfoLine(stationLine, out stationInfo).Should().BeTrue();

            stationLine = "CA1QC000018  45.8739  -74.0871  172.8 QC PRéVOST 0.6 N                              ";
            parser.TryParseStationInfoLine(stationLine, out stationInfo).Should().BeTrue();
        }
    }
}
