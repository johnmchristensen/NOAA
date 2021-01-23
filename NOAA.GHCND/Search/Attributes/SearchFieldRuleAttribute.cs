using System;

namespace NOAA.GHCND.Search.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SearchFieldRuleAttribute : Attribute
    {
        public Type SearchRuleType
        {
            get;
            set;
        }

        public SearchFieldRuleAttribute(Type searchRuleType)
        {
            this.SearchRuleType = searchRuleType;
        }
    }
}