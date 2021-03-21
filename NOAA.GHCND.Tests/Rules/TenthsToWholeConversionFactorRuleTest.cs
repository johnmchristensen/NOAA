using System;
using NUnit.Framework;
using FluentAssertions;
using NOAA.GHCND.Rules;

namespace NOAA.GHCND.Tests.Rules
{
    [TestFixture]
    public class TenthsToWholeConversionFactorRuleTest
    {
        [Test]
        public void WillConvertFromTenthsCelsiusToCelsius()
        {
            new TenthsToWholeConversionFactorRule().ConvertDataPoint(22).Should().Be(2.2f);
        }
    }
}
