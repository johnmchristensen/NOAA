using System;
namespace NOAA.GHCND.Rules
{
    public class NoConversionDataConversionRule : IDataConversionRule
    {
        public float ConvertDataPoint(int data) => (float)data;
    }
}
