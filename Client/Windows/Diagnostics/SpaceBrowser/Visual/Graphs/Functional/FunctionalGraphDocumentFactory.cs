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

    public class FunctionalGraphDocumentFactory : IFunctionalGraphDocumentFactory
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

            container.Register<IDocumentViewModelProvider, DocumentViewModelProvider>(Lifestyle.Singleton);
            container.Register<IJournalViewModel>(() => journal, Lifestyle.Singleton);
            container.Register<IFunctionalGraphDocumentViewModel, FunctionalGraphDocumentViewModel>(Lifestyle.Singleton);
            container.Register<IGraphButtonsViewModel, GraphButtonsViewModel>(Lifestyle.Singleton);
            container.Register<IGraphContextMenuViewModel, GraphContextMenuViewModel>(Lifestyle.Singleton);

            container.Register<IFabricContext>(() => fabricContext, Lifestyle.Singleton);
            container.Register<IDataContext>(() => dataContext, Lifestyle.Singleton);
            container.Register<IGraphContext>(() =>
            {
                var dvmp = container.GetInstance<IDocumentViewModelProvider>();
                return graphContextFactory.Create(logger, journal, fabricContext, dvmp);
            }, Lifestyle.Singleton);

            var documentViewModel = container.GetInstance<IFunctionalGraphDocumentViewModel>();
            var documentViewModelService = container.GetInstance<IDocumentViewModelProvider>();
            documentViewModelService.SetInstance(documentViewModel);

            return documentViewModel;
        }
    }
}
