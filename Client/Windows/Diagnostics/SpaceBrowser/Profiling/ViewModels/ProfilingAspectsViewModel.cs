namespace EtAlii.Ubigia.Client.Windows.Diagnostics
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
        private readonly IProfilingDataContext _dataContext;
        private readonly IProfilingLogicalContext _logicalContext;
        private readonly IProfilingFabricContext _fabricContext;
        private readonly IProfilingDataConnection _connection;

        public IProfilingAspectViewModel[] Functional { get; }

        public IProfilingAspectViewModel[] Logical { get; }

        public IProfilingAspectViewModel[] Fabric { get; }

        public IProfilingAspectViewModel[] Transport { get; }

        public ProfilingAspectsViewModel(
            IProfilingDataContext dataContext,
            IProfilingLogicalContext logicalContext,
            IProfilingFabricContext fabricContext,
            IProfilingDataConnection connection)
        {
            _dataContext = dataContext;
            _logicalContext = logicalContext;
            _fabricContext = fabricContext;
            _connection = connection;

            Functional = Create(ProfilingAspects.Functional.All, _dataContext, () => Functional);
            Logical = Create(ProfilingAspects.Logical.All, _logicalContext, () => Logical);
            Fabric = Create(ProfilingAspects.Fabric.All, _fabricContext, () => Fabric);
            Transport = Create(ProfilingAspects.Transport.All, _connection, () => Transport);

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
