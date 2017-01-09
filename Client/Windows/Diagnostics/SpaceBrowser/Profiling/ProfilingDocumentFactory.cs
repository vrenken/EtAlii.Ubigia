namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.Views;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;
    using ProfilingDataConnection = EtAlii.Ubigia.Api.Diagnostics.Profiling.ProfilingDataConnection;

    public class ProfilingDocumentFactory : IProfilingDocumentFactory
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

            container.Register<IProfilingViewModel, ProfilingViewModel>(Lifestyle.Singleton);
            container.Register<IProfilingAspectsViewModel, ProfilingAspectsViewModel>(Lifestyle.Singleton);

            container.Register<IProfilingDataContext>(() => (IProfilingDataContext)dataContext, Lifestyle.Singleton);
            container.Register<IProfilingLogicalContext>(() => (IProfilingLogicalContext)logicalContext, Lifestyle.Singleton);
            container.Register<IProfilingFabricContext>(() => (IProfilingFabricContext)fabricContext, Lifestyle.Singleton);
            container.Register<IProfilingDataConnection>(() => (IProfilingDataConnection)connection, Lifestyle.Singleton);

            container.Register<ProfilingView>(Lifestyle.Singleton);
            container.Register<DocumentViewModelProvider>(Lifestyle.Singleton);

            container.Register< IProfileComposer>(() => 
            new ProfileComposer(
                ((IProfilingDataContext)dataContext).Profiler,
                ((IProfilingLogicalContext)logicalContext).Profiler,
                ((IProfilingFabricContext)fabricContext).Profiler,
                ((IProfilingDataConnection)connection).Profiler
                ), Lifestyle.Singleton);

            var documentViewModel = container.GetInstance<IProfilingViewModel>();
            var documentViewModelService = container.GetInstance<DocumentViewModelProvider>();
            documentViewModelService.SetInstance(documentViewModel);
            return documentViewModel;
        }
    }
}
