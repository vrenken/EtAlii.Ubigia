namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Windows.Mvvm;

    public class ProfilingAspectViewModel : BindableBase, IProfilingAspectViewModel
    {
        private readonly ProfilingAspect _aspect;
        private readonly IProfilingContext _context;
        private readonly Func<IProfilingAspectViewModel[]> _getAllContextProfilingAspectViewModels;
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        private string _title;

        public string Group { get => _group; set => SetProperty(ref _group, value); }
        private string _group;

        public bool IsActive { get => _isActive; set => SetProperty(ref _isActive, value); }
        private bool _isActive;

        public ProfilingAspectViewModel(
            ProfilingAspect aspect, 
            string group, 
            IProfilingContext context, 
            Func<IProfilingAspectViewModel[]> getAllContextProfilingAspectViewModels)
        {
            _aspect = aspect;
            _context = context;
            _group = group;
            _getAllContextProfilingAspectViewModels = getAllContextProfilingAspectViewModels;
            _title = aspect.Id;

            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsActive":
                    _context.Profiler.Aspects = _getAllContextProfilingAspectViewModels()
                        .OfType<ProfilingAspectViewModel>()
                        .Where(vm => vm.IsActive)
                        .Select(vm => vm._aspect)
                        .ToArray();
                    break;
            }
        }
    }
}
