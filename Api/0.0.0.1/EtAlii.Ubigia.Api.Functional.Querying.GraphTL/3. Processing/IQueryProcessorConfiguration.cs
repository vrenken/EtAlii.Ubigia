namespace EtAlii.Ubigia.Api.Functional 
{
    public interface IQueryProcessorConfiguration : IConfiguration
    {
        IQueryScope QueryScope { get; }

        IGraphSLScriptContext ScriptContext { get; }

        QueryProcessorConfiguration Use(IQueryScope scope);
        QueryProcessorConfiguration Use(IGraphSLScriptContext scriptContext);
    }
}