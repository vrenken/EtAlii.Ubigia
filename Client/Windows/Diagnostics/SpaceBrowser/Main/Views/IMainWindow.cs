namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public interface IMainWindow
    {
        IMainWindowViewModel DataContext { get; set; }
        void Show();
    }
}