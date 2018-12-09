namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class GraphQueryLanguageDocumentFactory : IGraphQueryLanguageDocumentFactory
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

            container.Register(() => fabricContext);
            container.Register(() => dataContext);
            container.Register<IGraphSLScriptContext>(() => graphSlScriptContext);
            container.Register(() =>
            {
                var dvmp = container.GetInstance<IDocumentViewModelProvider>();
                return graphContextFactory.Create(logger, journal, fabricContext, dvmp);
            });

            container.Register<IDocumentViewModelProvider, DocumentViewModelProvider>();
            container.Register<IGraphQueryLanguageViewModel, GraphQueryLanguageViewModel>();
            container.Register(() => journal);

//            container.Register<IQueryButtonsViewModel, QueryButtonsViewModel>();

            container.Register<IParseGraphQueryLanguageUnitOfworkHandler, ParseGraphQueryLanguageUnitOfworkHandler>();
            container.Register<IProcessGraphQueryLanguageUnitOfworkHandler, ProcessGraphQueryLanguageUnitOfworkHandler>();
            container.Register<IErrorWriter, ErrorWriter>();
            container.Register<IStatusWriter, StatusWriter>();
            container.Register<IOutputGraphQueryLanguageProcessingSubscription, OutputGraphQueryLanguageProcessingSubscription>();
            container.Register<IStatusGraphQueryLanguageProcessingSubscription, StatusGraphQueryLanguageProcessingSubscription>();
            container.Register<IDiagnosticsGraphQueryLanguageProcessingSubscription, DiagnosticsGraphQueryLanguageProcessingSubscription>();


            container.Register<ITextTemplateQueryHandler, TextTemplateQueryHandler>();

            container.Register<IResultFactory, ResultFactory>();
            container.Register<IMultiResultFactory, MultiResultFactory>();

            var documentViewModel = container.GetInstance<IGraphQueryLanguageViewModel>();
            var documentViewModelProvider = container.GetInstance<IDocumentViewModelProvider>();
            documentViewModelProvider.SetInstance(documentViewModel);

            return documentViewModel;
        }
    }
}
