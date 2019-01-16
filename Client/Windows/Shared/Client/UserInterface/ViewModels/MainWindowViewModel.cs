namespace EtAlii.Ubigia.Windows.Client
{
    using EtAlii.Ubigia.Windows.Settings;
    using EtAlii.xTechnology.Mvvm;

    public class MainWindowViewModel : BindableBase
    {
        public IGlobalSettings GlobalSettings { get; }

        public MainWindowViewModel(IGlobalSettings globalSettings)
        {
            GlobalSettings = globalSettings;
        }
    }
}
