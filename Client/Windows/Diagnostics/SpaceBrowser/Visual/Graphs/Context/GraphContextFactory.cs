namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;

    public class GraphContextFactory : IGraphContextFactory
    {
        public IGraphContext Create(
            ILogger logger,
            IJournalViewModel journal,
            IFabricContext fabricContext,
            IDocumentViewModelProvider documentViewModelProvider)
        {
            var container = new Container();

            container.Register<ILogger>(() => logger, Lifestyle.Singleton);
            container.Register<IJournalViewModel>(() => journal, Lifestyle.Singleton);
            container.Register<IFabricContext>(() => fabricContext, Lifestyle.Singleton);
            container.Register<IDocumentViewModelProvider>(() => documentViewModelProvider, Lifestyle.Singleton);
            

            var graphScaffolding = new GraphScaffolding();
            graphScaffolding.Register(container);


            graphScaffolding.Initialize(container);
            return container.GetInstance<IGraphContext>();
        }
    }
}