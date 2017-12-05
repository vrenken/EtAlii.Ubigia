namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Diagnostics
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting.Owin;

    public static class IInfrastructureConfigurationWebApiDiagnosticsExtension
    {
        public static IInfrastructureConfiguration UseWebApi(this IInfrastructureConfiguration configuration, IDiagnosticsConfiguration diagnostics, IApplicationManager applicationManager = null)
        {
            var extensions = new IInfrastructureExtension[] // TODO: These extensions should be used, right?!
            {
                new WebApiDiagnosticsInfrastructureExtension(diagnostics),
            };
            return configuration
                .UseWebApi(applicationManager)
                .Use(diagnostics);
        }
    }
}