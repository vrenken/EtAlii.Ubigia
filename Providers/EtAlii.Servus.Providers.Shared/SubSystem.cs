namespace EtAlii.Servus.Providers
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.xTechnology.Logging;
    using Newtonsoft.Json;
    using SimpleInjector;
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Logical;

    public static class SubSystem
    {
        public static void Setup(Container container, IProvisioningConfiguration configuration, Func<Container, IEnumerable<ProviderComponent>> getComponents, IDiagnosticsConfiguration diagnostics)
        {
            RegisterStructure(container, configuration, getComponents);
            RegisterDiagnostics(container, diagnostics);

            //container.Register<IDataConnection>(() => new DataConnectionFactory().Create<WebApiDataTransport, SignalRNotificationTransport>(diagnostics), Lifestyle.Singleton);
            //container.RegisterInitializer<IDataConnection>(connection => InitializeDataConnection(container, connection));

            container.Register<IDataContext>(() =>
            {
                var connection = new DataConnectionFactory().Create<WebApiDataTransport, SignalRNotificationTransport>(diagnostics);
                connection.Open(configuration.Address, configuration.Account, configuration.Password, "Default");
                var fabricContext = new FabricContextFactory().Create(connection);
                var logicalContext = new LogicalContextFactory().Create(fabricContext, diagnostics);
                return new DataContextFactory().Create(logicalContext, diagnostics);
            }, Lifestyle.Singleton);
        }

        private static void RegisterStructure(Container container, IProvisioningConfiguration configuration, Func<Container, IEnumerable<ProviderComponent>> getComponents)
        {
            container.Register<ComponentManager, ComponentManager>(Lifestyle.Singleton);
            container.Register<IEnumerable<ProviderComponent>>(() => getComponents(container), Lifestyle.Singleton);
            container.Register<JsonSerializer>(() => new SerializerFactory().Create(), Lifestyle.Singleton);
            container.Register<IProvisioningConfiguration>(() => configuration, Lifestyle.Singleton);
        }

        //private static void InitializeDataConnection(Container container, IDataConnection connection)
        //{
        //    var configuration = container.GetInstance<IProvisioningConfiguration>();
        //    connection.Open(configuration.Address, configuration.Account, configuration.Password, "Default");
        //}

        private static void RegisterDiagnostics(Container container, IDiagnosticsConfiguration diagnostics)
        {
            container.Register<IDiagnosticsConfiguration>(() => diagnostics, Lifestyle.Singleton);

            container.Register<IProfilerFactory>(() => diagnostics.CreateProfilerFactory(), Lifestyle.Singleton);
            container.Register<IProfiler>(() => diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()), Lifestyle.Singleton);
            if (diagnostics.EnableProfiling) // profiling is enabled
            {
            }

            container.Register<ILogFactory>(() => diagnostics.CreateLogFactory(), Lifestyle.Singleton);
            container.Register<ILogger>(() => diagnostics.CreateLogger(container.GetInstance<ILogFactory>()), Lifestyle.Singleton);
            if (diagnostics.EnableLogging) // logging is enabled
            {
            }

            if (diagnostics.EnableDebugging) // debugging is enabled
            {
            }
        }

    }
}
