using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NOAA.GHCND.Data;
using NOAA.GHCND.Parser;
using NOAA.GHCND.Search.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOAA.GHCND.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationInfoController : ControllerBase
    {
        protected readonly HistoricClimateDatabase _database;

        public StationInfoController(HistoricClimateDatabase database)
        {
            this._database = database;
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

        [HttpPost]
        [Route("find")]
        public StationInfo[] Find(SearchQueryParameter<StationInfoSearchFields>[] parameters)
        {
            return this._database.FindStationInfos(parameters);
        }
    }
}