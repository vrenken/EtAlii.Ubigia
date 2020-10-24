namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    public interface IMainWindow 
    {
        object DataContext { get; set; }
        void InitializeComponent();
        void Show();
    }
}