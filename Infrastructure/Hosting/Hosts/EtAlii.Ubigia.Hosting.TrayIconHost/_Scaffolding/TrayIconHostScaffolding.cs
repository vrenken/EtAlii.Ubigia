namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public class TrayIconHostScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<ITaskbarIconViewModel, TaskbarIconViewModel>();
            container.Register<ITaskbarIcon, TaskbarIcon>();
            container.RegisterInitializer<ITaskbarIcon>(taskbarIcon =>
            {
                taskbarIcon.DataContext = container.GetInstance<ITaskbarIconViewModel>();
            });

            container.RegisterInitializer<IHost>(host =>
            {
                container.GetInstance<ITaskbarIconViewModel>().Initialize((ITrayIconHost)host);
            });
        }
    }
}