﻿using System;
using System.Collections.Generic;

namespace NOAA.GHCND.DataStructures
{
    public static class DayDataConstants
    {
        public const string MSG_NO_BUCKET_FOUND = "Could not calculate bucket for {0}";
        public const string MSG_NO_DATA_0 = "{0} is not included in the data";

        public static readonly DateTime MIN_DAY = new DateTime(1700, 1, 1);
        public static readonly DateTime MAX_DAY = DateTime.Now;
        public static int BUCKET_SIZE_YEARS = 10;
        public static readonly DateTime[] DAY_BUCKET_BOUNDARIES;

        static DayDataConstants()
        {
            var buckets = new List<DateTime>();
            for (var x = MIN_DAY.AddYears(BUCKET_SIZE_YEARS); x < MAX_DAY; x = x.AddYears(BUCKET_SIZE_YEARS))
            {
                buckets.Add(x);
            }
            buckets.Add(MAX_DAY);
            DAY_BUCKET_BOUNDARIES = buckets.ToArray();
        }
    }

    public class DayData<T> where T: IEquatable<T>
    {
        protected readonly Dictionary<string, T[][]> _dataByTypeMap = new Dictionary<string, T[][]>();
        protected readonly T _defaultValue;

        public DayData(T defaultValue)
{
            this._defaultValue = defaultValue;
        }

        public void AddDay(string dataType, DateTime day, T data)
        {
            if (false == this._dataByTypeMap.ContainsKey(dataType))
            {
                this._dataByTypeMap.Add(dataType, new T[DayDataConstants.DAY_BUCKET_BOUNDARIES.Length][]);
            }

            (var bucket, var offset) = this.GetDayBucketAndOffset(day);

            if (null == this._dataByTypeMap[dataType][bucket])
            {
                this.CreateDataBucket(dataType, bucket);
            }

            this._dataByTypeMap[dataType][bucket][offset] = data;
        }

        public T GetData(string dataType, DateTime date)
        {
            if (false == this._dataByTypeMap.ContainsKey(dataType))
            {
                return this._defaultValue;
            }

            return this.GetDataPoint(dataType, date);
        }

        public bool ContainsDataForDate(string dataType, DateTime date)
        {
            if (false == this._dataByTypeMap.ContainsKey(dataType))
            {
                return false;
            }

            return false == this._defaultValue.Equals(this.GetDataPoint(dataType, date));
        }

        protected T GetDataPoint(string dataType, DateTime day)
        {
            var (bucket, offset) = this.GetDayBucketAndOffset(day);
            return this._dataByTypeMap[dataType][bucket][offset];
        }

        protected void CreateDataBucket(string dataType, int bucketIndex)
        {
            int bucketSize = (bucketIndex == 0) ? 
                ((int)(DayDataConstants.DAY_BUCKET_BOUNDARIES[0] - DayDataConstants.MIN_DAY).TotalDays) :
                ((int)(DayDataConstants.DAY_BUCKET_BOUNDARIES[bucketIndex] - 
                    DayDataConstants.DAY_BUCKET_BOUNDARIES[bucketIndex - 1]).TotalDays);
            
            this._dataByTypeMap[dataType][bucketIndex] = new T[bucketSize];
            
            for (var i = 0; i < bucketSize; i++)
            {
                this._dataByTypeMap[dataType][bucketIndex][i] = this._defaultValue;
            }
        }

        protected (int bucket, int offset) GetDayBucketAndOffset(DateTime day)
        {
            if (day < DayDataConstants.MIN_DAY || day > DayDataConstants.MAX_DAY)
            {
                throw new ArgumentException(string.Format(DayDataConstants.MSG_NO_BUCKET_FOUND, day));
            }

            if (day < DayDataConstants.DAY_BUCKET_BOUNDARIES[0])
            {
                return (0, (int)(day - DayDataConstants.MIN_DAY).TotalDays);
            }

            for (int i = 1; i < DayDataConstants.DAY_BUCKET_BOUNDARIES.Length; i++)
            {
                if (day < DayDataConstants.DAY_BUCKET_BOUNDARIES[i])
                {
                    return (i, (int)(day - DayDataConstants.DAY_BUCKET_BOUNDARIES[i - 1]).TotalDays);
                }
            }

            throw new ArgumentException(string.Format(DayDataConstants.MSG_NO_BUCKET_FOUND, day));
        }
    }
}
