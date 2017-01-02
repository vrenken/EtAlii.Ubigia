namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    public class EntryContext : IEntryContext
    {
        private readonly IEntryNotificationClient _notificationClient;
        private readonly IEntryDataClient _dataClient;
        private readonly IConnectionStatusProvider _connectionStatusProvider;

        internal EntryContext(
            IEntryNotificationClient notificationClient, 
            IEntryDataClient dataClient,
            IConnectionStatusProvider connectionStatusProvider)
        {
            _notificationClient = notificationClient;
            _notificationClient.Prepared += OnPrepared;
            _notificationClient.Stored += OnStored;
            _dataClient = dataClient;
            _connectionStatusProvider = connectionStatusProvider;
        }

        public IEditableEntry Prepare()
        {
            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return _dataClient.Prepare();
        }

        public IReadOnlyEntry Change(IEditableEntry entry)
        {
            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return _dataClient.Change(entry);
        }

        public IReadOnlyEntry Get(Root root)
        {
            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return _dataClient.Get(root, EntryRelation.All);
        }

        public IReadOnlyEntry Get(Identifier identifier)
        {
            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return _dataClient.Get(identifier, EntryRelation.All);
        }

        public IEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> identifiers)
        {
            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return _dataClient.Get(identifiers, EntryRelation.All);
        }

        public IEnumerable<IReadOnlyEntry> GetRelated(Identifier identifier, EntryRelation relations)
        {
            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            return _dataClient.GetRelated(identifier, relations, EntryRelation.All);
        }


        public event Action<Identifier> Prepared = delegate { };
        public event Action<Identifier> Stored = delegate { };

        private void OnPrepared(Identifier identifier)
        {
            Prepared(identifier);
        }

        private void OnStored(Identifier identifier)
        {
            Stored(identifier);
        }
    }
}
