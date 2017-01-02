namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public interface IScriptProcessorConfiguration
    {
        IScriptScope ScriptScope { get; }

        ILogicalContext LogicalContext { get; }

        IScriptProcessorExtension[] Extensions { get; }
        bool CachingEnabled { get; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; }
        IFunctionHandlersProvider FunctionHandlersProvider { get; }

        IScriptProcessorConfiguration Use(IScriptScope scope);
        IScriptProcessorConfiguration Use(ILogicalContext logicalContext);

        IScriptProcessorConfiguration Use(IScriptProcessorExtension[] extensions);
        IScriptProcessorConfiguration UseCaching(bool cachingEnabled);
        IScriptProcessorConfiguration Use(IRootHandlerMappersProvider rootHandlerMappersProvider);
        IScriptProcessorConfiguration Use(IFunctionHandlersProvider functionHandlersProvider);
    }
}