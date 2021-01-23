using NOAA.GHCND.Search.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NOAA.GHCND.Search.Rules
{
    public interface ISearchFieldQueryRule<TFieldEnum, TDTO> where TFieldEnum : System.Enum
    {
        TFieldEnum GetRuleField();

        HashSet<QueryOperator> GetAcceptableOperators();

        IQueryable<TDTO> FilterQueryable(IQueryable<TDTO> queryable, QueryOperator op, string val);
    }
}