using FluentAssertions;
using NOAA.GHCND.Parser;
using NUnit.Framework;
using System;
using System.Linq;

namespace NOAA.GHCND.Tests.Parser
{
    [TestFixture]
    public class StationParserTest
    {
        /// <summary>
        /// Verify that parsing a single datapoint will work correctly by parsing the data element if the quality flag is blank, 
        /// and returning false if the quality flag is not.
        /// </summary>
        [Test]
        public void WillParseDataPoint()
        {
            var validDataPoint =   "   13  r";
            var invalidDataPoint = "  991 KS";
            var missingDataPoint = "-9999   ";

            short data;

            new StationParser().TryParseData(validDataPoint, out data).Should().BeTrue();
            data.Should().Be(13);
            new StationParser().TryParseData(invalidDataPoint, out data).Should().BeFalse();
            new StationParser().TryParseData(missingDataPoint, out data).Should().BeFalse();
        }

        /// <summary>
        /// Verifies that attempting to parse a line of station data different to the station passed in throws an exception.
        /// </summary>
        [Test]
        public void WillNotParseDifferentStation()
        {
            var line = "AJ000037735201905TAVG  188H S  219H S  215H S  167H S  172H S  181H S  204H S  227H S  231H S  216H S  176H S  199H S  211H S  209H S  212H S  218H S  229H S  234H S  259H S  225H S  184H S  194H S  207H S  222H S  217H S  216H S  222H S  195H S  209H S  239H S  237H S";
            var station = new Station("DIFFERENTSTATION");

            Action exceptionThrowingAction = () => new StationParser().ParseStationLine(line, station);
            exceptionThrowingAction.Should().Throw<ArgumentException>();
        }

        [Test]
        public void WillParseStationData()
        {
            var line = "AJ000037735201905TAVG  001H S  002H S  003H S  004H S  005H S  006H S  007H S  008H S  009H S  010H S  011H S  012H S  013H S  014H S  015H S  016H S  017H S  018H S  019H S  020H S  021H S  022H S  023H S  024H S  025H S  026H S  027H S  028H S  029H S  030H S  031H S";
            var station = new Station("AJ000037735");

            new StationParser().ParseStationLine(line, station);
            var data = station.GetData("TAVG", new DateTime(2019, 5, 1), new DateTime(2019,5,30)).ToArray();
            for (short i = 0; i < data.Length; i++)
            {
                data[i].Should().Be((short) (i + 1));
            }

            Action exceptionThrowingAction = () => station.GetData("AVG", new DateTime(2019, 5, 1), new DateTime(2019, 5, 30)).ToArray();
            exceptionThrowingAction.Should().Throw<ArgumentException>();
        }
    }
}
