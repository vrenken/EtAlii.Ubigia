// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;
    using System;

    public class TraversalContextFactory : Factory<ITraversalContext, FunctionalContextOptions, ITraversalContextExtension>
    {
        protected override IScaffolding[] CreateScaffoldings(FunctionalContextOptions options)
        {
            // Let's ensure that the function handler configuration is in fact valid.
            var functionHandlersProvider = options.FunctionHandlersProvider;
            var functionHandlerValidator = new FunctionHandlerValidator();
            functionHandlerValidator.Validate(functionHandlersProvider);

            // Let's ensure that the root handler configuration is in fact valid.
            var rootHandlerMappersProvider = options.RootHandlerMappersProvider;
            var rootHandlerMapperValidator = new RootHandlerMapperValidator();
            rootHandlerMapperValidator.Validate(rootHandlerMappersProvider);

            if (options.ParserOptions == null)
            {
                throw new InvalidOperationException($"No {nameof(options.ParserOptions)} specified");
            }

            if (options.ProcessorOptionsProvider == null)
            {
                throw new InvalidOperationException($"No {nameof(options.ProcessorOptionsProvider)} specified");
            }

            var processorOptionsProvider = options.ProcessorOptionsProvider;

            return new IScaffolding[]
            {
                new TraversalContextScaffolding(options, processorOptionsProvider, functionHandlersProvider, rootHandlerMappersProvider),
            };
        }
    }
}
