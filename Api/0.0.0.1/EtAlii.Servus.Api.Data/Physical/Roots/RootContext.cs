namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    public class RootContext : IRootContext
    {
        public event Action<Guid> Added = delegate { };
        public event Action<Guid> Changed = delegate { };
        public event Action<Guid> Removed = delegate { };

        private readonly IRootNotificationClient _notificationClient;
        private readonly IRootDataClient _dataClient;
        private readonly IConnectionStatusProvider _connectionStatusProvider;

        internal RootContext(
            IRootNotificationClient notificationClient, 
            IRootDataClient dataClient,
            IConnectionStatusProvider connectionStatusProvider)
        {
            _notificationClient = notificationClient;
            _notificationClient.Added += OnAdded;
            _notificationClient.Changed += OnChanged;
            _notificationClient.Removed += OnRemoved;

            _dataClient = dataClient;
            _connectionStatusProvider = connectionStatusProvider;
        }

        public Root Add(string name) 
        {
            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            return _dataClient.Add(name);
        }

        public void Remove(Guid id)
        {
            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            _dataClient.Remove(id);
        }

        public Root Change(Guid rootId, string rootName)
        {
            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            return _dataClient.Change(rootId, rootName);
        }

        public Root Get(string rootName)
        {
            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            return _dataClient.Get(rootName);
        }

        public Root Get(Guid rootId)
        {
            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            return _dataClient.Get(rootId);
        }

        public IEnumerable<Root> GetAll()
        {
            if (!_connectionStatusProvider.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }

            return _dataClient.GetAll();
        }


        private void OnAdded(Guid id)
        {
            Added(id);
        }

        private void OnChanged(Guid id)
        {
            Changed(id);
        }

        private void OnRemoved(Guid id)
        {
            Removed(id);
        }
    }
}
