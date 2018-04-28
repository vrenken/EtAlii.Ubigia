namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.ComponentModel;

    public interface IProfilingAspectViewModel : INotifyPropertyChanged
    {
        string Title { get; set; }
        string Group { get; set; }
        bool IsActive { get; set; }
    }
}