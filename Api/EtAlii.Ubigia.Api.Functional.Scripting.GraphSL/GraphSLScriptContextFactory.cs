namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using EtAlii.xTechnology.MicroContainer;

    public class GraphSLScriptContextFactory : Factory<IGraphSLScriptContext, GraphSLScriptContextConfiguration, IGraphSLScriptContextExtension>
    {
        protected override IScaffolding[] CreateScaffoldings(GraphSLScriptContextConfiguration configuration)
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
                new GraphSLScriptContextScaffolding(configuration),
                new ScriptsScaffolding(functionHandlersProvider, rootHandlerMappersProvider),
            };
        }
    }
}