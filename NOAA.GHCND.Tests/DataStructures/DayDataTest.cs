using FluentAssertions;
using NOAA.GHCND.DataStructures;
using NUnit.Framework;
using System;
using System.Linq;

namespace NOAA.GHCND.Tests.DataStructures
{
    [TestFixture]
    public class DayDataTest
    {
        protected static string DATA_TYPE = "TEST";

        protected Random _randomizer = new Random();

        [Test]
        public void CanAddAndReceiveData() 
        {
            var data = this._randomizer.Next();
            var date = DateTime.Now.AddDays(-1 * this._randomizer.Next(1, 30));

            var container = new DayData<int>(int.MinValue);
            container.AddDay(DATA_TYPE, date, data);
            container.GetData(DATA_TYPE, date, date).First().Should().Be(data);

            ((Action) (() => container.GetData(nameof(this.CanAddAndReceiveData), date, date).ToArray()))
                .Should().Throw<ArgumentException>();

            var missingDate = DateTime.Now.AddDays(-1 * this._randomizer.Next(31, 60));
            container.GetData(DATA_TYPE, missingDate, missingDate).First().Should().Be(int.MinValue);
        }

        [Test]
        public void CannotAddDataBeforeOrAfterBoundary() 
        {
            var container = new DayData<int>(int.MinValue);

            ((Action)(() => container.AddDay(DATA_TYPE, DayDataConstants.MIN_DAY.AddDays(-1), this._randomizer.Next())))
                .Should().Throw<ArgumentException>();

            ((Action)(() => container.AddDay(DATA_TYPE, DayDataConstants.MAX_DAY.AddDays(1), this._randomizer.Next())))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void CanTestIfDataPresent()
        {
            var data = this._randomizer.Next();
            var date = DateTime.Now.AddDays(-1 * this._randomizer.Next(2, 30));

            var container = new DayData<int>(int.MinValue);
            container.AddDay(DATA_TYPE, date, data);
            container.ContainsDataForDate(DATA_TYPE, date).Should().BeTrue();
            container.ContainsDataForDate(DATA_TYPE, date.AddDays(1)).Should().BeFalse();
            container.ContainsDataForDate(nameof(this.CanTestIfDataPresent), date).Should().BeFalse();
            container.ContainsDataForDate(nameof(this.CanTestIfDataPresent), date.AddDays(1)).Should().BeFalse();
        }
    }
}
