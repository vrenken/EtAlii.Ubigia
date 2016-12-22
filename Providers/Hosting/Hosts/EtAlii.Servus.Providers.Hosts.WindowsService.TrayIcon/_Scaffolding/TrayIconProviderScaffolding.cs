namespace EtAlii.Servus.Provisioning.Hosting
{
    using EtAlii.Servus.Provisioning;
    using EtAlii.xTechnology.MicroContainer;

    public class TrayIconProviderScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<ITaskbarIconViewModel, TaskbarIconViewModel>();
            container.Register<ITaskbarIcon, TaskbarIcon>();
            container.RegisterInitializer<ITaskbarIcon>(taskbarIcon =>
            {
                taskbarIcon.DataContext = container.GetInstance<ITaskbarIconViewModel>();
            });

            container.RegisterInitializer<IProviderHost>(provider =>
            {
                container.GetInstance<ITaskbarIconViewModel>().Initialize((ITrayIconProviderHost)provider);
            });

        }
    }
}
