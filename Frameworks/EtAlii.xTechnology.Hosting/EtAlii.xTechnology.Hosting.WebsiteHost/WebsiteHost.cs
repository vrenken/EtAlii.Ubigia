namespace EtAlii.xTechnology.Hosting.WebsiteHost.NetCore
{
    public partial class WebsiteHost : HostBase
    {
        public WebsiteHost(IHostConfiguration configuration, ISystemManager systemManager)
            : base(configuration, systemManager)
        {
        }
    }
}
