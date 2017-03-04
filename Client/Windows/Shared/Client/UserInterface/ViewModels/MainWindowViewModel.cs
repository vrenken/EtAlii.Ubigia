namespace EtAlii.Ubigia.Client.Windows.UserInterface
{
    using EtAlii.xTechnology.Mvvm;

    public class MainWindowViewModel : BindableBase
    {
        public IGlobalSettings GlobalSettings => _globalSettings;
        private readonly IGlobalSettings _globalSettings;

        public MainWindowViewModel(IGlobalSettings globalSettings)
        {
            _globalSettings = globalSettings;
        }
    }
}
