using NOAA.GHCND.Data;
using NOAA.GHCND.Search.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NOAA.GHCND.Search.Rules
{
    public class StationInfoCountryCodeQueryRule : ISearchFieldQueryRule<StationInfoSearchFields, StationInfo>
    {
        public const string MSG_INVALID_OPERATOR_0 = "Invalid operator {0}";

        public static HashSet<QueryOperator> AcceptableOperators = new HashSet<QueryOperator> { QueryOperator.Equals,
            QueryOperator.NotEquals, QueryOperator.Contains, QueryOperator.DoesNotContain };

        public IQueryable<StationInfo> FilterQueryable(IQueryable<StationInfo> queryable, QueryOperator op, string val)
        {
            if (false == AcceptableOperators.Contains(op))
            {
                throw new ArgumentException(string.Format(MSG_INVALID_OPERATOR_0, op));
            }

            switch (op)
            {
                case QueryOperator.Equals:
                    return queryable.Where(x => x.Id.CountryCode == val);
                case QueryOperator.NotEquals:
                    return queryable.Where(x => x.Id.CountryCode != val);
                case QueryOperator.Contains:
                    return queryable.Where(x => val.Split(',', StringSplitOptions.None).Contains(x.Id.CountryCode));
                case QueryOperator.DoesNotContain:
                    return queryable.Where(x => false == val.Split(',', StringSplitOptions.None).Contains(x.Id.CountryCode));
                default:
                    return queryable;
            }
        }

        public HashSet<QueryOperator> GetAcceptableOperators() => AcceptableOperators;

        public StationInfoSearchFields GetRuleField() => StationInfoSearchFields.CountryCode;
    }
}