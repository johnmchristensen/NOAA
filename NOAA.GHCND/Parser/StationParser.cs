using NOAA.GHCND.Data;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NOAA.GHCND.Tests")]

namespace NOAA.GHCND.Parser
{
    public class StationParser
    {
        public const int LENGTH_STATION_ID = 11;
        public const int LENGTH_VALUE = 5;
        public const int LENGTH_ELEMENT = 4;
        public const int LENGTH_DATA = 8;
            
        public const int INDEX_YEAR = 11;
        public const int INDEX_MONTH = 15;
        public const int INDEX_ELEMENT = 17;
        public const int INDEX_DATA_START = 21;

        public const int INDEX_QUALITY = 6;

        public const int NO_DATA = -9999;

        public const string MSG_INVALID_ID = "Line ID does not match station id";

        public static HashSet<string> OverflowCodes = new HashSet<string>();

        public void ParseStationLine(string line, StationData station)
        {
            var stationId = line.Substring(0, LENGTH_STATION_ID);
            if (station.Id != line.Substring(0, LENGTH_STATION_ID))
            {
                throw new ArgumentException(MSG_INVALID_ID);
            }

            var dataType = line.Substring(INDEX_ELEMENT, LENGTH_ELEMENT);

            // Go through the data elements of the line, one at a time. Each corresponds to a day of the month, which we advance at the end of the loop.
            var date = new DateTime(int.Parse(line.Substring(INDEX_YEAR, 4)), int.Parse(line.Substring(INDEX_MONTH, 2)), 1);
            for (var i = INDEX_DATA_START; i <= 261; i += LENGTH_DATA)
            {
                if (this.TryParseData(line.Substring(i, LENGTH_DATA), out var data))
                {
                    if (data < short.MinValue || short.MaxValue < data)
                    {
                        OverflowCodes.Add(dataType);
                    }
                    station.AddDay(dataType, date, data);
                }
                date = date.AddDays(1);
            }
        }

        protected internal bool TryParseData(string dataElement, out int data)
        {
            // Return false if one of the following conditions is true:
            // 1. The value of the quality flag isn't a blank.
            // 2. The value of the data element is not parsable to an int.
            // 3. The value of the data (after parsing) is equal to no data.
            if ((dataElement[INDEX_QUALITY] != ' ') || (false == int.TryParse(dataElement.Substring(0, LENGTH_VALUE), out data)) || data == NO_DATA) 
            {
                data = 0;
                return false;
            }

            return true;
        }
    }
}
