// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    /// <inheritdoc />
    public class SchemaScope : ISchemaScope
    {
        /// <inheritdoc />
        public Dictionary<string, ScopeVariable> Variables { get; }

        /// <summary>
        /// Create a new SchemaScope instance.
        /// </summary>
        public SchemaScope()
        {
            Variables = new Dictionary<string, ScopeVariable>();
        }

        /// <inheritdoc />
        public SchemaExecutionScope CreateExecutionScope() => new SchemaExecutionScope(Variables);
    }
}
