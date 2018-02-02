namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Owin
{
    using EtAlii.xTechnology.Hosting;
    using global::Owin;
    using Microsoft.Extensions.Configuration;

    public class AdminModule : OwinModuleBase
	{
        public AdminModule(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

        protected override void OnConfigureApplication(IAppBuilder applicationBuilder)
        {
        }
    }
}
