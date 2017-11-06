namespace EtAlii.xTechnology.Hosting
{
    using System.Windows.Input;
    using System.ComponentModel;
    public interface ITaskbarIconViewModel : INotifyPropertyChanged
    {
        bool CanStartService { get; set; }
        bool CanStopService { get; set; }
        string ToolTipText { get; }

        void Initialize(ITrayIconHost host);
    }
}