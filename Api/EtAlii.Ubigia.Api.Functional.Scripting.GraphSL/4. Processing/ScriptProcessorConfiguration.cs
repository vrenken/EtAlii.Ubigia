﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using EtAlii.Ubigia.Api.Logical;

    public class ScriptProcessorConfiguration : Configuration, IScriptProcessorConfiguration
    {
        public IScriptScope ScriptScope { get; private set; }

        public ILogicalContext LogicalContext { get; private set; }

        public bool CachingEnabled { get; private set; }

        public IRootHandlerMappersProvider RootHandlerMappersProvider { get; private set; }

        public IFunctionHandlersProvider FunctionHandlersProvider { get; private set; }

        public ScriptProcessorConfiguration()
        {
            CachingEnabled = true;
            RootHandlerMappersProvider = EtAlii.Ubigia.Api.Functional.Scripting.RootHandlerMappersProvider.Empty;
            FunctionHandlersProvider = EtAlii.Ubigia.Api.Functional.Scripting.FunctionHandlersProvider.Empty;
        }


        public ScriptProcessorConfiguration Use(IScriptScope scope)
        {
            ScriptScope = scope ?? throw new ArgumentException(nameof(scope));
            return this;
        }

        public ScriptProcessorConfiguration Use(ILogicalContext logicalContext)
        {
            LogicalContext = logicalContext ?? throw new ArgumentException(nameof(logicalContext));
            return this;
        }

        public ScriptProcessorConfiguration Use(IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            RootHandlerMappersProvider = rootHandlerMappersProvider;
            return this;
        }

        public ScriptProcessorConfiguration Use(IFunctionHandlersProvider functionHandlersProvider)
        {
            FunctionHandlersProvider = functionHandlersProvider;
            return this;
        }

        public ScriptProcessorConfiguration UseCaching(bool cachingEnabled)
        {
            CachingEnabled = cachingEnabled;
            return this;
        }
    }
}