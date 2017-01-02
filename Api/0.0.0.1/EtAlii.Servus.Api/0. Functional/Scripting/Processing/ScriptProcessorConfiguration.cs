namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Logical;

    internal class ScriptProcessorConfiguration : IScriptProcessorConfiguration
    {
        public IScriptScope ScriptScope { get { return _scriptScope; } }
        private IScriptScope _scriptScope;

        public ILogicalContext LogicalContext { get { return _logicalContext; } }
        private ILogicalContext _logicalContext;

        public IScriptProcessorExtension[] Extensions { get { return _extensions; } }
        private IScriptProcessorExtension[] _extensions;

        public bool CachingEnabled { get { return _cachingEnabled; } }
        private bool _cachingEnabled;

        public ScriptProcessorConfiguration()
        {
            _cachingEnabled = true;
            _extensions = new IScriptProcessorExtension[0];            
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
