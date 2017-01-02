namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.Servus.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;
    using Remotion.Linq.Parsing.Structure;

    internal class ContextScaffolding : IScaffolding
    {
        private readonly IDataContextConfiguration _configuration;

        public ContextScaffolding(IDataContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IDataContextConfiguration>(() => _configuration);
            container.Register<ILogicalContext>(() => _configuration.LogicalContext);

            container.Register<IDataContext, DataContext>();
            container.Register<IQueryParser, QueryParser>(QueryParser.CreateDefault);
            container.Register<IChangeTracker, ChangeTracker>();
        }
    }
}
