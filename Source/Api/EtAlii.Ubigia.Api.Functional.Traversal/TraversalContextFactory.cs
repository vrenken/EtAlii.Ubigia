namespace EtAlii.Ubigia.Api.Functional.Traversal
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

            if (configuration.ScriptParserFactory == null)
            {
                throw new InvalidOperationException($"No {nameof(configuration.ScriptParserFactory)} specified");
            }

            var scriptParserFactoryProvider = configuration.ScriptParserFactory;

            if (configuration.ScriptProcessorFactory == null)
            {
                throw new InvalidOperationException($"No {nameof(configuration.ScriptProcessorFactory)} specified");
            }

            var scriptProcessorFactoryProvider = configuration.ScriptProcessorFactory;

            if (configuration.PathParserFactory == null)
            {
                throw new InvalidOperationException($"No {nameof(configuration.PathParserFactory)} specified");
            }

            var pathParserFactoryProvider = configuration.PathParserFactory;

            return new IScaffolding[]
            {
                new TraversalContextScaffolding(configuration, functionHandlersProvider, rootHandlerMappersProvider, scriptParserFactoryProvider, scriptProcessorFactoryProvider, pathParserFactoryProvider),
            };
        }
    }
}
