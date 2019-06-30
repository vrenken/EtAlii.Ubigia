namespace EtAlii.Ubigia.Api.Functional
{
    using System.Collections.Generic;

    /// <summary>
    /// This class defines a scope in which a query can execute.
    /// It can be used to find variables or entries used by the query.
    /// </summary>
    public class QueryScope : IQueryScope
    {
        /// <summary>
        /// The recent value of the variables used in the query.
        /// </summary>
        public Dictionary<string, QueryScopeVariable> Variables { get; }

        /// <summary>
        /// Create a new QueryScope instance. 
        /// Assign a Action to the output parameter to retrieve and process the results of the query.
        /// </summary>
        public QueryScope()
        {
            Variables = new Dictionary<string, QueryScopeVariable>();
        }
    }
}
