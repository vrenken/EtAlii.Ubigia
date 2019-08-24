namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.MicroContainer;

    public class GraphScriptLanguageDocumentFactory : IGraphScriptLanguageDocumentFactory
    {
        public IDocumentViewModel Create(IDocumentContext documentContext)
        {
            var container = new Container();

            new DiagnosticsScaffolding().Register(container, documentContext.Diagnostics, documentContext.Logger, documentContext.LogFactory);
            new StructureScaffolding().Register(container);

            container.Register(() => documentContext.FabricContext);
            container.Register(() => documentContext.ScriptContext);
            container.Register(() =>
            {
                var dvmp = container.GetInstance<IDocumentViewModelProvider>();
                return documentContext.GraphContextFactory.Create(documentContext.Logger, documentContext.Journal, documentContext.FabricContext, dvmp);
            });

            container.Register<IDocumentViewModelProvider, DocumentViewModelProvider>();
            container.Register<IGraphScriptLanguageViewModel, GraphScriptLanguageViewModel>();
            container.Register(() => documentContext.Journal);

            container.Register<IScriptButtonsViewModel, ScriptButtonsViewModel>();

            container.Register<IParseGraphScriptLanguageUnitOfworkHandler, ParseGraphScriptLanguageUnitOfworkHandler>();
            container.Register<IProcessGraphScriptLanguageUnitOfworkHandler, ProcessGraphScriptLanguageUnitOfworkHandler>();
            container.Register<IErrorWriter, ErrorWriter>();
            container.Register<IStatusWriter, StatusWriter>();
            container.Register<IOutputGraphScriptLanguageProcessingSubscription, OutputGraphScriptLanguageProcessingSubscription>();
            container.Register<IStatusGraphScriptLanguageProcessingSubscription, StatusGraphScriptLanguageProcessingSubscription>();
            container.Register<IDiagnosticsGraphScriptLanguageProcessingSubscription, DiagnosticsGraphScriptLanguageProcessingSubscription>();


            container.Register<ITextTemplateQueryHandler, TextTemplateQueryHandler>();

            container.Register<IResultFactory, ResultFactory>();
            container.Register<IMultiResultFactory, MultiResultFactory>();

            var documentViewModel = container.GetInstance<IGraphScriptLanguageViewModel>();
            var documentViewModelProvider = container.GetInstance<IDocumentViewModelProvider>();
            documentViewModelProvider.SetInstance(documentViewModel);

            return documentViewModel;
        }
    }
}
