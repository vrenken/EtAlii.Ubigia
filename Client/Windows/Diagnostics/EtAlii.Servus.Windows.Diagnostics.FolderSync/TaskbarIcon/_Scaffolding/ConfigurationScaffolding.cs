namespace EtAlii.Servus.Diagnostics.FolderSync
{
    using SimpleInjector;

    internal class ConfigurationScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<ConfigurationViewModel>(Lifestyle.Transient);
            container.Register<ConfigurationWindow>(Lifestyle.Transient);
            container.RegisterInitializer<ConfigurationWindow>(window => window.DataContext = container.GetInstance<ConfigurationViewModel>());

            container.Register<EditFolderViewModel>(Lifestyle.Transient);
            container.Register<EditFolderWindow>(Lifestyle.Transient);
            container.RegisterInitializer<EditFolderWindow>(window => window.DataContext = container.GetInstance<EditFolderViewModel>());
        }
    }
}