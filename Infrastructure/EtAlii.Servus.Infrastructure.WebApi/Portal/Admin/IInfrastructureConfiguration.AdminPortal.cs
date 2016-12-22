namespace EtAlii.Servus.Infrastructure.WebApi.Portal.Admin
{
    using EtAlii.Servus.Infrastructure.Functional;

    public static class IInfrastructureConfigurationAdminPortalExtension
    {
        public static IInfrastructureConfiguration UseWebApiAdminPortal(this IInfrastructureConfiguration configuration)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new AdminPortalInfrastructureExtension(configuration),  
            };
            return configuration
                .Use(extensions);
                //.Use<WebApiInfrastructure>();
        }
    }
}