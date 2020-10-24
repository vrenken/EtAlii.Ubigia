// ReSharper disable UnusedMember.Global
namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Input;

    public interface IRolesViewModel : INotifyPropertyChanged
    {
        ICommand AddCommand { get; }
        IEnumerable<Role> AvailableRoles { get; }
        ICommand ClearCommand { get; }
        ICommand DeleteCommand { get; }
        string RoleName { get; set; }
        ICommand SaveCommand { get; }
        Account SelectedAccount { get; set; }
        Role SelectedRole { get; set; }
    }
}