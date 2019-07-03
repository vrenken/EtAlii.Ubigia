namespace EtAlii.Ubigia.Api.Functional 
{
    using EtAlii.xTechnology.MicroContainer;

    public class GraphTLQueryContextFactory : Factory<IGraphTLQueryContext, GraphTLQueryContextConfiguration, IGraphTLQueryContextExtension>
    {
        protected override IScaffolding[] CreateScaffoldings(GraphTLQueryContextConfiguration configuration)
        {
            // Let's ensure that the function handler configuration is in fact valid. 
            var functionHandlersProvider = configuration.FunctionHandlersProvider;
            var functionHandlerValidator = new FunctionHandlerValidator();
            functionHandlerValidator.Validate(functionHandlersProvider);

            // Let's ensure that the root handler configuration is in fact valid. 
            var rootHandlerMappersProvider = configuration.RootHandlerMappersProvider;
            var rootHandlerMapperValidator = new RootHandlerMapperValidator();
            rootHandlerMapperValidator.Validate(rootHandlerMappersProvider);

            return new IScaffolding[]
            {
                new GraphTLQueryContextScaffolding(configuration),
                
                //new ScriptsScaffolding(functionHandlersProvider, rootHandlerMappersProvider),
            };
        }
    }
}