using FluentAssertions;
using NOAA.GHCND.Data;
using NOAA.GHCND.Enums;
using NOAA.GHCND.Rules;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NOAA.GHCND.Tests.Rules
{
    [TestFixture]
    public class StationInfoSearchQueryRuleTest
    {
        [Test]
        public void CanSearchOnCountryCode()
        {
            var station = new StationInfo
            {
                Id = new StationId
                {
                    CountryCode = "US"
                }
            };

            var stationQueryable = new List<StationInfo> { station }.AsQueryable();

            // Will find when searching on equals.
            new StationInfoSearchQueryRule()
                .GetCountryCodeFilteredStationInfoQueryable(stationQueryable, QueryOperator.Equals, "US")
                .Should().Contain(station);

            // Will not get when searching on not equals.
            new StationInfoSearchQueryRule()
                .GetCountryCodeFilteredStationInfoQueryable(stationQueryable, QueryOperator.NotEquals, "US")
                .Should().BeEmpty();

            // Will find when searching on contains.
            new StationInfoSearchQueryRule()
                .GetCountryCodeFilteredStationInfoQueryable(stationQueryable, QueryOperator.Contains, "US,CA")
                .Should().Contain(station);

            // Will not find when searching on does not contains.
            new StationInfoSearchQueryRule()
                .GetCountryCodeFilteredStationInfoQueryable(stationQueryable, QueryOperator.DoesNotContain, "US,CA")
                .Should().BeEmpty();
        }
    }
}