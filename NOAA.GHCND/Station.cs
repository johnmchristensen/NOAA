using System;
using System.Collections.Generic;

namespace NOAA.GHCND
{
    public class Station
    {
        public const string MSG_NO_DATA_0 = "{0} is not included in the data for this station";
        public const string MSG_NO_BUCKET_FOUND = "Could not calculate bucket for {0}";

        public static readonly DateTime MIN_DAY = new DateTime(1700, 1, 1);
        public static readonly DateTime MAX_DAY = DateTime.Now;
        public static int BUCKET_SIZE_YEARS = 10;
        public static readonly DateTime[] DAY_BUCKET_BOUNDARIES;
            //new DateTime[] { new DateTime(1800, 1, 1), new DateTime(1900, 1, 1), new DateTime(2000, 1, 1), MAX_DAY };

        protected readonly Dictionary<string, short[][]> _dataByTypeMap = new Dictionary<string, short[][]>();

        static Station()
        {
            var buckets = new List<DateTime>();
            for (var x = MIN_DAY.AddYears(BUCKET_SIZE_YEARS); x < MAX_DAY; x = x.AddYears(BUCKET_SIZE_YEARS))
            {
                buckets.Add(x);
            }
            buckets.Add(MAX_DAY);
            DAY_BUCKET_BOUNDARIES = buckets.ToArray();
        }

        public Station(string stationId)
        {
            this.Id = stationId;
        }

        public string Id { get; }

        public void AddDay(string dataType, DateTime day, short data) 
        {
            if (false == this._dataByTypeMap.ContainsKey(dataType))
            {
                this._dataByTypeMap.Add(dataType, new short[DAY_BUCKET_BOUNDARIES.Length][]);
            }

            (var bucket, var offset) = this.GetDayBucketAndOffset(day);
            
            if (null == this._dataByTypeMap[dataType][bucket])
            {
                this.CreateDataBucket(dataType, bucket);
            }

            this._dataByTypeMap[dataType][bucket][offset] = data;
        }

        public IEnumerable<short> GetData(string dataType, DateTime startDate, DateTime endDate)
        {
            if (false == this._dataByTypeMap.ContainsKey(dataType))
            {
                throw new ArgumentException(string.Format(MSG_NO_DATA_0, dataType));
            }

            var currentDate = startDate;
            while (currentDate <= endDate)
            {
                yield return this.GetDataPoint(dataType, currentDate);
                currentDate = currentDate.AddDays(1);
            }
        }

        protected (int bucket, int offset) GetDayBucketAndOffset(DateTime day)
        {
            if (day < MIN_DAY || day > MAX_DAY)
            {
                throw new ArgumentException(string.Format(MSG_NO_BUCKET_FOUND, day));
            }

            if (day < DAY_BUCKET_BOUNDARIES[0])
            {
                return (0, (int)(day - MIN_DAY).TotalDays);
            }

            for (int i = 1; i < DAY_BUCKET_BOUNDARIES.Length; i++)
            {
                if (day < DAY_BUCKET_BOUNDARIES[i])
                {
                    return (i, (int)(day - DAY_BUCKET_BOUNDARIES[i - 1]).TotalDays);
                }
            }

            throw new ArgumentException(string.Format(MSG_NO_BUCKET_FOUND, day));
        }
            
        protected short GetDataPoint(string dataType, DateTime day)
        {
            var (bucket, offset) = this.GetDayBucketAndOffset(day);
            return this._dataByTypeMap[dataType][bucket][offset];
        }

        protected void CreateDataBucket(string dateType, int bucketIndex)
        {
            int bucketSize = (bucketIndex == 0) ? ((int)(DAY_BUCKET_BOUNDARIES[0] - MIN_DAY).TotalDays) :
                ((int)(DAY_BUCKET_BOUNDARIES[bucketIndex] - DAY_BUCKET_BOUNDARIES[bucketIndex - 1]).TotalDays);
            this._dataByTypeMap[dateType][bucketIndex] = new short[bucketSize];
        }
    }
}
