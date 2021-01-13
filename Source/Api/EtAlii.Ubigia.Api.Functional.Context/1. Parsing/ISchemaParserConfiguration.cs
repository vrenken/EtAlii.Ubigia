namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public interface ISchemaParserConfiguration : IConfiguration
    {
        IPathParserFactory PathParserFactory { get; }

        SchemaParserConfiguration Use(IPathParserFactory pathParserFactory);
    }
}
