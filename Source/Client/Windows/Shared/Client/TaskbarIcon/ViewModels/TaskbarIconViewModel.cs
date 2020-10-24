namespace EtAlii.Ubigia.Windows.Client
{
    using EtAlii.Ubigia.Windows.Mvvm;

    public class TaskbarIconViewModel : BindableBase
    {
        public bool StartAutomatically { get => _startAutomatically; set => SetProperty(ref _startAutomatically, value); }
        private bool _startAutomatically;

        public bool AutomaticallySendLogFiles { get => _automaticallySendLogFiles; set => SetProperty(ref _automaticallySendLogFiles, value); }
        private bool _automaticallySendLogFiles;
    }
}
