namespace EtAlii.Ubigia.Api.Functional 
{
    internal interface ISchemaProcessorFactory
    {
        ISchemaProcessor Create(SchemaProcessorConfiguration configuration);
    }
}
