// ReSharper disable UnusedMember.Global

namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Input;

    public interface IAccountsViewModel : INotifyPropertyChanged
    {
        string AccountName { get; set; }
        string AccountPassword { get; set; }
        ICommand AddCommand { get; }
        IEnumerable<Account> AvailableAccounts { get; }
        AccountTemplate[] AvailableAccountTemplates { get; }
        ICommand ClearCommand { get; }
        ICommand DeleteCommand { get; }
        ICommand SaveCommand { get; }
        Account SelectedAccount { get; set; }
        AccountTemplate SelectedAccountTemplate { get; set; }
    }
}