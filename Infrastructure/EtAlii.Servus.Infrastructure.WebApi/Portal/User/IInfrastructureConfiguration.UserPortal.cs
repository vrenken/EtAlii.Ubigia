namespace EtAlii.Servus.Infrastructure.WebApi.Portal.User
{
    using EtAlii.Servus.Infrastructure.Functional;

    public static class IInfrastructureConfigurationUserPortalExtension
    {
        public static IInfrastructureConfiguration UseWebApiUserPortal(this IInfrastructureConfiguration configuration)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new UserPortalInfrastructureExtension(configuration) 
            };
            return configuration
                .Use(extensions);
        }
    }
}