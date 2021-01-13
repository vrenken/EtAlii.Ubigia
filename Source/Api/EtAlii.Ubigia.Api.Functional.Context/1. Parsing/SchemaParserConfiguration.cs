namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class SchemaParserConfiguration : ConfigurationBase, ISchemaParserConfiguration
    {
        public IPathParserFactory PathParserFactory { get; private set; }

        public SchemaParserConfiguration Use(IPathParserFactory pathParserFactory)
        {
            PathParserFactory = pathParserFactory ?? throw new ArgumentException("No path parser factory specified", nameof(pathParserFactory));
            return this;
        }

    }
}
