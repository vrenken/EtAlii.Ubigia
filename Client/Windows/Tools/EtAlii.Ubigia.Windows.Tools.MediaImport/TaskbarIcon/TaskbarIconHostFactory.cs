namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Collections.ObjectModel;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using SimpleInjector;

    internal class TaskbarIconHostFactory
    {
        public TaskbarIconHost Create(
            IDiagnosticsConfiguration diagnostics, 
            IDataConnection connection,
            ObservableCollection<FolderSyncConfiguration> folderSyncConfigurations)
        {
            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new DataContextScaffolding(connection), 
                new DiagnosticsScaffolding(diagnostics),
                new TaskbarIconScaffolding(),
                new FolderMonitorScaffolding(folderSyncConfigurations),
                new ItemCheckScaffolding(),
                new ConfigurationScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            return container.GetInstance<TaskbarIconHost>();
        }
    }
}
