using System.Windows;

namespace EtAlii.Ubigia.Client.Windows.UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public new MainWindowViewModel DataContext
        {
            get { return base.DataContext as MainWindowViewModel; }
            set { base.DataContext = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
