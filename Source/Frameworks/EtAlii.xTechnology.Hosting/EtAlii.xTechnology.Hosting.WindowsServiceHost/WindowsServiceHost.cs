namespace EtAlii.xTechnology.Hosting
{
    public partial class WindowsServiceHost : HostBase
    {
        protected WindowsServiceHost(
            IHostConfiguration configuration,
            ISystemManager systemManager)
            : base(configuration, systemManager)
        {
        }
    }
}
