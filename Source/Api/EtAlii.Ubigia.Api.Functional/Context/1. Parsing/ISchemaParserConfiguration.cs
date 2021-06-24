// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public interface ISchemaParserConfiguration : IConfiguration
    {
        TraversalParserConfiguration TraversalParserConfiguration { get; }

        SchemaParserConfiguration Use(TraversalParserConfiguration traversalParserConfiguration);
    }
}
