namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class GraphSLScriptContextScaffolding : IScaffolding
    {
        private readonly IGraphSLScriptContextConfiguration _configuration;

        public GraphSLScriptContextScaffolding(IGraphSLScriptContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register(() => _configuration);
            container.Register(() => _configuration.DataContext.Configuration.LogicalContext);

            container.Register<IDataContext, DataContext>();
        }
    }
}
