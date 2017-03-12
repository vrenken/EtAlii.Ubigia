namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    public interface IMainWindow
    {
        IMainWindowViewModel DataContext { get; set; }
        void Show();
    }
}