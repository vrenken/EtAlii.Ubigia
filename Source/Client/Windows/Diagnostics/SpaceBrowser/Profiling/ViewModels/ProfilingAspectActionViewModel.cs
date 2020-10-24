namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Windows.Input;
    using EtAlii.Ubigia.Windows.Mvvm;

    public class ProfilingAspectActionViewModel : BindableBase, IProfilingAspectViewModel
    {
        private readonly Func<IProfilingAspectViewModel[]> _getAllContextProfilingAspectViewModels;
        private readonly Action<IProfilingAspectViewModel> _action;
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        private string _title;

        public string Group { get => _group; set => SetProperty(ref _group, value); }
        private string _group;
        public bool IsActive { get; set; }

        public ICommand InvokeActionCommand { get; }

        public ProfilingAspectActionViewModel(
            string title, 
            string group, 
            Func<IProfilingAspectViewModel[]> getAllContextProfilingAspectViewModels, 
            Action<IProfilingAspectViewModel> action)
        {
            _getAllContextProfilingAspectViewModels = getAllContextProfilingAspectViewModels;
            _action = action;
            _title = title;
            _group = group;

            InvokeActionCommand = new RelayCommand(InvokeAction, CanInvokeAction);
        }

        private void InvokeAction(object obj)
        {
            var profilingAspectViewModels = _getAllContextProfilingAspectViewModels();

            foreach (var profilingAspectViewModel in profilingAspectViewModels)
            {
                _action(profilingAspectViewModel);
            }
        }

        private bool CanInvokeAction(object obj)
        {
            return true;
        }
    }
}