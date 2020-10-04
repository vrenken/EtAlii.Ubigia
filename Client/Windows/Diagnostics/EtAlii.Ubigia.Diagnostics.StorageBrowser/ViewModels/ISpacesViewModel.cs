// ReSharper disable UnusedMember.Global
namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api.Transport;

    public interface ISpacesViewModel : INotifyPropertyChanged
    {
        ICommand AddCommand { get; }
        IEnumerable<Space> AvailableSpaces { get; }
        SpaceTemplate[] AvailableSpaceTemplates { get; }
        ICommand ClearCommand { get; }
        ICommand DeleteCommand { get; }
        ICommand SaveCommand { get; }
        Account SelectedAccount { get; set; }
        Space SelectedSpace { get; set; }
        SpaceTemplate SelectedSpaceTemplate { get; set; }
        string SpaceName { get; set; }
    }
}