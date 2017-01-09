namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.ComponentModel;
    public interface IMainWindow
    {
        IMainWindowViewModel DataContext { get; set; }
        void Show();
    }
}