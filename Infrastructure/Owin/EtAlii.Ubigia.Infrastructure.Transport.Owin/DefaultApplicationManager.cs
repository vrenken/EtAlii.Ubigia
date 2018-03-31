namespace EtAlii.Ubigia.Infrastructure.Transport.Owin
{
    using System;
    using global::Owin;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Owin.Hosting;

    public class DefaultApplicationManager : NewApplicationManager
    {
        public DefaultApplicationManager(IInfrastructureConfiguration configuration)
            : base(applicationBuilderFactory => CreateApplicationBuilder(configuration, applicationBuilderFactory))
        {
        }

        private static IDisposable CreateApplicationBuilder(IInfrastructureConfiguration configuration, Action<IAppBuilder> applicationBuilder)
        {
            return WebApp.Start(configuration.Address.ToString(), applicationBuilder);
        }
    }
}