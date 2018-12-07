namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    public class DataContextFactory
    {
        public IDataContext Create(IDataContextConfiguration configuration)
        {
            // Let's ensure that the function handler configuration is in fact valid. 
            var functionHandlersProvider = configuration.FunctionHandlersProvider;
            var functionHandlerValidator = new FunctionHandlerValidator();
            functionHandlerValidator.Validate(functionHandlersProvider);

            // Let's ensure that the root handler configuration is in fact valid. 
            var rootHandlerMappersProvider = configuration.RootHandlerMappersProvider;
            var rootHandlerMapperValidator = new RootHandlerMapperValidator();
            rootHandlerMapperValidator.Validate(rootHandlerMappersProvider);

            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new ContextScaffolding(configuration),
                new ScriptsScaffolding(functionHandlersProvider, rootHandlerMappersProvider),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.Extensions)
            {
                extension.Initialize(container);
            }

            return container.GetInstance<IDataContext>();
        }
    }
}
