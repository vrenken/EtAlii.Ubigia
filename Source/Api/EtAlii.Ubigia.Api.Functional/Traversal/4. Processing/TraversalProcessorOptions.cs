// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.Ubigia.Api.Logical;
    using Microsoft.Extensions.Configuration;

    public class TraversalProcessorOptions : ConfigurationBase, ITraversalProcessorOptions
    {
        public IConfiguration ConfigurationRoot { get; }

        public IScriptScope ScriptScope { get; private set; }

        public ILogicalContext LogicalContext { get; private set; }

        public bool CachingEnabled { get; private set; }

        public IRootHandlerMappersProvider RootHandlerMappersProvider { get; private set; }

        public IFunctionHandlersProvider FunctionHandlersProvider { get; private set; }

        public TraversalProcessorOptions(IConfiguration configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
            CachingEnabled = true;
            RootHandlerMappersProvider = EtAlii.Ubigia.Api.Functional.Traversal.RootHandlerMappersProvider.Empty;
            FunctionHandlersProvider = EtAlii.Ubigia.Api.Functional.Traversal.FunctionHandlersProvider.Empty;
        }

        public TraversalProcessorOptions Use(IScriptScope scope)
        {
            ScriptScope = scope ?? throw new ArgumentException("No script scope specified", nameof(scope));
            return this;
        }

        public TraversalProcessorOptions Use(ILogicalContext logicalContext)
        {
            LogicalContext = logicalContext ?? throw new ArgumentException("No logical context specified", nameof(logicalContext));

            return UseCaching(logicalContext.Options.CachingEnabled);
        }

        public TraversalProcessorOptions Use(IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            RootHandlerMappersProvider = rootHandlerMappersProvider;
            return this;
        }

        public TraversalProcessorOptions Use(IFunctionHandlersProvider functionHandlersProvider)
        {
            FunctionHandlersProvider = functionHandlersProvider;
            return this;
        }

        public TraversalProcessorOptions UseCaching(bool cachingEnabled)
        {
            CachingEnabled = cachingEnabled;
            return this;
        }
    }
}
