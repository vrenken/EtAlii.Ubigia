// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class SchemaParserConfiguration : ConfigurationBase, ISchemaParserConfiguration
    {
        public TraversalParserConfiguration TraversalParserConfiguration { get; private set; }

        public SchemaParserConfiguration Use(TraversalParserConfiguration traversalParserConfiguration)
        {
            TraversalParserConfiguration = traversalParserConfiguration ?? throw new ArgumentException("No traversal parser configuration specified", nameof(traversalParserConfiguration));
            return this;
        }

    }
}
