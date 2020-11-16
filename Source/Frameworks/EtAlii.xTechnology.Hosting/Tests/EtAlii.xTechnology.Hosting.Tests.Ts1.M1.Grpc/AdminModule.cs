namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.Grpc
{
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
            // Nothing to do here right now...
        }
    }
}
