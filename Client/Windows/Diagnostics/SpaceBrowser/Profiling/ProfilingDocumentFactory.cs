﻿namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingDocumentFactory : IProfilingDocumentFactory
    {
        public IDocumentViewModel Create(IDocumentContext documentContext)
        {
            var container = new Container();

            new DiagnosticsScaffolding().Register(container, documentContext.Diagnostics, documentContext.Logger, documentContext.LogFactory);
            new StructureScaffolding().Register(container);

            container.Register<IProfilingViewModel, ProfilingViewModel>();
            container.Register<IProfilingAspectsViewModel, ProfilingAspectsViewModel>();

            container.Register(() => (IProfilingGraphSLScriptContext)documentContext.ScriptContext);
            container.Register(() => (IProfilingGraphQLQueryContext)documentContext.QueryContext);

            container.Register(() => (IProfilingLogicalContext)documentContext.LogicalContext);
            container.Register(() => (IProfilingFabricContext)documentContext.FabricContext);
            container.Register(() => (IProfilingDataConnection)documentContext.Connection);
            container.Register(() =>
            {
                var dvmp = container.GetInstance<IDocumentViewModelProvider>();
                return documentContext.GraphContextFactory.Create(documentContext.Logger, documentContext.Journal, documentContext.FabricContext, dvmp);
            });

            container.Register<IDocumentViewModelProvider, DocumentViewModelProvider>();

            container.Register<IProfileComposer>(() => 
            new ProfileComposer(
                ((IProfilingGraphSLScriptContext)documentContext.ScriptContext).Profiler,
                ((IProfilingGraphQLQueryContext)documentContext.QueryContext).Profiler,
                ((IProfilingLogicalContext)documentContext.LogicalContext).Profiler,
                ((IProfilingFabricContext)documentContext.FabricContext).Profiler,
                ((IProfilingDataConnection)documentContext.Connection).Profiler
                ));

            var documentViewModel = container.GetInstance<IProfilingViewModel>();
            var documentViewModelProvider = container.GetInstance<IDocumentViewModelProvider>();
            documentViewModelProvider.SetInstance(documentViewModel);
            return documentViewModel;
        }
    }
}
