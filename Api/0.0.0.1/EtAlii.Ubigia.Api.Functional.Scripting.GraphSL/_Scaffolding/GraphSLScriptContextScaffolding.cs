namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    internal class GraphSLScriptContextScaffolding : IScaffolding
    {
        private readonly GraphSLScriptContextConfiguration _configuration;

        public GraphSLScriptContextScaffolding(GraphSLScriptContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IGraphSLScriptContextConfiguration>(() => _configuration);
            container.Register(() => new LogicalContextFactory().Create(_configuration));
        }
    }
}
