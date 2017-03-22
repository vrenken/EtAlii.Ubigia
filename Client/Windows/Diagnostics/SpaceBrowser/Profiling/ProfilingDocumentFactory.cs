namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

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
            IJournalViewModel journal,
            IGraphContextFactory graphContextFactory)
        {
            var container = new Container();

            new DiagnosticsScaffolding().Register(container, diagnostics, logger, logFactory);
            new StructureScaffolding().Register(container);

            container.Register<IProfilingViewModel, ProfilingViewModel>();
            container.Register<IProfilingAspectsViewModel, ProfilingAspectsViewModel>();

            container.Register(() => (IProfilingDataContext)dataContext);
            container.Register(() => (IProfilingLogicalContext)logicalContext);
            container.Register(() => (IProfilingFabricContext)fabricContext);
            container.Register(() => (IProfilingDataConnection)connection);
            container.Register(() =>
            {
                var dvmp = container.GetInstance<IDocumentViewModelProvider>();
                return graphContextFactory.Create(logger, journal, fabricContext, dvmp);
            });

            //container.Register<IProfilingView, ProfilingView>();
            container.Register<IDocumentViewModelProvider, DocumentViewModelProvider>();

            container.Register<IProfileComposer>(() => 
            new ProfileComposer(
                ((IProfilingDataContext)dataContext).Profiler,
                ((IProfilingLogicalContext)logicalContext).Profiler,
                ((IProfilingFabricContext)fabricContext).Profiler,
                ((IProfilingDataConnection)connection).Profiler
                ));

            var documentViewModel = container.GetInstance<IProfilingViewModel>();
            var documentViewModelProvider = container.GetInstance<IDocumentViewModelProvider>();
            documentViewModelProvider.SetInstance(documentViewModel);
            return documentViewModel;
        }
    }
}
