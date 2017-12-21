namespace EtAlii.Ubigia.Infrastructure.Transport.Owin
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting.Owin;

    public static class IInfrastructureConfigurationOwinExtension
    {
        public static IInfrastructureConfiguration UseOwin<TInfrastructure>(
            this IInfrastructureConfiguration configuration,
            IApplicationManager applicationManager = null)
            where TInfrastructure : class, IInfrastructure

        {
            var extensions = new IInfrastructureExtension[]
            {
                new OwinInfrastructureExtension(applicationManager),
            };
            return configuration
                .Use(extensions)
                .Use<TInfrastructure>();
        }

        public static IInfrastructureConfiguration UseOwin(
            this IInfrastructureConfiguration configuration,
            IApplicationManager applicationManager = null)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new OwinInfrastructureExtension(applicationManager),
            };
            return configuration
                .Use(extensions)
                .Use<OwinInfrastructure>();
        }
    }
}