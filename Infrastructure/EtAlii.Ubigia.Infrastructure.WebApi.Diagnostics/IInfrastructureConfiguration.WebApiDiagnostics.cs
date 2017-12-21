namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Diagnostics
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;

    public static class IInfrastructureConfigurationWebApiDiagnosticsExtension
    {
        public static IInfrastructureConfiguration UseWebApi(this IInfrastructureConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IInfrastructureExtension[] // TODO: These extensions should be used, right?!
            {
                new WebApiDiagnosticsInfrastructureExtension(diagnostics),
            };
            return configuration
                .UseWebApi()
                .Use(diagnostics);
        }
    }
}