namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ContextScaffolding : IScaffolding
    {
        private readonly IDataContextConfiguration _configuration;

        public ContextScaffolding(IDataContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register(() => _configuration);
            container.Register(() => _configuration.LogicalContext);

            container.Register<IDataContext, DataContext>();
            container.Register<IScriptsSet, ScriptsSet>();
        }
    }
}
