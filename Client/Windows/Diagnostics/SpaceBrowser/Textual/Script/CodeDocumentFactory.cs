namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;

    public class CodeDocumentFactory : ICodeDocumentFactory
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
            container.ResolveUnregisteredType += (sender, args) => { throw new InvalidOperationException("Unregistered type found: " + args.UnregisteredServiceType.Name); };

            new DiagnosticsScaffolding().Register(container, diagnostics, logger, logFactory);
            new StructureScaffolding().Register(container);

            container.Register<IFabricContext>(() => fabricContext, Lifestyle.Singleton);
            container.Register<IDataContext>(() => dataContext, Lifestyle.Singleton);
            container.Register<IGraphContext>(() =>
            {
                var dvmp = container.GetInstance<IDocumentViewModelProvider>();
                return graphContextFactory.Create(logger, journal, fabricContext, dvmp);
            }, Lifestyle.Singleton);

            container.Register<IDocumentViewModelProvider, DocumentViewModelProvider>(Lifestyle.Singleton);
            container.Register<ICodeViewModel, CodeViewModel>(Lifestyle.Singleton);
            container.Register<IJournalViewModel>(() => journal, Lifestyle.Singleton);

            container.Register<ICodeButtonsViewModel, CodeButtonsViewModel>(Lifestyle.Singleton);

            container.Register<ICompileCodeUnitOfworkHandler, CompileCodeUnitOfworkHandler>(Lifestyle.Singleton);
            container.Register<IExecuteCodeUnitOfworkHandler, ExecuteCodeUnitOfworkHandler>(Lifestyle.Singleton);
            container.Register<ICodeCompiler, CodeCompiler>(Lifestyle.Singleton);
            container.Register<ICodeCompilerResultsParser, CodeCompilerResultsParser>(Lifestyle.Singleton);

            container.Register<ITextTemplateQueryHandler, TextTemplateQueryHandler>(Lifestyle.Singleton);

            var documentViewModel = container.GetInstance<ICodeViewModel>();
            var documentViewModelProvider = container.GetInstance<IDocumentViewModelProvider>();
            documentViewModelProvider.SetInstance(documentViewModel);

            return documentViewModel;
        }
    }
}
