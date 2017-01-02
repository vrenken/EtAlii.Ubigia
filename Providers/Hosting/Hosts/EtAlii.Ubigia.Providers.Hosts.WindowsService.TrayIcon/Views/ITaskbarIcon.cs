namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using System.Windows;
    using System.Windows.Threading;

    public interface ITaskbarIcon
    {
        object DataContext { get; set; }
        Dispatcher Dispatcher { get; }
        Visibility Visibility { get; set; }
    }
}