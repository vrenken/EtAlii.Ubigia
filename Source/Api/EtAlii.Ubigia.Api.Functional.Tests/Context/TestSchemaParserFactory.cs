// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Context
{
#if USE_LAPA_PARSER_IN_TESTS
    internal class TestSchemaParserFactory : LapaSchemaParserFactory
    {
        public ISchemaParser Create()
        {
            var parserOptions = new ParserOptions().UseLapa();
            return base.Create(parserOptions);
        }
    }
#else
    using EtAlii.Ubigia.Api.Functional.Antlr.Context;
    using EtAlii.Ubigia.Api.Functional.Antlr.Traversal;

    internal class TestSchemaParserFactory : AntlrSchemaParserFactory
    {
        public ISchemaParser Create()
        {
            var parserOptions = new ParserOptions().UseAntlr();
            return base.Create(parserOptions);
        }
    }
#endif
}
