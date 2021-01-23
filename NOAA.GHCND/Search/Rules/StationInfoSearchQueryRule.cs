using NOAA.GHCND.Data;
using NOAA.GHCND.Search.Attributes;
using NOAA.GHCND.Search.Enums;
using System;
using System.Linq;

namespace NOAA.GHCND.Search.Rules
{
    /// <summary>
    /// Handles adding search criteria to an IQueryable of StationInfo objects, based on a collection of SearchQueryParameters.
    /// </summary>
    public class StationInfoSearchQueryRule : IStationInfoSearchQueryRule
    {
        protected readonly ISearchFieldQueryRule<StationInfoSearchFields, StationInfo> _countryCodeSearchRule;
        protected readonly IServiceProvider _serviceProvider;

        public StationInfoSearchQueryRule(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public IQueryable<StationInfo> GetFilteredStationInfoQueryable(IQueryable<StationInfo> queryable,
            SearchQueryParameter<StationInfoSearchFields>[] parameters)
        {
            foreach (var p in parameters)
            {
                queryable = this.GetFilteredStationInfoQueryable(queryable, p.Parameter, p.Operator, p.Value);
            }
            return queryable;
        }

        protected IQueryable<StationInfo> GetFilteredStationInfoQueryable(IQueryable<StationInfo> queryable, StationInfoSearchFields searchField,
            QueryOperator op, string val)
        {
            // Get the SearchFieldRuleAttribute and determine what type should handle this.
            var enumType = typeof(StationInfoSearchFields);
            var member = enumType.GetMember(searchField.ToString()).Single(x => x.DeclaringType == enumType);
            var ruleTypeAttribute = (SearchFieldRuleAttribute)member.GetCustomAttributes(typeof(SearchFieldRuleAttribute), true).Single();

            // Obtain an instance of that type of the service provider.
            var rule = (ISearchFieldQueryRule<StationInfoSearchFields, StationInfo>)this._serviceProvider.GetService(ruleTypeAttribute.SearchRuleType);

            // Use that to perform the filtering.
            return rule.FilterQueryable(queryable, op, val);
        }
    }
}