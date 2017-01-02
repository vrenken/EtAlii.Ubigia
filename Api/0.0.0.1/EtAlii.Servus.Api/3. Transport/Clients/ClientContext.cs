namespace EtAlii.Servus.Api.Transport
{
    using System.Threading.Tasks;

    public class ClientContext : IClientContext
    {
        public IAuthenticationDataClient AuthenticationData { get { return _authenticationData; } }
        private readonly IAuthenticationDataClient _authenticationData;

        public IRootNotificationClient RootNotifications { get { return _rootNotifications; } }
        private readonly IRootNotificationClient _rootNotifications;

        public IRootDataClient RootData { get { return _rootData; } }
        private readonly IRootDataClient _rootData;

        public IEntryDataClient EntryData { get { return _entryData; } }
        private readonly IEntryDataClient _entryData;

        public IEntryNotificationClient EntryNotifications { get { return _entryNotifications; } }
        private readonly IEntryNotificationClient _entryNotifications;

        public IPropertiesDataClient PropertiesData { get { return _propertiesData; } }
        private readonly IPropertiesDataClient _propertiesData;

        public IPropertiesNotificationClient PropertiesNotifications { get { return _propertiesNotifications;} }
        private readonly IPropertiesNotificationClient _propertiesNotifications;

        public IContentDataClient ContentData { get { return _contentData; } }
        private readonly IContentDataClient _contentData;

        public IContentNotificationClient ContentNotifications { get { return _contentNotifications; } }
        private readonly IContentNotificationClient _contentNotifications;

        protected ClientContext(
            IAuthenticationDataClient authenticationData,
            IRootDataClient rootData,
            IRootNotificationClient rootNotifications,
            IEntryDataClient entryData,
            IEntryNotificationClient entryNotifications,
            IPropertiesDataClient propertiesData,
            IPropertiesNotificationClient propertiesNotifications,
            IContentDataClient contentData,
            IContentNotificationClient contentNotifications
            )
        {
            _authenticationData = authenticationData;

            _rootNotifications = rootNotifications;
            _rootData = rootData;

            _entryData = entryData;
            _entryNotifications = entryNotifications;

            _propertiesData = propertiesData;
            _propertiesNotifications = propertiesNotifications;

            _contentData = contentData;
            _contentNotifications = contentNotifications;
        }

        public async Task Open(ITransport transport)
        {
            await _authenticationData.Connect(transport);

            await _rootData.Connect(transport);
            _rootNotifications.Connect(transport);

            await _entryData.Connect(transport);
            _entryNotifications.Connect(transport);

            await _propertiesData.Connect(transport);
            _propertiesNotifications.Connect(transport);

            await _contentData.Connect(transport);
            _contentNotifications.Connect(transport);
        }

        public async Task Close(ITransport transport)
        {
            await _authenticationData.Disconnect(transport);

            await _rootData.Disconnect(transport);
            _rootNotifications.Disconnect(transport);

            await _entryData.Disconnect(transport);
            _entryNotifications.Disconnect(transport);

            await _propertiesData.Disconnect(transport);
            _propertiesNotifications.Disconnect(transport);

            await _contentData.Disconnect(transport);
            _contentNotifications.Disconnect(transport);

        }
    }
}
