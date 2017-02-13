namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using System.ComponentModel;
    public interface IMainWindowViewModel : INotifyPropertyChanged
    {
        IAccountsViewModel Accounts { get; }
        IRolesViewModel Roles { get; }
        ISpacesViewModel Spaces { get; }
        IStoragesViewModel Storages { get; }
    }
}