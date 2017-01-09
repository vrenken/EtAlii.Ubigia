namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Collections.ObjectModel;
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;

    public class FolderMonitorScaffolding : IScaffolding
    {
        private readonly ObservableCollection<FolderSyncConfiguration> _folderSyncConfigurations;

        public FolderMonitorScaffolding(ObservableCollection<FolderSyncConfiguration> folderSyncConfigurations)
        {
            _folderSyncConfigurations = folderSyncConfigurations;
        }

        public void Register(Container container)
        {
            container.Register<ObservableCollection<FolderSyncConfiguration>>(() => _folderSyncConfigurations, Lifestyle.Singleton);
            container.Register<IFolderMonitor, FolderMonitor>(Lifestyle.Transient);
            container.Register<FolderMonitorManager>(Lifestyle.Singleton);
        }
    }
}
