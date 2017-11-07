namespace EtAlii.xTechnology.Hosting
{
    using System.Windows.Input;
    using System.ComponentModel;
    using System.Drawing;

    public interface ITaskbarIconViewModel : INotifyPropertyChanged
    {
        string ToolTipText { get; }

        void Initialize(ITrayIconHost host, Icon runningIcon, Icon stoppedIcon, Icon errorIcon);
    }
}