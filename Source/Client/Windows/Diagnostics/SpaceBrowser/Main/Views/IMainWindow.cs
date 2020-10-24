namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public interface IMainWindow
    {
        IMainWindowViewModel ViewModel { get; set; }
        void Show();
    }
}