namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class FunctionalGraphDocumentFactory : IFunctionalGraphDocumentFactory
    {
        public IDocumentViewModel Create(
            IGraphSLScriptContext graphSlScriptContext,
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

            container.Register<IDocumentViewModelProvider, DocumentViewModelProvider>();
            container.Register(() => journal);
            container.Register<IFunctionalGraphDocumentViewModel, FunctionalGraphDocumentViewModel>();
            container.Register<IGraphButtonsViewModel, GraphButtonsViewModel>();
            container.Register<IGraphContextMenuViewModel, GraphContextMenuViewModel>();

            container.Register(() => fabricContext);
            container.Register(() => dataContext);
            container.Register<IGraphSLScriptContext>(() => graphSlScriptContext);
            container.Register(() =>
            {
                var dvmp = container.GetInstance<IDocumentViewModelProvider>();
                return graphContextFactory.Create(logger, journal, fabricContext, dvmp);
            });

            var documentViewModel = container.GetInstance<IFunctionalGraphDocumentViewModel>();
            var documentViewModelService = container.GetInstance<IDocumentViewModelProvider>();
            documentViewModelService.SetInstance(documentViewModel);

            return documentViewModel;
        }
    }
}
