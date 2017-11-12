namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
{
    using System;
    using EtAlii.xTechnology.Hosting;
    using Functional;
    using Storage;

    public static class HostConfigurationInfrastructureExtension
    {
        public static IHostConfiguration UseInfrastructure(this IHostConfiguration configuration, IStorage storage, IInfrastructure infrastructure)
        {
            if (infrastructure == null)
            {
                throw new NotSupportedException("A Infrastructure is required to construct a Host instance");
            }
            if (storage == null)
            {
                throw new NotSupportedException("A Storage is required to construct a Host instance");
            }

            configuration.Use(new InfrastructureHostExtension(storage, infrastructure));

            var services = new Type[]
            {
                typeof(IStorageService),
                typeof(IInfrastructureService),
            };
            configuration.Use(services);

            return configuration;

            //var scaffoldings = new List<IScaffolding>(new IScaffolding[]
            //{
            //new InfrastructureHostScaffolding(configuration), 

            //new HostDiagnosticsScaffolding(diagnostics), 
            //new HostProfilingScaffolding(diagnostics), 
            //new HostLoggingScaffolding(diagnostics), 
            //new HostDebuggingScaffolding(diagnostics), 
            //});

            //foreach (var scaffolding in scaffoldings)
            //{
            //    scaffolding.Register(container);
            //}

            //foreach (var extension in configuration.Extensions)
            //{
            //    extension.Register(container);
            //}

            //return configuration.GetHost(container);
        }
    }
}