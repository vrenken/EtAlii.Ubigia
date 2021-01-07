namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    public class GraphContextFactory : Factory<IGraphContext, GraphContextConfiguration, IGraphContextExtension>
    {
        protected override IScaffolding[] CreateScaffoldings(GraphContextConfiguration configuration)
        {
            // Let's ensure that the function handler configuration is in fact valid.
            var functionHandlersProvider = configuration.FunctionHandlersProvider;
            var functionHandlerValidator = new FunctionHandlerValidator();
            functionHandlerValidator.Validate(functionHandlersProvider);

            // Let's ensure that the root handler configuration is in fact valid.
            var rootHandlerMappersProvider = configuration.RootHandlerMappersProvider;
            var rootHandlerMapperValidator = new RootHandlerMapperValidator();
            rootHandlerMapperValidator.Validate(rootHandlerMappersProvider);

            return System.Array.Empty<IScaffolding>();
            //new GraphContextScaffolding(configuration),
            //new ScriptsScaffolding(functionHandlersProvider, rootHandlerMappersProvider),

        }
    }
}
