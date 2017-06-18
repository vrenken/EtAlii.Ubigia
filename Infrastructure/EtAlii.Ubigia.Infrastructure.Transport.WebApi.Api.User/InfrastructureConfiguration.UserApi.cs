namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public static class InfrastructureConfigurationAdminApiExtension
    {
        public static IInfrastructureConfiguration UseWebApiUserApi(this IInfrastructureConfiguration configuration)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new UserApiInfrastructureExtension(configuration) 
            };
            return configuration
                .Use(extensions)
                .Use<IUserApiComponent, UserApiComponent>();
        }
    }
}