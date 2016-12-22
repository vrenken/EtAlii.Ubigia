namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using System;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;

    public class SequentialDocumentFactory : IDocumentFactory
    {
        public IDocumentViewModel Create(
            IDataContext dataContext,
            ILogicalContext logicalContext,
            IFabricContext fabricContext,
            IDataConnection connection,
            IDiagnosticsConfiguration diagnostics,
            ILogger logger,
            ILogFactory logFactory,
            IJournal journal)
        {
            var container = new Container();
            container.ResolveUnregisteredType += (sender, args) => { throw new InvalidOperationException("Unregistered type found: " + args.UnregisteredServiceType.Name); };

            new DiagnosticsScaffolding().Register(container, diagnostics, logger, logFactory);
            new StructureScaffolding().Register(container);
            new GraphScaffolding().Register(container);

            container.Register<IFabricContext>(() => fabricContext, Lifestyle.Singleton);
            container.Register<IDataContext>(() => dataContext, Lifestyle.Singleton);

            container.Register<DocumentViewModelProvider>(Lifestyle.Singleton);
            container.Register<IJournal>(() => journal, Lifestyle.Singleton);
            container.Register<SequentialDocumentViewModel>(Lifestyle.Singleton);

            var documentViewModel = container.GetInstance<SequentialDocumentViewModel>();
            var documentViewModelService = container.GetInstance<DocumentViewModelProvider>();
            documentViewModelService.SetInstance(documentViewModel);
            return documentViewModel;
        }
    }
}
