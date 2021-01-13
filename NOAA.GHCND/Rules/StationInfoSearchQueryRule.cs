using NOAA.GHCND.Data;
using NOAA.GHCND.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NOAA.GHCND.Rules
{
    /// <summary>
    /// Handles adding search criteria to an IQueryable of StationInfo objects, based on a collection of SearchQueryParameters.
    /// </summary>
    public class StationInfoSearchQueryRule : IStationInfoSearchQueryRule
    {
        public const string MSG_INVALID_OPERATOR = "Invalid operator {0} for field {1}";

        public static readonly Dictionary<StationInfoSearchFields, HashSet<QueryOperator>> ALLOWED_OPERATORS_MAP =
            new Dictionary<StationInfoSearchFields, HashSet<QueryOperator>>
            {
                {StationInfoSearchFields.CountryCode, new HashSet<QueryOperator> { QueryOperator.Equals, QueryOperator.NotEquals,
                    QueryOperator.Contains, QueryOperator.DoesNotContain } }
            };

        public IQueryable<StationInfo> GetFilteredStationInfoQueryable(IQueryable<StationInfo> queryable,
            SearchQueryParameter<StationInfoSearchFields>[] parameters)
        {
            return queryable;
        }

        protected IQueryable<StationInfo> GetFilteredStationInfoQueryable(IQueryable<StationInfo> queryable, SearchQueryParameter<StationInfoSearchFields> parameter)
        {
            switch (parameter.Parameter)
            {
                case StationInfoSearchFields.CountryCode:
                    return this.GetCountryCodeFilteredStationInfoQueryable(queryable, parameter.Operator, parameter.Value);
                default:
                    return queryable;
            }
        }

        protected internal IQueryable<StationInfo> GetCountryCodeFilteredStationInfoQueryable(IQueryable<StationInfo> queryable, QueryOperator op, string val)
        {
            if (false == ALLOWED_OPERATORS_MAP[StationInfoSearchFields.CountryCode].Contains(op))
            {
                throw new ArgumentException(string.Format(MSG_INVALID_OPERATOR, op, StationInfoSearchFields.CountryCode));
            }

            if (QueryOperator.Equals == op)
            {
                return queryable.Where(x => x.Id.CountryCode == val);
            }
            else if (QueryOperator.NotEquals == op)
            {
                return queryable.Where(x => x.Id.CountryCode != val);
            }

            var values = val.Split(',');
            if (QueryOperator.Contains == op)
            {
                return queryable.Where(x => values.Contains(x.Id.CountryCode));
            }
            else if (QueryOperator.DoesNotContain == op)
            {
                return queryable.Where(x => false == values.Contains(x.Id.CountryCode));
            }

            return queryable;
        }
    }
}