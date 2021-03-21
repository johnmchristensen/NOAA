using System;
using System.Collections.Generic;
using AutoMapper;
using NOAA.GHCND.Data;

namespace NOAA.GHCND.Adapters
{
    public class DataPointDTOAdapter : IDataPointDTOGetter
    {
        protected readonly Dictionary<string, float?> _data = new Dictionary<string, float?>();
        protected readonly IMapper _mapper;

        public DataPointDTOAdapter(DateTime date, string dataType, float? data, IMapper mapper) : this(date, mapper)
        {
            this._data.Add(dataType, data);
        }

        public DataPointDTOAdapter(DateTime date, IMapper mapper)
        {
            this.Date = date;
            this._mapper = mapper;
        }

        public DateTime Date { get; }
        public IReadOnlyDictionary<string, float?> Data => this._data;

        public DataPointDTO ToDTO() => this._mapper.Map<IDataPointDTOGetter, DataPointDTO>(this);

        public void AddData(string dataType, float? data) => this._data.Add(dataType, data);
    }
}
