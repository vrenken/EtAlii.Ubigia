namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Windows;
    using EtAlii.xTechnology.Mvvm;
    using EtAlii.xTechnology.Workflow;
    using ICommand = System.Windows.Input.ICommand;

    public class ProfilingAspectsViewModel : BindableBase, IProfilingAspectsViewModel
    {
        private readonly IProfilingDataContext _dataContext;
        private readonly IProfilingLogicalContext _logicalContext;
        private readonly IProfilingFabricContext _fabricContext;
        private readonly IProfilingDataConnection _connection;

        public IProfilingAspectViewModel[] Functional => _functional;
        private readonly IProfilingAspectViewModel[] _functional;

        public IProfilingAspectViewModel[] Logical => _logical;
        private readonly IProfilingAspectViewModel[] _logical;

        public IProfilingAspectViewModel[] Fabric => _fabric;
        private readonly IProfilingAspectViewModel[] _fabric;

        public IProfilingAspectViewModel[] Transport => _transport;
        private readonly IProfilingAspectViewModel[] _transport;

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

            _functional = Create(ProfilingAspects.Functional.All, _dataContext, () => _functional);
            _logical = Create(ProfilingAspects.Logical.All, _logicalContext, () => _logical);
            _fabric = Create(ProfilingAspects.Fabric.All, _fabricContext, () => _fabric);
            _transport = Create(ProfilingAspects.Transport.All, _connection, () => _transport);

            Initializer(_functional);
            Initializer(_logical);
            Initializer(_fabric);
            Initializer(_transport);
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
