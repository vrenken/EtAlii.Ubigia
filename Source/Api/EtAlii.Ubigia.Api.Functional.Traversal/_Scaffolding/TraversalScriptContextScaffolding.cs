namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class TraversalScriptContextScaffolding : IScaffolding
    {
        private readonly TraversalScriptContextConfiguration _configuration;

        public TraversalScriptContextScaffolding(TraversalScriptContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<ITraversalScriptContextConfiguration>(() => _configuration);
            container.Register(() => new LogicalContextFactory().Create(_configuration));
        }
    }
}
