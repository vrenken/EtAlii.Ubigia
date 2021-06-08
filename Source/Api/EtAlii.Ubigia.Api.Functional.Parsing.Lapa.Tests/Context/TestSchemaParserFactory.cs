// This file seems not in the right place. Should be moved to somewhere better.
// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Context
{
    internal class TestSchemaParserFactory : LapaSchemaParserFactory
    {
        public ISchemaParser Create() => base.Create(new TestSchemaParserConfiguration());
    }
}
