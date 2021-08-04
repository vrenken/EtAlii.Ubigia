// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical;

    public interface ITraversalProcessorOptions : IExtensible
    {
        IScriptScope ScriptScope { get; }

        ILogicalContext LogicalContext { get; }

        bool CachingEnabled { get; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; }
        IFunctionHandlersProvider FunctionHandlersProvider { get; }

        TraversalProcessorOptions UseCaching(bool cachingEnabled);
        TraversalProcessorOptions Use(IScriptScope scope);
        TraversalProcessorOptions Use(ILogicalContext logicalContext);
        TraversalProcessorOptions Use(IRootHandlerMappersProvider rootHandlerMappersProvider);
        TraversalProcessorOptions Use(IFunctionHandlersProvider functionHandlersProvider);
    }
}
