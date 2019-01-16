namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.xTechnology.Mvvm;

    public class ProfilingAspectsViewModel : BindableBase, IProfilingAspectsViewModel
    {
        public IProfilingAspectViewModel[] Functional { get; }

        public IProfilingAspectViewModel[] Logical { get; }

        public IProfilingAspectViewModel[] Fabric { get; }

        public IProfilingAspectViewModel[] Transport { get; }

        public ProfilingAspectsViewModel(
            IProfilingGraphSLScriptContext scriptContext,
            IProfilingGraphQLQueryContext queryContext,
            IProfilingLogicalContext logicalContext,
            IProfilingFabricContext fabricContext,
            IProfilingDataConnection connection)
        {
            Functional = Create(ProfilingAspects.Functional.All, scriptContext, () => Functional);
            //Functional = Create(ProfilingAspects.Functional.All, scriptContext, queryContext, () => Functional);
            Logical = Create(ProfilingAspects.Logical.All, logicalContext, () => Logical);
            Fabric = Create(ProfilingAspects.Fabric.All, fabricContext, () => Fabric);
            Transport = Create(ProfilingAspects.Transport.All, connection, () => Transport);

            Initializer(Functional);
            Initializer(Logical);
            Initializer(Fabric);
            Initializer(Transport);
        }


        private void Initializer(IProfilingAspectViewModel[] aspectViewModels)
        {
            foreach (var aspectViewModel in aspectViewModels)
            {
                aspectViewModel.IsActive = true;
            }
        }

        private IProfilingAspectViewModel[] Create(
            ProfilingAspect[] aspects, IProfilingContext context, Func<IProfilingAspectViewModel[]> getAllContextProfilingAspectViewModels)
        {
            return aspects
                .Select(aspect => new ProfilingAspectViewModel(aspect, "Filters", context, getAllContextProfilingAspectViewModels))
                .Concat(new IProfilingAspectViewModel[]
                {
                    new ProfilingAspectActionViewModel("All", "Actions", getAllContextProfilingAspectViewModels, vm => vm.IsActive = true),
                    new ProfilingAspectActionViewModel("None", "Actions", getAllContextProfilingAspectViewModels, vm => vm.IsActive = false), 
                })
                .ToArray();
        }
    }
}
