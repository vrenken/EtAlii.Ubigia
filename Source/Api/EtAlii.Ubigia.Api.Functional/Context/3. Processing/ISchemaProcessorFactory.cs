namespace EtAlii.Ubigia.Api.Functional.Context
{
    internal interface ISchemaProcessorFactory
    {
        ISchemaProcessor Create(SchemaProcessorConfiguration configuration);
    }
}
