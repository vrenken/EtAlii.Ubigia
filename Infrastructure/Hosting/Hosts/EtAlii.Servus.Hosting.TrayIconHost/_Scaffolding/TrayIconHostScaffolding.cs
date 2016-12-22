namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Infrastructure;
    using SimpleInjector;

    public class TrayIconHostScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<TaskbarIconViewModel, TaskbarIconViewModel>(Lifestyle.Singleton);
            container.Register<ITaskbarIcon, TaskbarIcon>(Lifestyle.Singleton);
            container.RegisterInitializer<ITaskbarIcon>(taskbarIcon =>
            {
                taskbarIcon.DataContext = container.GetInstance<TaskbarIconViewModel>();
            });

            container.RegisterInitializer<IHost>(host =>
            {
                container.GetInstance<TaskbarIconViewModel>().Initialize((ITrayIconHost)host);
            });
        }
    }
}