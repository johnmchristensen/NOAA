using System;
using FluentAssertions;
using NOAA.GHCND.Extensions;
using NUnit.Framework;

namespace NOAA.GHCND.Tests.Extensions
{
    [TestFixture]
    public class DateTimeExtensionsTest
    {
        [Test]
        public void CannotAddDaysPastMaxValue()
        {
            var date = DateTime.MaxValue.AddDays(-2);
            date.TryAddDays(3, out _).Should().BeFalse();
            date.TryAddDays(1, out var result).Should().BeTrue();
            result.Should().Be(date.AddDays(1));
        }
    }
}