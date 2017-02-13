using Hardcodet.Wpf.TaskbarNotification;

namespace EtAlii.Ubigia.Client.Windows.TaskbarIcon
{
    using EtAlii.Ubigia.Client.Windows.Shared;

    public interface ITaskbarIconService : IApplicationService
    {
        Hardcodet.Wpf.TaskbarNotification.TaskbarIcon TaskbarIcon { get; }
    }
}