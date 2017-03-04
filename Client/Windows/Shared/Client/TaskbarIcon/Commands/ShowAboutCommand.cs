
namespace EtAlii.Ubigia.Client.Windows.TaskbarIcon
{
    /// <summary>
    /// Shows the about info tab.
    /// </summary>
    public class ShowAboutCommand : ShowTabItemCommand<ShowAboutCommand>
    {
        public override System.Windows.Controls.TabItem TabItemToActivate => App.Current.MainWindow.AboutTabItem;
    }
}