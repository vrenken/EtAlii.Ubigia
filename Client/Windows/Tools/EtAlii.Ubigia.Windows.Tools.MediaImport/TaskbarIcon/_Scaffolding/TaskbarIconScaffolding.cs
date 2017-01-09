namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using EtAlii.Ubigia.Api;
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
