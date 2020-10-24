namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.Properties;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IMainWindow
    {
        public IMainWindowViewModel ViewModel
        {
            get => DataContext as IMainWindowViewModel;
            set => DataContext = value;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowInitialized(object sender, EventArgs e)
        {
            //LoadLayoutSettings()
        }

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //SaveLayoutSettings()
            Settings.Default.Save();
        }
    }
}
