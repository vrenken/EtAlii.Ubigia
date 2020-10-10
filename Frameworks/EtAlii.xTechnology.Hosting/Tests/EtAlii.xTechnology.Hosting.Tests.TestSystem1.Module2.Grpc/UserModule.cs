namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Grpc
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    public class UserModule : ModuleBase
    {
        public UserModule(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            // Nothing to do here right now...
        }
    }
}
