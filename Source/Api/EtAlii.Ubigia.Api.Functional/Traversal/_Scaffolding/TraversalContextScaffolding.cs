// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class TraversalContextScaffolding : IScaffolding
    {
        private readonly IFunctionHandlersProvider _functionHandlersProvider;
        private readonly IRootHandlerMappersProvider _rootHandlerMappersProvider;
        private readonly Func<TraversalParserConfiguration> _parserConfigurationProvider;
        private readonly Func<TraversalProcessorConfiguration> _processorConfigurationProvider;
        private readonly FunctionalContextConfiguration _configuration;

        public TraversalContextScaffolding(
            FunctionalContextConfiguration configuration,
            Func<TraversalParserConfiguration> parserConfigurationProvider,
            Func<TraversalProcessorConfiguration> processorConfigurationProvider,
            IFunctionHandlersProvider functionHandlersProvider,
            IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            _configuration = configuration;
            _functionHandlersProvider = functionHandlersProvider;
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
            _parserConfigurationProvider = parserConfigurationProvider;
            _processorConfigurationProvider = processorConfigurationProvider;
        }

        public void Register(Container container)
        {
            container.Register<ITraversalContext>(() =>
            {
                var scriptParserFactory = new ScriptParserFactory();
                var scriptProcessorFactory = new ScriptProcessorFactory();
                var logicalContext = new LogicalContextFactory().Create(_configuration);
                return new TraversalContext(_parserConfigurationProvider, _processorConfigurationProvider, _functionHandlersProvider, _rootHandlerMappersProvider, scriptProcessorFactory, scriptParserFactory, logicalContext);
            });
        }
    }
}
