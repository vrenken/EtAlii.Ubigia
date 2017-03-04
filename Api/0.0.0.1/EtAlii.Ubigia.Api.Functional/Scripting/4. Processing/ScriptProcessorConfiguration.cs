namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Logical;

    internal class ScriptProcessorConfiguration : IScriptProcessorConfiguration
    {
        public IScriptScope ScriptScope => _scriptScope;
        private IScriptScope _scriptScope;

        public ILogicalContext LogicalContext => _logicalContext;
        private ILogicalContext _logicalContext;

        public IScriptProcessorExtension[] Extensions => _extensions;
        private IScriptProcessorExtension[] _extensions;

        public bool CachingEnabled => _cachingEnabled;
        private bool _cachingEnabled;

        public IRootHandlerMappersProvider RootHandlerMappersProvider => _rootHandlerMappersProvider;
        private IRootHandlerMappersProvider _rootHandlerMappersProvider;

        public IFunctionHandlersProvider FunctionHandlersProvider => _functionHandlersProvider;
        private IFunctionHandlersProvider _functionHandlersProvider;

        public ScriptProcessorConfiguration()
        {
            _cachingEnabled = true;
            _extensions = new IScriptProcessorExtension[0];
            _rootHandlerMappersProvider = Functional.RootHandlerMappersProvider.Empty;
            _functionHandlersProvider = Functional.FunctionHandlersProvider.Empty;
        }


        public IScriptProcessorConfiguration Use(IScriptScope scope)
        {
            if (scope == null)
            {
                throw new ArgumentException("scope");
            }

            _scriptScope = scope;
            return this;
        }

        public IScriptProcessorConfiguration Use(ILogicalContext logicalContext)
        {
            if (logicalContext == null)
            {
                throw new ArgumentException("logicalContext");
            }

            _logicalContext = logicalContext;
            return this;
        }

        public IScriptProcessorConfiguration Use(IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
            return this;
        }

        public IScriptProcessorConfiguration Use(IFunctionHandlersProvider functionHandlersProvider)
        {
            _functionHandlersProvider = functionHandlersProvider;
            return this;
        }

        public IScriptProcessorConfiguration Use(IScriptProcessorExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException("extensions");
            }

            _extensions = extensions
                .Concat(_extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public IScriptProcessorConfiguration UseCaching(bool cachingEnabled)
        {
            _cachingEnabled = cachingEnabled;
            return this;
        }
    }
}
