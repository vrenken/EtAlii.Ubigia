// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class SchemaProcessorOptions : ConfigurationBase, ISchemaProcessorOptions
    {
        /// <inheritdoc />
        public ISchemaScope SchemaScope { get; private set; }

        /// <inheritdoc />
        public ITraversalContext TraversalContext { get; private set; }

        /// <inheritdoc />
        public SchemaProcessorOptions Use(ISchemaScope scope)
        {
            SchemaScope = scope ?? throw new ArgumentException("No scope specified", nameof(scope));
            return this;
        }

        /// <inheritdoc />
        public SchemaProcessorOptions Use(ITraversalContext traversalContext)
        {
            TraversalContext = traversalContext ?? throw new ArgumentException("No traversal context specified", nameof(traversalContext));
            return this;
        }
    }
}
