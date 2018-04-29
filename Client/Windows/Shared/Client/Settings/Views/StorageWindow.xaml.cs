namespace EtAlii.Ubigia.Windows.Client
{
    /// <summary>
    /// Interaction logic for StorageWindow.xaml
    /// </summary>
    public partial class StorageWindow
    {
        public new StorageSettingsViewModel DataContext
        {
            get { return base.DataContext as StorageSettingsViewModel; }
            set { base.DataContext = value; }
        }

        public StorageWindow()
        {
            InitializeComponent();
        }
    }
}
