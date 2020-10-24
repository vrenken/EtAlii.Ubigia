namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    internal interface IConfigurationWindow
    {
        object DataContext { get; set; }

        void Show();
        void InitializeComponent();
    }
}