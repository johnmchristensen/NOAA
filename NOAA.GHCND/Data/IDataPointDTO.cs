using System;
using System.Collections.Generic;

namespace NOAA.GHCND.Data
{
    public interface IDataPointDTOGetter
    {
        DateTime Date { get; }
        IReadOnlyDictionary<string, float?> Data { get; }
    }

    public interface IDataPointDTOSetter
    {
        DateTime Date { set; }
        IReadOnlyDictionary<string, float?> Data { set; }
    }

    public interface IDataPointDTO : IDataPointDTOGetter, IDataPointDTOSetter
    {
    }
}