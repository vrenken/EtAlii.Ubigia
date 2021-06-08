// This file seems not in the right place. Should be moved to somewhere better.

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Antlr.Context;

    internal class TestSchemaParserFactory : AntlrSchemaParserFactory
    {
        public ISchemaParser Create() => base.Create(new TestSchemaParserConfiguration());
    }
}
