namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Portal.NetCore
{
    using Microsoft.Extensions.Configuration;

    public class UserPortalFileHostingService : ServiceBase
    {
        public UserPortalFileHostingService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }
    }
}
