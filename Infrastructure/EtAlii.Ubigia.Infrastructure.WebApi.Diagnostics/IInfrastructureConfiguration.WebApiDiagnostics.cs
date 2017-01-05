namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;

    public static class IInfrastructureConfigurationWebApiDiagnosticsExtension
    {
        public static IInfrastructureConfiguration UseWebApi(this IInfrastructureConfiguration configuration, IDiagnosticsConfiguration diagnostics, IApplicationManager applicationManager = null)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new WebApiDiagnosticsInfrastructureExtension(diagnostics),
            };
            return configuration
                .UseWebApi(applicationManager)
                .Use(diagnostics);
        }
    }
}