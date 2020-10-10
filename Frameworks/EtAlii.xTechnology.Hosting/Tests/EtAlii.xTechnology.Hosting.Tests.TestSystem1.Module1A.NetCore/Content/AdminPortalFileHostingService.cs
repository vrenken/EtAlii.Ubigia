namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.Portal.NetCore
{
    using Microsoft.Extensions.Configuration;

    public class AdminPortalFileHostingService : ServiceBase
    {
        public AdminPortalFileHostingService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }
    }
}
