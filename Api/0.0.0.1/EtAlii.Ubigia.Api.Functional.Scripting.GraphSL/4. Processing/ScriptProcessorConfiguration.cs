namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Logical;

    internal class ScriptProcessorConfiguration : IScriptProcessorConfiguration
    {
        public IScriptScope ScriptScope { get; private set; }

        public ILogicalContext LogicalContext { get; private set; }

        public IScriptProcessorExtension[] Extensions { get; private set; }

        public bool CachingEnabled { get; private set; }

        public IRootHandlerMappersProvider RootHandlerMappersProvider { get; private set; }

        public IFunctionHandlersProvider FunctionHandlersProvider { get; private set; }

        public ScriptProcessorConfiguration()
        {
            CachingEnabled = true;
            Extensions = new IScriptProcessorExtension[0];
            RootHandlerMappersProvider = Functional.RootHandlerMappersProvider.Empty;
            FunctionHandlersProvider = Functional.FunctionHandlersProvider.Empty;
        }


        public IScriptProcessorConfiguration Use(IScriptScope scope)
        {
            if (scope == null)
            {
                throw new ArgumentException("scope");
            }

            ScriptScope = scope;
            return this;
        }

        public IScriptProcessorConfiguration Use(ILogicalContext logicalContext)
        {
            if (logicalContext == null)
            {
                throw new ArgumentException("logicalContext");
            }

            LogicalContext = logicalContext;
            return this;
        }

        public IScriptProcessorConfiguration Use(IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            RootHandlerMappersProvider = rootHandlerMappersProvider;
            return this;
        }

        public IScriptProcessorConfiguration Use(IFunctionHandlersProvider functionHandlersProvider)
        {
            FunctionHandlersProvider = functionHandlersProvider;
            return this;
        }

        public IScriptProcessorConfiguration Use(IScriptProcessorExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException("extensions");
            }

            Extensions = extensions
                .Concat(Extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public IScriptProcessorConfiguration UseCaching(bool cachingEnabled)
        {
            CachingEnabled = cachingEnabled;
            return this;
        }
    }
}
