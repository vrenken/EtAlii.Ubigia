namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    public interface IScriptProcessorConfiguration : IConfiguration<IScriptProcessorExtension, ScriptProcessorConfiguration>
    {
        IScriptScope ScriptScope { get; }

        ILogicalContext LogicalContext { get; }

        bool CachingEnabled { get; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; }
        IFunctionHandlersProvider FunctionHandlersProvider { get; }

        IScriptProcessorConfiguration Use(IScriptScope scope);
        IScriptProcessorConfiguration Use(ILogicalContext logicalContext);
        IScriptProcessorConfiguration UseCaching(bool cachingEnabled);
        IScriptProcessorConfiguration Use(IRootHandlerMappersProvider rootHandlerMappersProvider);
        IScriptProcessorConfiguration Use(IFunctionHandlersProvider functionHandlersProvider);
    }
}