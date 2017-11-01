namespace EtAlii.xTechnology.Hosting
{
    using System.Windows;
    using System.Windows.Threading;
    using System.Drawing;

    public interface ITaskbarIcon
    {
        Icon Icon { get; set; }
        object DataContext { get; set; }
        Dispatcher Dispatcher { get; }
        Visibility Visibility { get; set; }
    }
}