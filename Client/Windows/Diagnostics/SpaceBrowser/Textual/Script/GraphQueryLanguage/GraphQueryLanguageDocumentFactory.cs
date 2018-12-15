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
        public IDocumentViewModel Create(IDocumentContext documentContext)
        {
            var container = new Container();

            new DiagnosticsScaffolding().Register(container, documentContext.Diagnostics, documentContext.Logger, documentContext.LogFactory);
            new StructureScaffolding().Register(container);

            container.Register(() => documentContext.FabricContext);
            container.Register<IGraphQLQueryContext>(() => documentContext.QueryContext);
            
            container.Register(() =>
            {
                var dvmp = container.GetInstance<IDocumentViewModelProvider>();
                return documentContext.GraphContextFactory.Create(documentContext.Logger, documentContext.Journal, documentContext.FabricContext, dvmp);
            });

            container.Register<IDocumentViewModelProvider, DocumentViewModelProvider>();
            container.Register<IGraphQueryLanguageViewModel, GraphQueryLanguageViewModel>();
            container.Register(() => documentContext.Journal);

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
