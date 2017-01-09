namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public interface IJournalViewModel : INotifyPropertyChanged
    {
        ObservableCollection<JournalItem> Items { get; }
        int Size { get; set; }
        void AddItem(string action, string description);
    }
}