namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.NetCore
{
    using Microsoft.Extensions.Configuration;

    public class AuthenticationService : ServiceBase
    {
        public AuthenticationService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }
    }
}
