﻿namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
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