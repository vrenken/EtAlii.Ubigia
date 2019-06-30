namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class QueryProcessingScaffolding  : IScaffolding
    {
        private readonly IQueryProcessorConfiguration _configuration;

        public QueryProcessingScaffolding (IQueryProcessorConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IQueryProcessor, QueryProcessor>();

            container.Register<IQueryFragmentProcessor, QueryFragmentProcessor>();
            container.Register<IMutationFragmentProcessor, MutationFragmentProcessor>();

            container.Register<IScriptProcessingContext, ScriptProcessingContext>();
            container.Register(() => _configuration.ScriptContext);
            container.Register(() => _configuration.QueryScope);
            container.Register(() => _configuration);
        }
    }
}
