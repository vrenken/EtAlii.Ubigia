namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class TemporalDocumentFactory : ITemporalDocumentFactory
    {
        public IDocumentViewModel Create(
            IDataContext dataContext,
            ILogicalContext logicalContext,
            IFabricContext fabricContext,
            IDataConnection connection,
            IDiagnosticsConfiguration diagnostics,
            ILogger logger,
            ILogFactory logFactory,
            IJournalViewModel journal,
            IGraphContextFactory graphContextFactory)
        {
            var container = new Container();

            new DiagnosticsScaffolding().Register(container, diagnostics, logger, logFactory);
            new StructureScaffolding().Register(container);

            container.Register(() => fabricContext);
            container.Register(() => dataContext);
            container.Register(() =>
            {
                var dvmp = container.GetInstance<IDocumentViewModelProvider>();
                return graphContextFactory.Create(logger, journal, fabricContext, dvmp);
            });

            container.Register<IDocumentViewModelProvider, DocumentViewModelProvider>();
            container.Register(() => journal);
            container.Register<ITemporalDocumentViewModel, TemporalDocumentViewModel>();
            container.Register<IGraphButtonsViewModel, GraphButtonsViewModel>();
            container.Register<IGraphContextMenuViewModel, GraphContextMenuViewModel>();

            var documentViewModel = container.GetInstance<ITemporalDocumentViewModel>();
            var documentViewModelProvider = container.GetInstance<IDocumentViewModelProvider>();
            documentViewModelProvider.SetInstance(documentViewModel);

            return documentViewModel;
        }
    }
}
