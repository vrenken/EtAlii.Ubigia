using System.Windows;

namespace EtAlii.Servus.Client.Windows.UserInterface
{
    /// <summary>
    /// Interaction logic for StorageWindow.xaml
    /// </summary>
    public partial class StorageWindow : Window
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
