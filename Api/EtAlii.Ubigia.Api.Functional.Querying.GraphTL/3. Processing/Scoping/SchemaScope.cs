namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Collections.Generic;

    /// <summary>
    /// This class defines a scope in which a query can execute.
    /// It can be used to find variables or entries used by the query.
    /// </summary>
    public class SchemaScope : ISchemaScope
    {
        /// <summary>
        /// The recent value of the variables used in the query.
        /// </summary>
        public Dictionary<string, SchemaScopeVariable> Variables { get; }

        /// <summary>
        /// Create a new QueryScope instance. 
        /// Assign a Action to the output parameter to retrieve and process the results of the query.
        /// </summary>
        public SchemaScope()
        {
            Variables = new Dictionary<string, SchemaScopeVariable>();
        }
    }
}
