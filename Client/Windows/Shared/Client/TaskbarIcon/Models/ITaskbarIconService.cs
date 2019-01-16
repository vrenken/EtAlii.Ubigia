namespace EtAlii.Ubigia.Windows.Client
{
    public interface ITaskbarIconService : IApplicationService
    {
        Hardcodet.Wpf.TaskbarNotification.TaskbarIcon TaskbarIcon { get; }
    }
}