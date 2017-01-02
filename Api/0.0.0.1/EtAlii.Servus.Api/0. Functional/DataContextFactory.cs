namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    public class DataContextFactory
    {
        public IDataContext Create(IDataContextConfiguration configuration)
        {
            // Let's ensure that the function handler configuration is in fact valid. 
            var validator = new FunctionHandlerValidator();

            var functionHandlersProvider = configuration.FunctionHandlersProvider;
            validator.Validate(functionHandlersProvider);

            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new ContextScaffolding(configuration),
                new ScriptsScaffolding(functionHandlersProvider),
                new IndexSetScaffolding(),
                new NodeSetScaffolding(),
                new RootSetScaffolding(),
                new ScriptSetScaffolding(),
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
