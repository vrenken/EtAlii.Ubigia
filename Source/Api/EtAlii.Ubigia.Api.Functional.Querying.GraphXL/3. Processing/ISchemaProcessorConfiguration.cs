namespace EtAlii.Ubigia.Api.Functional 
{
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public interface ISchemaProcessorConfiguration : IConfiguration
    {
        ISchemaScope SchemaScope { get; }

        IGraphSLScriptContext ScriptContext { get; }

        SchemaProcessorConfiguration Use(ISchemaScope scope);
        SchemaProcessorConfiguration Use(IGraphSLScriptContext scriptContext);
    }
}