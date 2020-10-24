namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Grpc
{
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    public class AdminModule : ModuleBase
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
