// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Antlr.Traversal;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class TestSchemaParserConfiguration : SchemaParserConfiguration
    {
        public TestSchemaParserConfiguration()
        {
            Use(new TraversalParserConfiguration().UseAntlr());
        }
    }
}
