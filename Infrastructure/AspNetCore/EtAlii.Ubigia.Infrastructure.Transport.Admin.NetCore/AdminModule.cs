namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.NetCore
{
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    public class AdminModule : NetCoreModuleBase
    {
        public AdminModule(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
        }
    }
}
