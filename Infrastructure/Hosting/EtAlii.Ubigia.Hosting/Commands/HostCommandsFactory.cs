namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;

    public class HostCommandsFactory
    {
        public HostCommandsFactory()
        {
        }

        public IHostCommand[] Create(IInfrastructure infrastructure)
        {
            var container = new Container();
            container.Register<IProcessStarter, ProcessStarter>();
            container.Register<IWebsiteBrowser, WebsiteBrowser>();
            container.Register<IInfrastructure>(() => infrastructure);

            container.Register<IStartSpaceBrowserCommand, StartSpaceBrowserCommand>();
            container.Register<IStartStorageBrowserCommand, StartStorageBrowserCommand>();

            container.Register<IOpenAdminPortalCommand, OpenAdminPortalCommand>();
            container.Register<IOpenUserPortalCommand, OpenUserPortalCommand>();

            container.Register<IStartHostCommand, StartHostCommand>();
            container.Register<IStopHostCommand, StopHostCommand>();
            container.Register<IShutdownHostCommand, ShutdownHostCommand>();
            container.Register<IConfigureFirewallRulesCommand, ConfigureFirewallRulesCommand>();
            
            return new IHostCommand[]
            {
                container.GetInstance<IStartHostCommand>(),
                container.GetInstance<IStopHostCommand>(),
                container.GetInstance<IConfigureFirewallRulesCommand>(),
                container.GetInstance<IShutdownHostCommand>(),

                container.GetInstance<IStartSpaceBrowserCommand>(),
                container.GetInstance<IStartStorageBrowserCommand>(),
                container.GetInstance<IOpenAdminPortalCommand>(),
                container.GetInstance<IOpenUserPortalCommand>(),
            };
        }
    }
}
