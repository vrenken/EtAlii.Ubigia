namespace EtAlii.Ubigia.Infrastructure.Transport.User.NetCore
{
    using EtAlii.xTechnology.Hosting.NetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    public class UserModule : NetCoreModuleBase
    {
        public UserModule(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            // Put all configuration logic here.
        }
    }
}
