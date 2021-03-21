using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Moq;
using NOAA.GHCND.Adapters;
using NOAA.GHCND.Data;
using NOAA.GHCND.Rules;
using NUnit.Framework;

namespace NOAA.GHCND.Tests.Rules
{
    [TestFixture]
    public class StationDatasetRuleTest
    {
        protected MapperConfiguration _mapperConfiguration;

        [SetUp]
        public void SetupUp()
        {
            this._mapperConfiguration = new MapperConfiguration(x => x.AddProfile<AdaptersProfile>());
        }
        
        [Test]
        public void WillGetMultipleDataTypes()
        {
            #region Setup Entities

            var dataTypeOne = "dataTypeOne";
            var dataTypeTwo = "dataTypeTwo";

            var dateStart = DateTime.Today.AddDays(-1);
            var dateEnd = DateTime.Today;
            
            #endregion
            
            #region Setup Mocks

            var stationDataMock = new Mock<IStationData>();
            stationDataMock.Setup(x => x.GetData(dataTypeOne, dateStart)).Returns(1);
            stationDataMock.Setup(x => x.GetData(dataTypeTwo, dateStart)).Returns(int.MinValue);
            stationDataMock.Setup(x => x.GetData(dataTypeOne, dateEnd)).Returns(2);
            stationDataMock.Setup(x => x.GetData(dataTypeTwo, dateEnd)).Returns(3);

            #endregion

            var stationDatasetRule = new StationDatasetRule(this._mapperConfiguration.CreateMapper(), null,
                new NoConversionDataConversionRule());

            var result = stationDatasetRule.GetDataSet(stationDataMock.Object, new string[] {dataTypeOne, dataTypeTwo},
                dateStart, dateEnd);

            result.Single(x => x.Date == dateStart).Data[dataTypeOne].Should().Be(1f);
            result.Single(x => x.Date == dateStart).Data[dataTypeTwo].Should().BeNull();
            result.Single(x => x.Date == dateEnd).Data[dataTypeOne].Should().Be(2f);
            result.Single(x => x.Date == dateEnd).Data[dataTypeTwo].Should().Be(3f);
        }

        [Test]
        public void WillGetDataFromMinToMax()
        {
            #region Setup Entities

            var dateStart = DateTime.Today.AddDays(-1);
            var dateEnd = DateTime.Today;
            
            #endregion
            
            #region Setup Mocks

            var stationDataMock = new Mock<IStationData>();
            stationDataMock.Setup(x => x.GetData(nameof(this.WillGetDataFromMinToMax), dateStart)).Returns(1);
            stationDataMock.Setup(x => x.GetData(nameof(this.WillGetDataFromMinToMax), dateEnd)).Returns(2);

            #endregion

            var stationDatasetRule = new StationDatasetRule(this._mapperConfiguration.CreateMapper(), null,
                new NoConversionDataConversionRule());

            var result = stationDatasetRule.GetDataSet(stationDataMock.Object, nameof(this.WillGetDataFromMinToMax),
                DateTime.MinValue, DateTime.MaxValue);
            result.Single(x => x.Date == dateStart).Data[nameof(this.WillGetDataFromMinToMax)].Should().Be(1f);
        }

        [Test]
        public void WillNotGetDataPointForDayWithNoData()
        {
            #region Setup Entities

            var dateStart = DateTime.Today.AddDays(-1);
            var dateEnd = DateTime.Today;
            
            #endregion
            
            #region Setup Mocks

            var stationDataMock = new Mock<IStationData>();
            stationDataMock.Setup(x => x.GetData(nameof(this.WillGetDataFromMinToMax), It.IsAny<DateTime>()))
                .Returns(int.MinValue);
            stationDataMock.Setup(x => x.GetData(nameof(this.WillGetDataFromMinToMax), dateStart)).Returns(1);
            stationDataMock.Setup(x => x.GetData(nameof(this.WillGetDataFromMinToMax), dateEnd)).Returns(2);

            #endregion

            var stationDatasetRule = new StationDatasetRule(this._mapperConfiguration.CreateMapper(), null,
                new NoConversionDataConversionRule());

            var result = stationDatasetRule.GetDataSet(stationDataMock.Object, nameof(this.WillGetDataFromMinToMax),
                DateTime.MinValue, DateTime.MaxValue);
            result.Single(x => x.Date == dateStart).Data[nameof(this.WillGetDataFromMinToMax)].Should().Be(1f);
            result.Count().Should().Be(2);
        }
    }
}