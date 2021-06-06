// This file seems not in the right place. Should be moved to somewhere better.
// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Antlr.Context;

#if USE_LAPA_PARSER_IN_TESTS
    internal class TestSchemaParserFactory : LapaSchemaParserFactory
#else
    internal class TestSchemaParserFactory : AntlrSchemaParserFactory
#endif
    {
        public ISchemaParser Create() => base.Create(new TestSchemaParserConfiguration());
    }
}
