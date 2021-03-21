using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NOAA.GHCND.Adapters;
using NOAA.GHCND.Collections;
using NOAA.GHCND.Data;
using NOAA.GHCND.Extensions;

namespace NOAA.GHCND.Rules
{
    public class StationDatasetRule : IStationDatasetRule
    {
        protected readonly IMapper _mapper;
        protected readonly IDictionary<string, IDataConversionRule> _dataConverters = new Dictionary<string, IDataConversionRule>();
        protected readonly NoConversionDataConversionRule _noConversionRule;

        public StationDatasetRule(IMapper mapper, TenthsToWholeConversionFactorRule tenthsToWholeRule, NoConversionDataConversionRule noConversionRule)
        {
            this._mapper = mapper;
            this._noConversionRule = noConversionRule;

            var tenthsToWholeElements = new string[] {
                DataElementConstants.AVERAGE_TEMPERATURE, DataElementConstants.MAX_TEMP, DataElementConstants.MAX_TEMP_MULTIDAY, DataElementConstants.MIN_TEMP,
                DataElementConstants.MIN_TEMP_MULTIDAY, DataElementConstants.OBSERVATION_TEMPERATUR, DataElementConstants.PRECIPITATION, DataElementConstants.PRECIPITATION_MULTIDAY,
                DataElementConstants.AVERAGE_WIND_SPEED, DataElementConstants.SPEED_FASTEST_FIVE_MINUTE_WIND, DataElementConstants.SPEED_FASTEST_MILE_WIND, DataElementConstants.SPEED_FASTEST_ONE_MINUE_WIND,
                DataElementConstants.SPEED_FASTEST_TWO_MINUTE_WIND, DataElementConstants.SPEED_HIGHEST_INSTANT_WIND, DataElementConstants.SPEED_PEAK_GUST_WIND, DataElementConstants.EVAPORATION,
                DataElementConstants.EVAPORATION_MULTIDAY, DataElementConstants.THICKNESS_ICE_WATER};

            foreach (var e in tenthsToWholeElements)
            {
                this._dataConverters.Add(e, tenthsToWholeRule);
            }
        }

        public IEnumerable<DataPointDTO> GetDataSet(IStationData stationData, string dataType, DateTime start, DateTime end)
        {
            return this.GetDataSet(stationData, dataType.MakeArray(), start, end);
        }

        public IEnumerable<DataPointDTO> GetDataSet(IStationData stationData, string[] dataTypes, DateTime start,
            DateTime end)
        {
            DataPointDTO GetData(DateTime date)
            {
                var dataAdapter = new DataPointDTOAdapter(date, this._mapper);
                foreach (var dt in dataTypes)
                {
                    dataAdapter.AddData(dt, this.GetConvertedData(stationData, dt, date));
                }

                return dataAdapter.ToDTO();
            }
            
            var currentDate = start;
            do
            {
                var dataPoint = GetData(currentDate);
                if (dataPoint.Data.Any())
                {
                    yield return GetData(currentDate);
                }
            } while (currentDate.TryAddDays(1, out currentDate));
        }

        protected float? GetConvertedData(IStationData stationData, string dataType, DateTime date)
        {
            var data = stationData.GetData(dataType, date);

            return (data != int.MinValue)
                ? (float?) this._dataConverters.GetValueOrDefault(dataType, this._noConversionRule).ConvertDataPoint(data)
                : null;
        }
    }
}
