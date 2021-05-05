namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical;

    public interface ITraversalProcessorConfiguration : IConfiguration
    {
        IScriptScope ScriptScope { get; }

        ILogicalContext LogicalContext { get; }

        bool CachingEnabled { get; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; }
        IFunctionHandlersProvider FunctionHandlersProvider { get; }

        TraversalProcessorConfiguration UseCaching(bool cachingEnabled);
        TraversalProcessorConfiguration Use(IScriptScope scope);
        TraversalProcessorConfiguration Use(ILogicalContext logicalContext);
        TraversalProcessorConfiguration Use(IRootHandlerMappersProvider rootHandlerMappersProvider);
        TraversalProcessorConfiguration Use(IFunctionHandlersProvider functionHandlersProvider);
    }
}
