namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public static class IInfrastructureConfigurationWebApiExtension
    {
        public static IInfrastructureConfiguration UseWebApi(this IInfrastructureConfiguration configuration)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new WebApiInfrastructureExtension(),
            };
            return configuration
                .Use(extensions)
                .Use(new WebApiComponentManagerFactory().Create);
        }
    }
}