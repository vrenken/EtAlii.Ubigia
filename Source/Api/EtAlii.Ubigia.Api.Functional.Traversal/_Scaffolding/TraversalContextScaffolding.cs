namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class TraversalContextScaffolding : IScaffolding
    {
        private readonly TraversalContextConfiguration _configuration;

        public TraversalContextScaffolding(TraversalContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<ITraversalContextConfiguration>(() => _configuration);
            container.Register(() => new LogicalContextFactory().Create(_configuration));
        }
    }
}
