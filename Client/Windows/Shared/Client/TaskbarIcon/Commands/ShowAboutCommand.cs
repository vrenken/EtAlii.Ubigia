
namespace EtAlii.Ubigia.Client.Windows.TaskbarIcon
{
    /// <summary>
    /// Shows the about info tab.
    /// </summary>
    public class ShowAboutCommand : ShowTabItemCommand<ShowAboutCommand>
    {
        public override System.Windows.Controls.TabItem TabItemToActivate
        {
            get { return App.Current.MainWindow.AboutTabItem; }
        }
    }
}