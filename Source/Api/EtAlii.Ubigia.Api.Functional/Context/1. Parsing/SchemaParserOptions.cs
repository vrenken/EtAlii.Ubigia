// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class SchemaParserOptions : ConfigurationBase, ISchemaParserOptions
    {
        public TraversalParserOptions TraversalParserOptions { get; private set; }

        public SchemaParserOptions Use(TraversalParserOptions traversalParserOptions)
        {
            TraversalParserOptions = traversalParserOptions ?? throw new ArgumentException("No traversal parser options specified", nameof(traversalParserOptions));
            return this;
        }

    }
}
