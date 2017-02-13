namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class GraphContextFactory : IGraphContextFactory
    {
        public IGraphContext Create(
            ILogger logger,
            IJournalViewModel journal,
            IFabricContext fabricContext,
            IDocumentViewModelProvider documentViewModelProvider)
        {
            var container = new Container();

            container.Register<ILogger>(() => logger);
            container.Register<IJournalViewModel>(() => journal);
            container.Register<IFabricContext>(() => fabricContext);
            container.Register<IDocumentViewModelProvider>(() => documentViewModelProvider);
            

            var graphScaffolding = new GraphScaffolding();
            graphScaffolding.Register(container);


            graphScaffolding.Initialize(container);
            return container.GetInstance<IGraphContext>();
        }
    }
}