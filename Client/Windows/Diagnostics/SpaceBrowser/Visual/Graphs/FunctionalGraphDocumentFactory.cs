﻿namespace EtAlii.Ubigia.Client.Windows.Diagnostics
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
            IJournalViewModel journal)
        {
            var container = new Container();
            container.ResolveUnregisteredType += (sender, args) => { throw new InvalidOperationException("Unregistered type found: " + args.UnregisteredServiceType.Name); };

            new DiagnosticsScaffolding().Register(container, diagnostics, logger, logFactory);
            new StructureScaffolding().Register(container);
            new GraphScaffolding().Register(container);

            container.Register<DocumentViewModelProvider>(Lifestyle.Singleton);
            container.Register<IJournalViewModel>(() => journal, Lifestyle.Singleton);
            container.Register<GraphDocumentViewModel>(Lifestyle.Singleton);

            container.Register<IFabricContext>(() => fabricContext, Lifestyle.Singleton);
            container.Register<IDataContext>(() => dataContext, Lifestyle.Singleton);

            var documentViewModel = container.GetInstance<GraphDocumentViewModel>();
            var documentViewModelService = container.GetInstance<DocumentViewModelProvider>();
            documentViewModelService.SetInstance(documentViewModel);
            return documentViewModel;
        }
    }
}
