namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public class TrayIconHostScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<ITaskbarIconViewModel, TaskbarIconViewModel>();
            container.Register<ITaskbarIcon, TaskbarIcon>();
            container.RegisterInitializer<IHost>(host =>
            {
                container.GetInstance<ITaskbarIconViewModel>().Initialize((ITrayIconHost)host);
            });
        }
    }
}