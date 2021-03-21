using System;
namespace NOAA.GHCND.Rules
{
    public class TenthsToWholeConversionFactorRule : IDataConversionRule
    {
        public float ConvertDataPoint(int data)
        {
            return (float)data / 10f;
        }
    }
}
