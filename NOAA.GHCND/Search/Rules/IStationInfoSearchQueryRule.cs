using NOAA.GHCND.Data;
using NOAA.GHCND.Search.Enums;
using System.Linq;

namespace NOAA.GHCND.Search.Rules
{
    public interface IStationInfoSearchQueryRule
    {
        IQueryable<StationInfo> GetFilteredStationInfoQueryable(IQueryable<StationInfo> queryable, SearchQueryParameter<StationInfoSearchFields>[] parameters);
    }
}