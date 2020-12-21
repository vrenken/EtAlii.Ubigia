namespace EtAlii.Ubigia.Api.Functional
{
    internal interface ISchemaParserFactory
    {
        ISchemaParser Create(SchemaParserConfiguration configuration);
    }
}