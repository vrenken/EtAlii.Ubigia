namespace EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore.Tests
{
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.AspNetCore;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting;

    public static class HostConfigurationTestHostExtension
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