namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;

    public class GraphContextFactory : IGraphContextFactory
    {
        public IGraphContext Create(
            IJournalViewModel journal,
            IFabricContext fabricContext,
            IDocumentViewModelProvider documentViewModelProvider)
        {
            var container = new Container();

            container.Register(() => journal);
            container.Register(() => fabricContext);
            container.Register(() => documentViewModelProvider);
            

            var graphScaffolding = new GraphScaffolding();
            graphScaffolding.Register(container);


            graphScaffolding.Initialize(container);
            return container.GetInstance<IGraphContext>();
        }
    }
}