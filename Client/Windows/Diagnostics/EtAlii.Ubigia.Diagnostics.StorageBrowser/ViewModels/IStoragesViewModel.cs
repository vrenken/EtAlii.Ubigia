// ReSharper disable UnusedMember.Global
namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api;
    using System.ComponentModel;
    public interface IStoragesViewModel : INotifyPropertyChanged
    {
        ICommand AddCommand { get; }
        IEnumerable<Storage> AvailableStorages { get; }
        ICommand ClearCommand { get; }
        ICommand DeleteCommand { get; }
        ICommand SaveCommand { get; }
        Storage SelectedStorage { get; set; }
        string StorageAddress { get; set; }
        string StorageName { get; set; }
    }
}