namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;
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
            container.Register(() => _configuration);
            container.Register(() => _configuration.LogicalContext);

            container.Register<IDataContext, DataContext>();
            container.Register<IQueryParser, QueryParser>(QueryParser.CreateDefault);
            container.Register<IChangeTracker, ChangeTracker>();
        }
    }
}
