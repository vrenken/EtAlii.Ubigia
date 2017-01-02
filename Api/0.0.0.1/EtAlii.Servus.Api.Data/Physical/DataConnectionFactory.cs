namespace EtAlii.Servus.Api.Data
{
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public sealed class DataConnectionFactory
    {
        public IDataConnection Create(IDataTransport dataTransport, INotificationTransport notificationTransport, IDiagnosticsConfiguration diagnostics = null, IInfrastructureClient client = null)
        {
            var container = new Container();

            container.RegisterSingle<IDataTransport>(() => dataTransport);
            container.RegisterSingle<INotificationTransport>(() => notificationTransport);

            bool hasNewInfrastructureClientRegistration;
            RegisterStructure(container, client, out hasNewInfrastructureClientRegistration);
            RegisterClients(container);
            RegisterDiagnostics(container, diagnostics, hasNewInfrastructureClientRegistration);

            return container.GetInstance<IDataConnection>();
        }

        public IDataConnection Create<D, N>(IDiagnosticsConfiguration diagnostics = null, IInfrastructureClient client = null)
            where D : IDataTransport
            where N : INotificationTransport
        {
            var container = new Container();

            container.RegisterSingle<IDataTransport, D>();
            container.RegisterSingle<INotificationTransport, N>();

            bool hasNewInfrastructureClientRegistration;
            RegisterStructure(container, client, out hasNewInfrastructureClientRegistration);
            RegisterClients(container);
            RegisterDiagnostics(container, diagnostics, hasNewInfrastructureClientRegistration);

            return container.GetInstance<IDataConnection>();
        }

        private void RegisterClients(Container container)
        {
            var isDataTransport = container.GetInstance<IDataTransport>() is WebApiDataTransport;
            if (isDataTransport)
            {
                RegisterDataClients<WebApiEntryDataClient, WebApiRootDataClient, WebApiContentDataClient>(container);
            }
            else
            {
                RegisterDataClients<EntryDataClientStub, RootDataClientStub, ContentDataClientStub>(container);
            }

            var isSignalRNotificationTransport = container.GetInstance<INotificationTransport>() is SignalRNotificationTransport;
            if (isSignalRNotificationTransport)
            {
                RegisterNotificationClients<SignalREntryNotificationClient, SignalRRootNotificationClient, SignalRContentNotificationClient>(container);
            }
            else
            {
                RegisterNotificationClients<EntryNotificationClientStub, RootNotificationClientStub, ContentNotificationClientStub>(container);
            }
        }

        private void RegisterDiagnostics(Container container, IDiagnosticsConfiguration diagnostics, bool hasNewInfrastructureClientRegistration)
        {
            if (diagnostics == null)
            {
                // We do not need to frustrate end clients with development diagnostics, 
                // so lets create a failsafe for when no diagnostics configuration is provided. 
                diagnostics = new DiagnosticsFactory().Create(false, false, false,
                    () => new DisabledLogFactory(),
                    () => new DisabledProfilerFactory(),
                    (factory) => factory.Create("EtAlii", "EtAlii.Servus.Api"),
                    (factory) => factory.Create("EtAlii", "EtAlii.Servus.Api"));
            }

            container.Register<IDiagnosticsConfiguration>(() => diagnostics, Lifestyle.Singleton);

            container.Register<ILogFactory>(() => diagnostics.CreateLogFactory(), Lifestyle.Singleton);
            container.Register<ILogger>(() => diagnostics.CreateLogger(container.GetInstance<ILogFactory>()), Lifestyle.Transient);
            if (diagnostics.EnableLogging) // logging is enabled.
            {
                if (hasNewInfrastructureClientRegistration)
                {
                    container.RegisterDecorator(typeof(IInfrastructureClient), typeof(LoggingInfrastructureClient), Lifestyle.Singleton);
                }

                container.RegisterDecorator(typeof(IStorageConnection), typeof(LoggingStorageConnection), Lifestyle.Singleton);
                container.RegisterDecorator(typeof(INotificationTransport), typeof(LoggingNotificationTransport), Lifestyle.Singleton);
                container.RegisterDecorator(typeof(IDataConnection), typeof(LoggingDataConnection), Lifestyle.Singleton);

                container.RegisterDecorator(typeof(IRootDataClient), typeof(LoggingRootDataClient), Lifestyle.Singleton);
                container.RegisterDecorator(typeof(IEntryDataClient), typeof(LoggingEntryDataClient), Lifestyle.Singleton);
            }

            container.Register<IProfilerFactory>(() => diagnostics.CreateProfilerFactory(), Lifestyle.Singleton);
            container.Register<IProfiler>(() => diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()), Lifestyle.Singleton);
            if (diagnostics.EnableProfiling) // profiling is enabled
            {
                container.RegisterDecorator(typeof(IDataConnection), typeof(ProfilingDataConnection), Lifestyle.Singleton);
            }

            if (diagnostics.EnableDebugging) // diagnostics is enabled
            {
            }
        }

        private static void RegisterStructure(Container container, IInfrastructureClient client, out bool hasNewInfrastructureClientRegistration)
        {
            hasNewInfrastructureClientRegistration = false;
            container.RegisterSingle<IStorageConnection, StorageConnection>();
            container.RegisterSingle<IConnectionStatusProvider, ConnectionStatusProvider>();
            container.RegisterSingle<IDataConnection, DataConnection>();
            container.RegisterInitializer<IDataConnection>(connection => { container.GetInstance<IConnectionStatusProvider>().Connection = connection; });

            container.RegisterSingle<IAddressFactory, AddressFactory>();

            if (client != null)
            {
                container.RegisterSingle<IInfrastructureClient>(() => client);
            }
            else
            {
                hasNewInfrastructureClientRegistration = true;
                container.RegisterSingle<IInfrastructureClient, InfrastructureClient>();
                container.RegisterSingle<IPayloadSerializer, PayloadSerializer>();
                container.RegisterSingle<IHttpClientFactory, DefaultHttpClientFactory>();
            }

            RegisterEntryContext(container);
            RegisterContentContext(container);

            container.RegisterSingle<IRootContext, RootContext>();
        }

        private static void RegisterEntryContext(Container container)
        {
            var cacheEnabled = true;
            if (cacheEnabled)
            {
                // Caching enabled.
                container.RegisterSingle<EntryCacheChangeHandler>();
                container.RegisterSingle<EntryCacheGetHandler>();
                container.RegisterSingle<EntryCacheGetRelatedHandler>();
                container.RegisterSingle<EntryCacheReconnectOnStartupHandler>();

                container.RegisterSingle<EntryCacheProvider>();
                container.RegisterSingle<EntryCacheHelper>();
                container.RegisterSingle<EntryContext>();
                container.RegisterSingle<EntryCacheContextProvider>(() => new EntryCacheContextProvider(container.GetInstance<EntryContext>()));
                container.RegisterSingle<IEntryContext>(() => CreateCachingEntryContext(container));
            }
            else
            {
                // Caching disabled.
                container.RegisterSingle<IEntryContext, EntryContext>();
            }
        }

        private static void RegisterContentContext(Container container)
        {
            var cacheEnabled = true;
            if (cacheEnabled)
            {
                // Caching enabled.
                container.RegisterSingle<ContentCacheRetrieveDefinitionHandler>();
                container.RegisterSingle<ContentCacheStoreDefinitionHandler>();

                container.RegisterSingle<ContentCacheRetrieveHandler>();
                container.RegisterSingle<ContentCacheRetrievePartHandler>();
                container.RegisterSingle<ContentCacheStoreHandler>();
                container.RegisterSingle<ContentCacheStorePartHandler>();

                container.RegisterSingle<ContentCacheProvider>();
                container.RegisterSingle<ContentCacheHelper>();
                container.RegisterSingle<ContentDefinitionCacheHelper>();
                container.RegisterSingle<ContentContext>();
                container.RegisterSingle<ContentCacheContextProvider>(() => new ContentCacheContextProvider(container.GetInstance<ContentContext>()));
                container.RegisterSingle<IContentContext>(() => CreateCachingContentContext(container));
            }
            else
            {
                // Caching disabled.
                container.RegisterSingle<IContentContext, ContentContext>();
            }
        }

        private static IContentContext CreateCachingContentContext(Container container)
        {
            return new CachingContentContext
            (
                container.GetInstance<ContentCacheContextProvider>(),
                container.GetInstance<ContentCacheRetrieveDefinitionHandler>(),
                container.GetInstance<ContentCacheStoreDefinitionHandler>(),
                container.GetInstance<ContentCacheRetrieveHandler>(),
                container.GetInstance<ContentCacheRetrievePartHandler>(),
                container.GetInstance<ContentCacheStoreHandler>(),
                container.GetInstance<ContentCacheStorePartHandler>()
            );
        }

        private static IEntryContext CreateCachingEntryContext(Container container)
        {
            return new CachingEntryContext
            (
                container.GetInstance<EntryCacheContextProvider>(),
                container.GetInstance<EntryCacheChangeHandler>(),
                container.GetInstance<EntryCacheGetHandler>(),
                container.GetInstance<EntryCacheGetRelatedHandler>(),
                container.GetInstance<EntryCacheStoreHandler>()
            );
        }

        public static void RegisterNotificationClients<E, R, C>(Container container)
            where E : IEntryNotificationClient
            where R : IRootNotificationClient
            where C : IContentNotificationClient
        {
            container.RegisterSingle<IEntryNotificationClient, E>();
            container.RegisterSingle<IRootNotificationClient, R>();
            container.RegisterSingle<IContentNotificationClient, C>();
        }

        public static void RegisterDataClients<E, R, C>(Container container)
            where E : IEntryDataClient
            where R : IRootDataClient
            where C : IContentDataClient
        {
            container.RegisterSingle<IEntryDataClient, E>();
            container.RegisterSingle<IRootDataClient, R>();
            container.RegisterSingle<IContentDataClient, C>();
        }
    }
}
