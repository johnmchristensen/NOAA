using System;

namespace NOAA.GHCND.DataStructures
{
    public enum StationTypes
    {
        Unspecified,
        CoCoRaHS, //Community Collaborative Rain, Hail,and Snow
        USCooperative,
        ECAD, // ECA&D
        WMO, // World Meteorological Organization
        National,
        RAWS, // Community Collaborative Rain, Hail,and Snow
        SNOTEL, // U.S. Natural Resources Conservation Service SNOwpack TELemetry
        WBAN
    }

    public class StationId
    {
        public string CountryCode { get; set; }
        public string FullId { get; set; }
        public StationTypes StationType { get; set; }
        public string Id { get; set; }
    }

    public class StationInfo
    {
        public StationId Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal Elevation { get; set; }
        public string State { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Indicates the station is part of the GCOS Surface Network.
        /// </summary>
        public bool IsGSN { get; set; }

        /// <summary>
        /// Indicates station is part of the Historical Climatology Network.
        /// </summary>
        public bool IsHCN { get; set; }

        /// <summary>
        /// Indicates station is part of the US Climate Reference Network
        /// </summary>
        public bool IsCRN { get; set; }

        /// <summary>
        /// The station's id in the World Meteorological Organization, if it exists.
        /// </summary>
        public string WMOId { get; set; }
    }
}
