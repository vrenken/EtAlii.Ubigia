namespace EtAlii.Servus.Api.Transport
{
    using System;
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Structure;

    internal class DataConnectionClientsScaffolding : IScaffolding
    {
        private readonly ISelector<IDataTransport, Action<Container>> _dataClientsRegistrarSelector;
        private readonly ISelector<INotificationTransport, Action<Container>> _notificationClientsRegistrarSelector;

        public DataConnectionClientsScaffolding()
        {
            _dataClientsRegistrarSelector = new Selector<IDataTransport, Action<Container>>()
                .Register(transport => transport is WebApiDataTransport, RegisterDataClients<WebApiEntryDataClient, WebApiRootDataClient, WebApiContentDataClient, WebApiPropertiesDataClient>)
                .Register(transport => transport is SignalRDataTransport, c =>
                {
                    RegisterDataClients<SignalREntryDataClient, SignalRRootDataClient, SignalRContentDataClient, SignalRPropertiesDataClient>(c);
                    c.Register<IHubProxyMethodInvoker, HubProxyMethodInvoker>();
                })
                .Register(transport => true, RegisterDataClients<EntryDataClientStub, RootDataClientStub, ContentDataClientStub, PropertiesDataClientStub>);

            _notificationClientsRegistrarSelector = new Selector<INotificationTransport, Action<Container>>()
                .Register(transport => transport is SignalRNotificationTransport, RegisterNotificationClients<SignalREntryNotificationClient, SignalRRootNotificationClient, SignalRContentNotificationClient, SignalRPropertiesNotificationClient>)
                .Register(transport => true, RegisterNotificationClients<EntryNotificationClientStub, RootNotificationClientStub, ContentNotificationClientStub, PropertiesNotificationClientStub>);
        }

        public void Register(Container container)
        {
            var dataTransport = container.GetInstance<IDataTransport>();
            var dataClientsRegistrar = _dataClientsRegistrarSelector.Select(dataTransport);
            dataClientsRegistrar(container);

            var notificationTransport = container.GetInstance<INotificationTransport>();
            var notificationClientsRegistrar = _notificationClientsRegistrarSelector.Select(notificationTransport);
            notificationClientsRegistrar(container);
        }

        private void RegisterDataClients<TEntryDataClient, TRootDataClient, TContentDataClient, TPropertyDataClient>(Container container)
            where TEntryDataClient : IEntryDataClient
            where TRootDataClient : IRootDataClient
            where TContentDataClient : IContentDataClient
            where TPropertyDataClient : IPropertiesDataClient
        {
            container.Register<IEntryDataClient, TEntryDataClient>();
            container.Register<IRootDataClient, TRootDataClient>();
            container.Register<IContentDataClient, TContentDataClient>();
            container.Register<IPropertiesDataClient, TPropertyDataClient>();
        }

        private void RegisterNotificationClients<TEntryNotificationClient, TRootNotificationClient, TContentNotificationClient, TPropertyNotificationClient>(Container container)
            where TEntryNotificationClient : IEntryNotificationClient
            where TRootNotificationClient : IRootNotificationClient
            where TContentNotificationClient : IContentNotificationClient
            where TPropertyNotificationClient : IPropertiesNotificationClient
        {
            container.Register<IEntryNotificationClient, TEntryNotificationClient>();
            container.Register<IRootNotificationClient, TRootNotificationClient>();
            container.Register<IContentNotificationClient, TContentNotificationClient>();
            container.Register<IPropertiesNotificationClient, TPropertyNotificationClient>();
        }

    }
}
