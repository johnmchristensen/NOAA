using System;

namespace NOAA.GHCND
{
    public static class DataElementConstants
    {
        #region Core Elements

        public const string PRECIPITATION = "PRCP"; // tenths of mm
        public const string SNOWFALL = "SNOW"; // mm
        public const string SNOW_DEPTH = "SNWD"; // mm
        public const string MAX_TEMP = "TMAX"; // tenths of degrees C
        public const string MIN_TEMP = "TMIN"; // tenths of degrees C

        #endregion

        #region Cloudiness

        public const string AVERAGE_CLOUDINESS_CEILOMETER = "ACMC"; // Percent
        public const string AVERAGE_CLOUDINESS_MANUAL = "ACMH"; // percent
        public const string AVERAGE_DAY_CLOUDINESS_CEILOMETER = "ACSC"; // percent
        public const string AVERAGE_DAY_CLOUDINESS_MANUAL = "ACSH"; // percent

        #endregion

        #region Precipitation, Snowfall, Evaporation

        public const string NUMBER_DAYS_PRECIPITATION_TOTAL = "DAPR";
        public const string PRECIPITATION_MULTIDAY = "MDPR"; // tenths of mm

        public const string NUMBER_DAYS_SNOWFALL_TOTAL = "DASF";
        public const string SNOWFALL_MULTIDAY = "MDSF";
        public const string GROUND_SNOW_WATER_EQUIVALENT = "WESD"; // tenths of mm
        public const string SNOWFALL_WATER_EQUIVALENT = "WESF"; // tenths of mm

        public const string NUMBER_DAYS_ZERO_PRECIPITATION = "DWPR";

        public const string NUMBER_DAYS_EVAPORATION_TOTAL = "DAEV";
        public const string EVAPORATION = "EVAP"; // tenths of mm
        public const string EVAPORATION_MULTIDAY = "MDEV"; // tenths of mm


        #endregion

        #region Temperature

        public const string NUMBER_DAYS_MIN_TEMP_TOTAL = "DATN";
        public const string MIN_TEMP_MULTIDAY = "MDTN"; // tenths of degrees C

        public const string NUMBER_DAYS_MAX_TEMP_TOTAL = "DATX";
        public const string MAX_TEMP_MULTIDAY = "MXPN"; // tenths of degrees c

        public const string AVERAGE_TEMPERATURE = "TAVG"; // tenths of degree c
        public const string OBSERVATION_TEMPERATUR = "TOBS"; // tenths of degree c

        #endregion

        #region Wind

        public const string NUMBER_DAYS_WIND_MOVEMENT = "DAWM";
        public const string FASTEST_WIND_TIME = "FMTM"; // HHMM
        public const string PEAK_GUST_TIME = "PGTM"; // HHMM
        public const string AVERAGE_WIND_DIRECTION = "AWDR"; // degrees
        public const string AVERAGE_WIND_SPEED = "AWND"; // tenths of meters per second
        public const string DIRECTION_FASTEST_ONE_MINUTE_WIND = "WDF1"; // degrees
        public const string SPEED_FASTEST_ONE_MINUE_WIND = "WSF1"; // tenths of meters per second
        public const string DIRECTION_FASTEST_TWO_MINUTE_WIND = "WDF2"; // degrees
        public const string SPEED_FASTEST_TWO_MINUTE_WIND = "WSF2"; // tenths of meters per second
        public const string DIRECTION_FASTEST_FIVE_MINUTE_WIND = "WDF5"; // degrees
        public const string SPEED_FASTEST_FIVE_MINUTE_WIND = "WSF5"; // tenths of meters per second
        public const string DIRECTION_PEAK_GUST_WIND = "WDFG"; // degrees
        public const string SPEED_PEAK_GUST_WIND = "WSFG"; // tenths of meters per second
        public const string DIRECTION_HIGHEST_INSTANT_WIND = "WDFI"; // degrees
        public const string SPEED_HIGHEST_INSTANT_WIND = "WSFI"; // tenths of meters per second
        public const string DIRECTION_FASTEST_MILE_WIND = "WDFM"; // degrees
        public const string SPEED_FASTEST_MILE_WIND = "WSFM"; // tenths of meters per second";
        public const string TWENTY_FOUR_HOUR_MOVEMENT_WIND = "WDMV"; // km

        #endregion

        #region Ground Conditions

        public const string FROZEN_GROUND_LAYER_BASE = "FRGB"; // cm
        public const string FROZEN_GROUND_LAYER_TOP = "FRGT"; // cm
        public const string FROZEN_GROUND_LAYER_THICKNESS = "FRTH"; // cm
        public const string THICKNESS_ICE_WATER = "THIC"; // tenths of mm

        #endregion

        #region Sunshine

        public const string DAILY_SUN_PERCENT = "PSUN"; // Percent
        public const string DAILY_SUN_TOTAL = "TSUN"; // minutes

        #endregion
    }
}
