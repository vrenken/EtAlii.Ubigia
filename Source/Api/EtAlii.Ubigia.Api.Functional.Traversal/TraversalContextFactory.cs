﻿namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;
    using System;

    public class TraversalContextFactory : Factory<ITraversalContext, TraversalContextConfiguration, ITraversalContextExtension>
    {
        protected override IScaffolding[] CreateScaffoldings(TraversalContextConfiguration configuration)
        {
            // Let's ensure that the function handler configuration is in fact valid.
            var functionHandlersProvider = configuration.FunctionHandlersProvider;
            var functionHandlerValidator = new FunctionHandlerValidator();
            functionHandlerValidator.Validate(functionHandlersProvider);

            // Let's ensure that the root handler configuration is in fact valid.
            var rootHandlerMappersProvider = configuration.RootHandlerMappersProvider;
            var rootHandlerMapperValidator = new RootHandlerMapperValidator();
            rootHandlerMapperValidator.Validate(rootHandlerMappersProvider);

            if (configuration.ParserConfigurationProvider == null)
            {
                throw new InvalidOperationException($"No {nameof(configuration.ParserConfigurationProvider)} specified");
            }

            var parserConfigurationProvider = configuration.ParserConfigurationProvider;

            if (configuration.ProcessorConfigurationProvider == null)
            {
                throw new InvalidOperationException($"No {nameof(configuration.ProcessorConfigurationProvider)} specified");
            }

            var processorConfigurationProvider = configuration.ProcessorConfigurationProvider;

            return new IScaffolding[]
            {
                new TraversalContextScaffolding(configuration, parserConfigurationProvider, processorConfigurationProvider, functionHandlersProvider, rootHandlerMappersProvider),
            };
        }
    }
}
