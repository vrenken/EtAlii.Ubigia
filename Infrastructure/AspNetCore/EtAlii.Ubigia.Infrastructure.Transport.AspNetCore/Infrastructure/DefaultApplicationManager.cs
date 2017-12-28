namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting.AspNetCore;
    using Microsoft.AspNetCore.Builder;

    public class DefaultApplicationManager : NewApplicationManager
    {
        public DefaultApplicationManager(IInfrastructureConfiguration configuration)
            : base(applicationBuilderFactory => CreateApplicationBuilder(configuration, applicationBuilderFactory))
        {
        }

        private static IDisposable CreateApplicationBuilder(IInfrastructureConfiguration configuration, Action<IApplicationBuilder> applicationBuilder)
        {
            return null;//WebApp.Start(configuration.Address, applicationBuilder);
        }
    }
}