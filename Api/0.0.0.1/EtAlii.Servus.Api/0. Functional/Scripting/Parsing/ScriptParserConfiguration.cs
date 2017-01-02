namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Logical;

    internal class ScriptParserConfiguration : IScriptParserConfiguration
    {
        public IScriptParserExtension[] Extensions { get { return _extensions; } }
        private IScriptParserExtension[] _extensions;

        public IFunctionHandlersProvider FunctionHandlersProvider { get { return _functionHandlersProvider; } }
        private IFunctionHandlersProvider _functionHandlersProvider;

        private ILogicalContextConfiguration _parentConfiguration;

        public ScriptParserConfiguration()
        {
            _extensions = new IScriptParserExtension[0];
            _functionHandlersProvider = Functional.FunctionHandlersProvider.Empty;
        }


        public IScriptParserConfiguration Use(IFunctionHandlersProvider functionHandlersProvider)
        {
            _functionHandlersProvider = functionHandlersProvider;
            return this;
        }

        public IScriptParserConfiguration Use(IScriptParserExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException(nameof(extensions));
            }

            _extensions = extensions
                .Concat(_extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public IScriptParserConfiguration Use(ILogicalContextConfiguration parentConfiguration)
        {
            if (parentConfiguration == null)
            {
                throw new ArgumentException(nameof(parentConfiguration));
            }
            _parentConfiguration = parentConfiguration;
            return this;
        }

    }
}