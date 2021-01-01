using NOAA.GHCND.DataStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace NOAA.GHCND.Parser
{
    public class StationInfoParser
    {
        public const int LATITUDE_INDEX = 12;
        public const int LONGITUDE_INDEX = 21;
        public const int ELEVATION_INDEX = 32;
        public const int STATE_INDEX = 39;
        public const int NAME_INDEX = 41;
        public const int GSN_FLAG_INDEX = 72;
        public const int HCN_CRN_INDEX = 76;
        public const int WMO_INDEX = 80;

        public const int EXPECTED_LENGTH = 85;
        public const int ID_LENGTH = 10;
        public const int LATITUDE_LONGITUDE_LENGTH = 7;
        public const int ELEVATION_LENGTH = 5;
        public const int STATE_LENGTH = 2;
        public const int NAME_LENGTH = 29;
        public const int GSN_FLAG_LENGTH = 3;
        public const int HCN_CRN_LENGTH = 3;
        public const int WMO_LENGTH = 4;

        public static Dictionary<char, StationTypes> IDENTIFIER_TYPE_MAP = new Dictionary<char, StationTypes>
        {
            {'0', StationTypes.Unspecified },
            {'1', StationTypes.CoCoRaHS },
            {'C', StationTypes.USCooperative },
            {'E', StationTypes.ECAD },
            {'M', StationTypes.WMO },
            {'N', StationTypes.National},
            {'R', StationTypes.RAWS },
            {'S', StationTypes.SNOTEL },
            {'W', StationTypes.WBAN },
            {'P', StationTypes.Unspecified }
        };

        public bool TryParseStationInfoLine(string stationLine, out StationInfo stationInfo)
        {

            if (false == this.TryParseStationId(stationLine.Substring(0, ID_LENGTH), out var stationId)
                || false == decimal.TryParse(stationLine.Substring(LATITUDE_INDEX, LATITUDE_LONGITUDE_LENGTH), out var latitude)
                || false == decimal.TryParse(stationLine.Substring(LONGITUDE_INDEX, LATITUDE_LONGITUDE_LENGTH), out var longitude)
                || false == decimal.TryParse(stationLine.Substring(ELEVATION_INDEX, ELEVATION_LENGTH), out var elevation)
                || false == this.TryParseGSNFlag(stationLine.Substring(GSN_FLAG_INDEX, GSN_FLAG_LENGTH), out var isGSN)
                || false == this.TryParseHCNCRNFlag(stationLine.Substring(HCN_CRN_INDEX, HCN_CRN_LENGTH), out var isHCN, out var isCRN))
            {
                stationInfo = null;
                return false;
            }

            stationInfo = new StationInfo
            {
                Id = stationId,
                Latitude = latitude,
                Longitude = longitude,
                Elevation = elevation,
                IsGSN = isGSN,
                IsHCN = isHCN,
                IsCRN = isCRN,
                Name = stationLine.Substring(NAME_INDEX, NAME_LENGTH).Trim(),
                WMOId = stationLine.Substring(WMO_INDEX, WMO_LENGTH).Trim()
            };
            return true;
        }

        protected bool TryParseStationId(string stationIdString, out StationId stationId)
        {
            if (ID_LENGTH != stationIdString.Length || false == IDENTIFIER_TYPE_MAP.ContainsKey(stationIdString[2]))
            {
                stationId = null;
                return false;
            }

            stationId = new StationId
            {
                StationType = IDENTIFIER_TYPE_MAP[stationIdString[2]],
                CountryCode = stationIdString.Substring(0, 2),
                Id = stationIdString.Substring(3),
                FullId = stationIdString
            };
            return true;
        }

        protected bool TryParseGSNFlag(string gsnFlagString, out bool isGSN)
        {
            if (gsnFlagString != "   " && gsnFlagString != "GSN")
            {
                isGSN = default(bool);
                return false;
            }

            isGSN = (gsnFlagString == "GSN");
            return true;
        } 

        protected bool TryParseHCNCRNFlag(string hcnCRNFlagString, out bool isHCN, out bool isCRN)
        {
            if (hcnCRNFlagString != "   " &&  hcnCRNFlagString != "HCN" && hcnCRNFlagString != "CRN")
            {
                isHCN = default(bool);
                isCRN = default(bool);
                return false;
            }

            isHCN = (hcnCRNFlagString == "HCN");
            isCRN = (hcnCRNFlagString == "CRN");
            return true;
        }
    }
}
