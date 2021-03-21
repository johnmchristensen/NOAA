using Microsoft.AspNetCore.Mvc;
using NOAA.GHCND.Data;
using System;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using NOAA.GHCND.Exceptions;
using NOAA.GHCND.Rules;

namespace NOAA.GHCND.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationInfoController : ControllerBase
    {
        protected readonly HistoricClimateDatabase _database;
        protected readonly IStationDatasetRule _stationDatasetRule;

        public StationInfoController(HistoricClimateDatabase database, IStationDatasetRule stationDatasetRule)
        {
            this._database = database;
            this._stationDatasetRule = stationDatasetRule;
        }

        [HttpGet]
        public StationInfo[] GetList()
        {
            return this._database.StationInfos;
        }

        [Route("{id}")]
        public ActionResult<StationInfo> Get(string id)
        {
            if (false == this._database.StationInfos.Any(x => x.Id.FullId == id))
            {
                return NotFound();
            }

            return this._database.StationInfos.Single(x => x.Id.FullId == id);
        }

        [Route("{stationId}/{dataType}/firstDate")]
        public ActionResult<DateTime> GetFirstDateWithData(string stationId, string dataType)
        {
            try
            {
                var data = this._database.GetStationData(stationId);
                if (false == data.ContainsDataForType(dataType))
                {
                    throw new NotFoundException(dataType);
                }

                return data.GetMinimumDateWithData(dataType);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex);
            }
        }
        
        [Route("{stationId}/{dataType}/data")]
        public ActionResult<DataPointDTO[]> GetData(string stationId, string dataType, DateTime start, DateTime end)
        {
            try
            {
                var data = this._database.GetStationData(stationId);
                if (false == data.ContainsDataForType(dataType))
                {
                    throw new NotFoundException(dataType);
                }

                return this._stationDatasetRule.GetDataSet(data, dataType, start, end).ToArray();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex);
            }
        }

        [Route("{stationId}/availableData")]
        public ActionResult<string[]> GetDataTypes(string stationId)
        {
            try
            {
                return this._database.GetStationData(stationId).GetAvailableData();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex);
            }
        }

        [Route("{stationId}/data")]
        [HttpPost]
        public ActionResult<DataPointDTO[]> GetData(string stationId, string[] dataTypes, DateTime start, DateTime end)
        {
            try
            {
                var data = this._database.GetStationData(stationId);
                if (dataTypes.All(x => false == data.ContainsDataForType(x)))
                {
                    throw new NotFoundException(string.Join(",", dataTypes));
                }

                return this._stationDatasetRule.GetDataSet(data, dataTypes, start, end).ToArray();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex);
            }
        }

        [Route("{stationId}/allData")]
        public ActionResult<DataPointDTO[]> GetAllData(string stationId)
        {
            try
            {
                var data = this._database.GetStationData(stationId);

                return this._stationDatasetRule.GetDataSet(data, data.GetAvailableData(), DateTime.MinValue,DateTime.MaxValue).ToArray();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex);
            }
        }
    }
}
