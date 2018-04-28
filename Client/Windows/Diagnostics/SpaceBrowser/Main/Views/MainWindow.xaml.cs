namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using Fluent;
    using System;
    using Settings = EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.Properties.Settings;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow, IMainWindow
    {
        public new IMainWindowViewModel DataContext
        {
            get { return base.DataContext as IMainWindowViewModel; }
            set { base.DataContext = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowInitialized(object sender, EventArgs e)
        {
            //LoadLayoutSettings();
        }

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //SaveLayoutSettings();
            Settings.Default.Save();
        }
    }
}
