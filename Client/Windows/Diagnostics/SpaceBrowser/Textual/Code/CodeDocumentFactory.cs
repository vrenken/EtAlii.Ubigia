namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class CodeDocumentFactory : ICodeDocumentFactory
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
            container.Register<ICodeViewModel, CodeViewModel>();
            container.Register(() => journal);

            container.Register<ICodeButtonsViewModel, CodeButtonsViewModel>();

            container.Register<ICompileCodeUnitOfworkHandler, CompileCodeUnitOfworkHandler>();
            container.Register<IExecuteCodeUnitOfworkHandler, ExecuteCodeUnitOfworkHandler>();
            container.Register<ICodeCompiler, CodeCompiler>();
            container.Register<ICodeCompilerResultsParser, CodeCompilerResultsParser>();

            container.Register<ITextTemplateQueryHandler, TextTemplateQueryHandler>();

            var documentViewModel = container.GetInstance<ICodeViewModel>();
            var documentViewModelProvider = container.GetInstance<IDocumentViewModelProvider>();
            documentViewModelProvider.SetInstance(documentViewModel);

            return documentViewModel;
        }
    }
}
