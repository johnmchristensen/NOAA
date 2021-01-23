using NOAA.GHCND.Search.Attributes;
using NOAA.GHCND.Search.Rules;
using System;

namespace NOAA.GHCND.Search.Enums
{
    public enum QueryOperator
    {
        Contains = 0,
        Equals = 1,
        GreaterThan = 2,
        GreaterThanEqual = 3,
        LessThan = 4,
        LessThanEqual = 5,
        Within = 6,
        NotEquals = 7,
        DoesNotContain = 8
    }

    public enum StationInfoSearchFields
    {
        [SearchFieldRule(typeof(StationInfoCountryCodeQueryRule))]
        CountryCode = 0,

        Latitude = 1,
        Longitude = 2,
        Elevation = 3,
        State = 4,
        Name = 5,
        WMOID = 6
    }
}