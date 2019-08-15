namespace EtAlii.Ubigia.Windows.Client
{
    using EtAlii.Ubigia.Windows.Mvvm;
    using EtAlii.Ubigia.Windows.Settings;

    public class MainWindowViewModel : BindableBase
    {
        public IGlobalSettings GlobalSettings { get; }

        public MainWindowViewModel(IGlobalSettings globalSettings)
        {
            GlobalSettings = globalSettings;
        }
    }
}
