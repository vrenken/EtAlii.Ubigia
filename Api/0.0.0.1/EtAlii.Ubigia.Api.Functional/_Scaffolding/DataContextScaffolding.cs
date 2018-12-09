namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class DataContextScaffolding : IScaffolding
    {
        private readonly IDataContextConfiguration _configuration;

        public DataContextScaffolding(IDataContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register(() => _configuration);
            container.Register(() => _configuration.LogicalContext);

            container.Register<IDataContext, DataContext>();
            //container.Register<IScriptsSet, ScriptsSet>();
        }
    }
}
