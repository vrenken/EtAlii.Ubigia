namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public class AuthenticationServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration)
        {
            return new AuthenticationService();
        }
    }
}
