namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;
    using System;

    public class TraversalScriptContextFactory : Factory<ITraversalScriptContext, TraversalScriptContextConfiguration, ITraversalScriptContextExtension>
    {
        protected override IScaffolding[] CreateScaffoldings(TraversalScriptContextConfiguration configuration)
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
                throw new InvalidOperationException("No ScriptParserFactory specified");
            }

            var scriptParserFactoryProvider = configuration.ScriptParserFactory;

            if (configuration.ScriptProcessorFactory == null)
            {
                throw new InvalidOperationException("No ScriptProcessorFactory specified");
            }

            var scriptProcessorFactoryProvider = configuration.ScriptProcessorFactory;

            return new IScaffolding[]
            {
                new TraversalScriptContextScaffolding(configuration),
                new ScriptsScaffolding(functionHandlersProvider, rootHandlerMappersProvider, scriptParserFactoryProvider, scriptProcessorFactoryProvider),
            };
        }
    }
}
