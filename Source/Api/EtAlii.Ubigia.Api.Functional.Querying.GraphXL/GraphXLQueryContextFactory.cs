namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.xTechnology.MicroContainer;

    public class GraphXLQueryContextFactory : Factory<IGraphXLContext, GraphXLQueryContextConfiguration, IGraphXLQueryContextExtension>
    {
        protected override IScaffolding[] CreateScaffoldings(GraphXLQueryContextConfiguration configuration)
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
                new GraphXLQueryContextScaffolding(configuration),

                //new ScriptsScaffolding(functionHandlersProvider, rootHandlerMappersProvider),
            };
        }
    }
}
