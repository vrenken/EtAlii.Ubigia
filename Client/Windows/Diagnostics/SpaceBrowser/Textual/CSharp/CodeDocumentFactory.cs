namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.xTechnology.MicroContainer;

    public class CodeDocumentFactory : ICodeDocumentFactory
    {
        public IDocumentViewModel Create(IDocumentContext documentContext)
        {
            var container = new Container();

            new DiagnosticsScaffolding().Register(container, documentContext.Diagnostics, documentContext.Logger, documentContext.LogFactory);
            new StructureScaffolding().Register(container);

            container.Register(() => documentContext.FabricContext);
            container.Register<IGraphSLScriptContext>(() => documentContext.ScriptContext);
            container.Register(() =>
            {
                var dvmp = container.GetInstance<IDocumentViewModelProvider>();
                return documentContext.GraphContextFactory.Create(documentContext.Logger, documentContext.Journal, documentContext.FabricContext, dvmp);
            });

            container.Register<IDocumentViewModelProvider, DocumentViewModelProvider>();
            container.Register<ICodeViewModel, CodeViewModel>();
            container.Register(() => documentContext.Journal);

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
