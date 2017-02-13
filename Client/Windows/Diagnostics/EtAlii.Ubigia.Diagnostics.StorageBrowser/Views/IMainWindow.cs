namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using System.ComponentModel;
    public interface IMainWindow 
    {
        object DataContext { get; set; }
        void InitializeComponent();
        void Show();
    }
}