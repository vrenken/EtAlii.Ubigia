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

    public class ScriptDocumentFactory : IScriptDocumentFactory
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
            container.Register<IScriptViewModel, ScriptViewModel>(Lifestyle.Singleton);
            container.Register<IJournalViewModel>(() => journal, Lifestyle.Singleton);

            container.Register<IScriptButtonsViewModel, ScriptButtonsViewModel>(Lifestyle.Singleton);

            container.Register<IParseScriptUnitOfworkHandler, ParseScriptUnitOfworkHandler>(Lifestyle.Singleton);
            container.Register<IProcessScriptUnitOfworkHandler, ProcessScriptUnitOfworkHandler>(Lifestyle.Singleton);
            container.Register<IErrorWriter, ErrorWriter>(Lifestyle.Singleton);
            container.Register<IStatusWriter, StatusWriter>(Lifestyle.Singleton);
            container.Register<IOutputScriptProcessingSubscription, OutputScriptProcessingSubscription>(Lifestyle.Singleton);
            container.Register<IStatusScriptProcessingSubscription, StatusScriptProcessingSubscription>(Lifestyle.Singleton);
            container.Register<IDiagnosticsScriptProcessingSubscription, DiagnosticsScriptProcessingSubscription>(Lifestyle.Singleton);


            container.Register<ITextTemplateQueryHandler, TextTemplateQueryHandler>(Lifestyle.Singleton);

            container.Register<IResultFactory, ResultFactory>(Lifestyle.Singleton);
            container.Register<IMultiResultFactory, MultiResultFactory>(Lifestyle.Singleton);

            var documentViewModel = container.GetInstance<IScriptViewModel>();
            var documentViewModelProvider = container.GetInstance<IDocumentViewModelProvider>();
            documentViewModelProvider.SetInstance(documentViewModel);

            return documentViewModel;
        }
    }
}
