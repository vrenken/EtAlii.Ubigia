﻿namespace EtAlii.Servus.Client.Windows.Diagnostics
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

    public class ScriptDocumentFactory : IDocumentFactory
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

            container.Register<IFabricContext>(() => fabricContext, Lifestyle.Singleton);
            container.Register<IDataContext>(() => dataContext, Lifestyle.Singleton);

            container.Register<ScriptViewModel>(Lifestyle.Singleton);
            container.Register<IJournal>(() => journal, Lifestyle.Singleton);

            container.Register<ScriptButtonsViewModel>(Lifestyle.Singleton);

            container.Register<ParseScriptUnitOfworkHandler>(Lifestyle.Singleton);
            container.Register<ProcessScriptUnitOfworkHandler>(Lifestyle.Singleton);
            container.Register<IErrorWriter, ErrorWriter>(Lifestyle.Singleton);
            container.Register<IStatusWriter, StatusWriter>(Lifestyle.Singleton);
            container.Register<IOutputScriptProcessingSubscription, OutputScriptProcessingSubscription>(Lifestyle.Singleton);
            container.Register<IStatusScriptProcessingSubscription, StatusScriptProcessingSubscription>(Lifestyle.Singleton);
            container.Register<IDiagnosticsScriptProcessingSubscription, DiagnosticsScriptProcessingSubscription>(Lifestyle.Singleton);


            container.Register<TextTemplateQueryHandler>(Lifestyle.Singleton);

            container.Register<IResultFactory, ResultFactory>(Lifestyle.Singleton);
            container.Register<IMultiResultFactory, MultiResultFactory>(Lifestyle.Singleton);

            return container.GetInstance<ScriptViewModel>();
        }
    }
}
