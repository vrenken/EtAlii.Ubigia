namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Grpc
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
