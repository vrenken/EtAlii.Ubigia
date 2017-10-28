namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public class TrayIconProviderScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<ITaskbarIconViewModel, TaskbarIconViewModel>();
            container.Register<ITaskbarIcon, TaskbarIcon>();
            container.RegisterInitializer<IProviderHost>(provider =>
            {
                container.GetInstance<ITaskbarIconViewModel>().Initialize((ITrayIconProviderHost)provider);
            });

        }
    }
}
