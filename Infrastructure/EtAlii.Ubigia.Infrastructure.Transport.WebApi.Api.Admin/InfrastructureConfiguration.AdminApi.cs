namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi.Api.Admin
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public static class InfrastructureConfigurationAdminApiExtension
    {
        public static IInfrastructureConfiguration UseWebApiAdminApi(this IInfrastructureConfiguration configuration)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new AdminApiInfrastructureExtension(configuration) 
            };
            return configuration
                .Use(extensions)
                .Use<IAdminApiComponent, AdminApiComponent>();
        }
    }
}