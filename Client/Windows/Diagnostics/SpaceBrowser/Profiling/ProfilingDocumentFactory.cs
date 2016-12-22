namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using System;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Diagnostics.Profiling;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Windows.Diagnostics.SpaceBrowser.Views;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;
    using ProfilingDataConnection = EtAlii.Servus.Api.Diagnostics.Profiling.ProfilingDataConnection;

    public class ProfilingDocumentFactory : IDocumentFactory
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
