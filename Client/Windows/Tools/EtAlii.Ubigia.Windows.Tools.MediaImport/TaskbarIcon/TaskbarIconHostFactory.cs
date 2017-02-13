namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Collections.ObjectModel;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class TaskbarIconHostFactory
    {
        public ITaskbarIconHost Create(
            IDiagnosticsConfiguration diagnostics, 
            IDataConnection connection,
            IObservableFolderSyncConfigurationCollection folderSyncConfigurations)
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

            container.Register<ITaskbarIconHost, TaskbarIconHost>();

            return container.GetInstance<ITaskbarIconHost>();
        }
    }
}
