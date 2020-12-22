namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.Generic;

    public interface ISchemaScope
    {
        /// <summary>
        /// The recent value of the variables used in the query.
        /// </summary>
        Dictionary<string, SchemaScopeVariable> Variables { get; }
    }
}
