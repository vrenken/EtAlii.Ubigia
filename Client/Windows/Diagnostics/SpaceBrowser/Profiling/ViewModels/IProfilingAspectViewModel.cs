namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.ComponentModel;

    public interface IProfilingAspectViewModel : INotifyPropertyChanged
    {
        string Title { get; set; }
        string Group { get; set; }
        bool IsActive { get; set; }
    }
}