namespace EtAlii.Ubigia.Api.Functional.Context
{
    internal interface ISchemaParserFactory
    {
        ISchemaParser Create(SchemaParserConfiguration configuration);
    }
}
