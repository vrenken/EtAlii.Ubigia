// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class TraversalContextScaffolding : IScaffolding
    {
        private readonly IFunctionHandlersProvider _functionHandlersProvider;
        private readonly IRootHandlerMappersProvider _rootHandlerMappersProvider;
        private readonly Func<TraversalParserOptions> _parserOptionsProvider;
        private readonly Func<TraversalProcessorOptions> _processorOptionsProvider;
        private readonly FunctionalContextOptions _options;

        public TraversalContextScaffolding(
            FunctionalContextOptions options,
            Func<TraversalParserOptions> parserOptionsProvider,
            Func<TraversalProcessorOptions> processorOptionsProvider,
            IFunctionHandlersProvider functionHandlersProvider,
            IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            _options = options;
            _functionHandlersProvider = functionHandlersProvider;
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
            _parserOptionsProvider = parserOptionsProvider;
            _processorOptionsProvider = processorOptionsProvider;
        }

        public void Register(Container container)
        {
            container.Register<ITraversalContext>(() =>
            {
                var scriptParserFactory = new ScriptParserFactory();
                var scriptProcessorFactory = new ScriptProcessorFactory();
                var logicalContext = new LogicalContextFactory().Create(_options);
                return new TraversalContext(_parserOptionsProvider, _processorOptionsProvider, _functionHandlersProvider, _rootHandlerMappersProvider, scriptProcessorFactory, scriptParserFactory, logicalContext);
            });
        }
    }
}
