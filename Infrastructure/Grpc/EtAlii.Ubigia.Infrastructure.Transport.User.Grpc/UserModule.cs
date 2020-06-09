namespace EtAlii.Ubigia.Infrastructure.Transport.User.Grpc
{
    using EtAlii.xTechnology.Hosting;
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
        }
    }
}
