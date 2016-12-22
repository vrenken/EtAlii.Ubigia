namespace EtAlii.Servus.Infrastructure
{
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Infrastructure.WebApi;

    public static class IInfrastructureConfigurationWebApiExtension
    {
        public static IInfrastructureConfiguration UseWebApi(this IInfrastructureConfiguration configuration, IApplicationManager applicationManager = null)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new WebApiInfrastructureExtension(applicationManager),
            };
            return configuration
                .Use(extensions)
                .Use(typeof(IWebApiComponentManager))
                .Use<WebApiInfrastructure>();
        }
    }
}