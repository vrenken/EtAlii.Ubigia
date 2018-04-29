namespace EtAlii.Ubigia.Windows.Client
{
    using EtAlii.xTechnology.Mvvm;

    public class TaskbarIconViewModel : BindableBase
    {
        public bool StartAutomatically { get { return _startAutomatically; } set { SetProperty(ref _startAutomatically, value); } }
        private bool _startAutomatically;

        public bool AutomaticallySendLogFiles { get { return _automaticallySendLogFiles; } set { SetProperty(ref _automaticallySendLogFiles, value); } }
        private bool _automaticallySendLogFiles;
    }
}
