namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public interface ISchemaParserConfiguration : IConfiguration
    {
        TraversalParserConfiguration TraversalParserConfiguration { get; }

        SchemaParserConfiguration Use(TraversalParserConfiguration traversalParserConfiguration);
    }
}
