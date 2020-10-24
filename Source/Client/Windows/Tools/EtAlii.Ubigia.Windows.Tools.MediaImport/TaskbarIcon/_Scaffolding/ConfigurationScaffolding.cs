namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ConfigurationScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IConfigurationViewModel, ConfigurationViewModel>();
            container.Register<IConfigurationWindow, ConfigurationWindow>();
            container.RegisterInitializer<IConfigurationWindow>(window => window.DataContext = container.GetInstance<IConfigurationViewModel>());

            container.Register<IEditFolderViewModel, EditFolderViewModel>();
            container.Register<IEditFolderWindow, EditFolderWindow>();
            container.RegisterInitializer<IEditFolderWindow>(window => window.DataContext = container.GetInstance<IEditFolderViewModel>());
        }
    }
}