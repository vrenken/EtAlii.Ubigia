namespace EtAlii.Ubigia.Infrastructure.WebApi.Portal.Admin
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public static class InfrastructureConfigurationAdminPortalExtension
    {
        public static IInfrastructureConfiguration UseWebApiAdminPortal(this IInfrastructureConfiguration configuration)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new AdminPortalInfrastructureExtension(configuration),  
            };
            return configuration
                .Use(extensions)
                .UseComponents(new AdminPortalComponent());
                //.Use<WebApiInfrastructure>();
        }
    }
}