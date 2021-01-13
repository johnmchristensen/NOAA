using NOAA.GHCND.Data;
using NOAA.GHCND.Enums;
using System.Linq;

namespace NOAA.GHCND.Rules
{
    public interface IStationInfoSearchQueryRule
    {
        IQueryable<StationInfo> GetFilteredStationInfoQueryable(IQueryable<StationInfo> queryable, SearchQueryParameter<StationInfoSearchFields>[] parameters);
    }
}