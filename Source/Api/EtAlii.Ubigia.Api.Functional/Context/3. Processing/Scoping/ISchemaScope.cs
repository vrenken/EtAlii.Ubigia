namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    /// <summary>
    /// A class to facilitate the state of any schema-wide variables.
    /// </summary>
    public interface ISchemaScope
    {
        /// <summary>
        /// The recent value of the variables made available to the schema.
        /// </summary>
        Dictionary<string, ScopeVariable> Variables { get; }

        /// <summary>
        /// Returns a schema execution scope - that is a subset of the schema-wide scope, limited to one single execution.
        /// </summary>
        /// <returns></returns>
        SchemaExecutionScope CreateExecutionScope();
    }
}
