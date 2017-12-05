namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting.Owin;

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
                .Use(new WebApiComponentManagerFactory().Create)
                .Use<OwinInfrastructure>();
        }
    }
}