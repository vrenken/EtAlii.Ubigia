namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting;

    public static class IHostConfigurationTestHostExtension
    {
        public static IHostConfiguration UseTestHost(this IHostConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IHostExtension[]
            {
                new TestHostExtension(diagnostics),
            };
            return configuration
                .Use(diagnostics)
                .Use(extensions);
        }
    }
}