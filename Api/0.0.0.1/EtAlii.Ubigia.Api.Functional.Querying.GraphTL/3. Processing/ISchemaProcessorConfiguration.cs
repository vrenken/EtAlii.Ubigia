namespace EtAlii.Ubigia.Api.Functional 
{
    public interface ISchemaProcessorConfiguration : IConfiguration
    {
        ISchemaScope SchemaScope { get; }

        IGraphSLScriptContext ScriptContext { get; }

        SchemaProcessorConfiguration Use(ISchemaScope scope);
        SchemaProcessorConfiguration Use(IGraphSLScriptContext scriptContext);
    }
}