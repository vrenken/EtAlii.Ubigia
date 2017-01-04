namespace EtAlii.Ubigia.Infrastructure.WebApi.Portal.User
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public static class IInfrastructureConfigurationUserPortalExtension
    {
        public static IInfrastructureConfiguration UseWebApiUserPortal(this IInfrastructureConfiguration configuration)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new UserPortalInfrastructureExtension(configuration) 
            };
            return configuration
                .Use(extensions)
                .UseComponents(new UserPortalComponent());
        }
    }
}