using System;
namespace NOAA.GHCND.Rules
{
    public interface IDataConversionRule
    {
        float ConvertDataPoint(int data);
    }
}
