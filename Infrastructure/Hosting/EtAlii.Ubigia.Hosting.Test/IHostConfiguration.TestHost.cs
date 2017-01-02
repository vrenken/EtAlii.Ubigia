namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using EtAlii.xTechnology.Diagnostics;

    public static class IHostConfigurationTestHostExtension
    {
        public static IHostConfiguration UseTestHost(this IHostConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            return configuration.UseTestHost<TestHost>(diagnostics);
        }

        public static IHostConfiguration UseTestHost<THost>(this IHostConfiguration configuration, IDiagnosticsConfiguration diagnostics)
            where THost: class, IHost
        {
            var extensions = new IHostExtension[]
            {
                new TestHostExtension(diagnostics),
            };
            return configuration
                .Use(diagnostics)
                .Use(extensions)
                .Use<THost>();
        }
    }
}