
namespace EtAlii.Ubigia.Windows.Client
{
    /// <summary>
    /// Shows the main info tab.
    /// </summary>
    public class ShowSettingsCommand : ShowTabItemCommand<ShowSettingsCommand>
    {
        public override System.Windows.Controls.TabItem TabItemToActivate => App.Current.MainWindow.SettingsTabItem;
    }
}