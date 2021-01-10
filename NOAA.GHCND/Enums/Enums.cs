using System;
using System.Collections.Generic;
using System.Text;

namespace NOAA.GHCND.Enums
{
    public enum QueryOperator { Contains, Equals, GreaterThan, GreaterThanEqual, LessThan, LessThanEqual, Within, NotEquals, DoesNotContain }

    public enum StationInfoSearchFields { CountryCode, Latitude, Longitude, Elevation, State, Name, WMOID}
}
