// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class TraversalContextScaffolding : IScaffolding
    {
        private readonly FunctionalOptions _options;

        public TraversalContextScaffolding(
            FunctionalOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IScriptParserFactory, ScriptParserFactory>();
            container.Register<IScriptProcessorFactory, ScriptProcessorFactory>();
            container.Register<ITraversalContext>(services =>
            {
                var scriptParserFactory = services.GetInstance<IScriptParserFactory>();
                var scriptProcessorFactory = services.GetInstance<IScriptProcessorFactory>();
                return new TraversalContext(_options, scriptProcessorFactory, scriptParserFactory);
            });
        }
    }
}
