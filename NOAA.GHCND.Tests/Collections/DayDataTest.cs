﻿using FluentAssertions;
using NOAA.GHCND.Collections;
using NUnit.Framework;
using System;
using System.Linq;

namespace NOAA.GHCND.Tests.Collections
{
    [TestFixture]   
    public class DayDataTest
    {
        protected static string DATA_TYPE = "TEST";

        protected Random _randomizer = new Random();

        [Test]
        public void CanAddAndReceiveData()
        {
            var data = _randomizer.Next();
            var date = DateTime.Now.AddDays(-1 * _randomizer.Next(1, 30));

            var container = new DayData<int>(int.MinValue);
            container.AddDay(DATA_TYPE, date, data);
            container.GetData(DATA_TYPE, date).Should().Be(data);

            container.GetData(nameof(this.CanAddAndReceiveData), date).Should().Be(int.MinValue);

            var missingDate = DateTime.Now.AddDays(-1 * _randomizer.Next(31, 60));
            container.GetData(DATA_TYPE, missingDate).Should().Be(int.MinValue);
        }

        [Test]
        public void CannotAddDataBeforeOrAfterBoundary()
        {
            var container = new DayData<int>(int.MinValue);

            ((Action)(() => container.AddDay(DATA_TYPE, DayDataConstants.MIN_DAY.AddDays(-1), _randomizer.Next())))
                .Should().Throw<ArgumentException>();

            ((Action)(() => container.AddDay(DATA_TYPE, DayDataConstants.MAX_DAY.AddDays(1), _randomizer.Next())))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void CanTestIfDataPresent()
        {
            var data = _randomizer.Next();
            var date = DateTime.Now.AddDays(-1 * _randomizer.Next(2, 30));

            var container = new DayData<int>(int.MinValue);
            container.AddDay(DATA_TYPE, date, data);
            container.ContainsDataForDate(DATA_TYPE, date).Should().BeTrue();
            container.ContainsDataForDate(DATA_TYPE, date.AddDays(1)).Should().BeFalse();
            container.ContainsDataForDate(nameof(this.CanTestIfDataPresent), date).Should().BeFalse();
            container.ContainsDataForDate(nameof(this.CanTestIfDataPresent), date.AddDays(1)).Should().BeFalse();
        }

        [Test]
        public void WhenGettingDataFromBeyondLastDay_DefaultReturned()
        {
            var container = new DayData<int>(int.MinValue);
            container.AddDay(nameof(this.WhenGettingDataFromBeyondLastDay_DefaultReturned), DateTime.Today.AddDays(-5), 1);
            container.GetData(nameof(this.WhenGettingDataFromBeyondLastDay_DefaultReturned), DateTime.Today)
                .Should()
                .Be(int.MinValue);
        }

        [Test]
        public void WhenGettingDataFromBeforeMinDay_DefaultReturned()
        {
            var container = new DayData<int>(int.MinValue);
            container.GetData(nameof(this.WhenGettingDataFromBeforeMinDay_DefaultReturned),
                DateTime.MinValue).Should().Be(int.MinValue);
        }

        [Test]
        public void WhenGettingDataFromAfterMaxDay_DefaultReturned()
        {
            var container = new DayData<int>(int.MinValue);
            container.GetData(nameof(this.WhenGettingDataFromAfterMaxDay_DefaultReturned),
                DateTime.MaxValue).Should().Be(int.MinValue);
        }
    }
}
