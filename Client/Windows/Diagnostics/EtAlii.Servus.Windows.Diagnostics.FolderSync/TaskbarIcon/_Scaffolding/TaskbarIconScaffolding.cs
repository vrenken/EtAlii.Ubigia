namespace EtAlii.Servus.Diagnostics.FolderSync
{
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;

    public class TaskbarIconScaffolding : IScaffolding
    {
        public TaskbarIconScaffolding()
        {
        }

        public void Register(Container container)
        {
            container.Register<TaskbarIconViewModel, TaskbarIconViewModel>(Lifestyle.Singleton);
            container.Register<TaskbarIcon, TaskbarIcon>(Lifestyle.Singleton);
            container.RegisterInitializer<TaskbarIcon>(taskbarIcon => taskbarIcon.DataContext = container.GetInstance<TaskbarIconViewModel>());
        }
    }
}
