namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using EtAlii.xTechnology.MicroContainer;

    public class FolderMonitorScaffolding : IScaffolding
    {
        private readonly IObservableFolderSyncConfigurationCollection _folderSyncConfigurations;

        public FolderMonitorScaffolding(IObservableFolderSyncConfigurationCollection folderSyncConfigurations)
        {
            _folderSyncConfigurations = folderSyncConfigurations;
        }

        public void Register(Container container)
        {
            container.Register<IObservableFolderSyncConfigurationCollection>(() => _folderSyncConfigurations);
            container.Register<IFolderMonitor, FolderMonitor>();
            container.Register<IFolderMonitorManager, FolderMonitorManager>();
        }
    }
}
