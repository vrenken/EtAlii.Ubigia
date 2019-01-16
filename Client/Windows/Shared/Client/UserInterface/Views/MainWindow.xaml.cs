namespace EtAlii.Ubigia.Windows.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
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
