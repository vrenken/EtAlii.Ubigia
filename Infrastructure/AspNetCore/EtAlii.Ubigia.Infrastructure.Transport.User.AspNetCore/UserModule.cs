namespace EtAlii.Ubigia.Infrastructure.Transport.User.AspNetCore
{
    using EtAlii.xTechnology.Hosting.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    public class UserModule : AspNetCoreModuleBase
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
