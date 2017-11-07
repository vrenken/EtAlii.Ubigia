namespace EtAlii.xTechnology.Hosting
{
    using System.Windows.Input;
    using System.ComponentModel;
    public interface ITaskbarIconViewModel : INotifyPropertyChanged
    {
        string ToolTipText { get; }

        void Initialize(ITrayIconHost host);
    }
}