// ReSharper disable UnusedMember.Global
namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.ComponentModel;
    using System.Windows.Input;

    internal interface ITaskbarIconViewModel : INotifyPropertyChanged
    {
        bool CanShowAbout { get; set; }
        bool CanShowConfiguration { get; set; }
        bool CanShowStatus { get; set; }
        ICommand ExitApplicationCommand { get; }
        string IconToShow { get; set; }
        ICommand ShowAboutCommand { get; }
        ICommand ShowConfigurationCommand { get; }
        ICommand ShowStatusCommand { get; }
        string ToolTipText { get; set; }
    }
}