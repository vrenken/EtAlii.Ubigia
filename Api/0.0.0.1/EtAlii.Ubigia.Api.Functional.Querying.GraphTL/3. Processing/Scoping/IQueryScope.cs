namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Collections.Generic;

    public interface IQueryScope
    {
        /// <summary>
        /// The recent value of the variables used in the query.
        /// </summary>
        Dictionary<string, QueryScopeVariable> Variables { get; }
    }
}