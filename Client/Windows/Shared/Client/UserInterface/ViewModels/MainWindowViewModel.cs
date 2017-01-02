namespace EtAlii.Ubigia.Client.Windows.UserInterface
{
    using EtAlii.xTechnology.Mvvm;

    public class MainWindowViewModel : BindableBase
    {
        public GlobalSettings GlobalSettings { get { return _globalSettings; } }
        private readonly GlobalSettings _globalSettings;

        public MainWindowViewModel(GlobalSettings globalSettings)
        {
            _globalSettings = globalSettings;
        }
    }
}
