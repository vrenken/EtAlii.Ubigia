namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.User
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public static class InfrastructureConfigurationUserPortalExtension
    {
        public static IInfrastructureConfiguration UseWebApiUserPortal(this IInfrastructureConfiguration configuration)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new UserPortalInfrastructureExtension(configuration) 
            };
            return configuration
                .Use(extensions)
                .Use<IUserPortalComponent, UserPortalComponent>();
        }
    }
}