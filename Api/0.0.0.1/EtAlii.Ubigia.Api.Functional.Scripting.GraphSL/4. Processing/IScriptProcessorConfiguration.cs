namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public interface IScriptProcessorConfiguration : IConfiguration
    {
        IScriptScope ScriptScope { get; }

        ILogicalContext LogicalContext { get; }

        bool CachingEnabled { get; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; }
        IFunctionHandlersProvider FunctionHandlersProvider { get; }

        ScriptProcessorConfiguration Use(IScriptScope scope);
        ScriptProcessorConfiguration Use(ILogicalContext logicalContext);
        ScriptProcessorConfiguration UseCaching(bool cachingEnabled);
        ScriptProcessorConfiguration Use(IRootHandlerMappersProvider rootHandlerMappersProvider);
        ScriptProcessorConfiguration Use(IFunctionHandlersProvider functionHandlersProvider);
    }
}