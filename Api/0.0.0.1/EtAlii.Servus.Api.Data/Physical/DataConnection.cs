namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DataConnection : IDataConnection
    {
        public bool IsConnected { get { return _connectionStatusProvider.IsConnected; } }

        public Storage Storage { get { return _connection.Storage; } }

        public Space Space { get; private set; }
        public Account Account { get; private set; }

        public IRootContext Roots { get { return _roots; } }
        private readonly IRootContext _roots;

        public IEntryContext Entries { get { return _entries; } }
        private readonly IEntryContext _entries;

        public IContentContext Content { get { return _content; } }
        private readonly IContentContext _content;

        private readonly IStorageConnection _connection;
        private readonly IDataTransport _dataTransport;
        private readonly INotificationTransport _notificationTransport;
        private readonly IInfrastructureClient _infrastructure;
        private readonly IAddressFactory _addressFactory;
        private readonly IConnectionStatusProvider _connectionStatusProvider;

        internal DataConnection(
            IStorageConnection connection,
            IDataTransport dataTransport,
            INotificationTransport notificationTransport,
            IAddressFactory addressFactory,
            IInfrastructureClient client,
            IRootContext roots,
            IEntryContext entries,
            IContentContext content,
            IConnectionStatusProvider connectionStatusProvider)
        {
            _connection = connection;
            _dataTransport = dataTransport;
            _notificationTransport = notificationTransport;

            _addressFactory = addressFactory;
            _infrastructure = client;

            _roots = roots;
            _entries = entries;
            _content = content;
            _connectionStatusProvider = connectionStatusProvider;
        }

        public void Open(string address, string accountName, string spaceName)
        {
            if (IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            _connection.Open(address, accountName);
            InternalOpen(accountName, spaceName);
        }

        public void Open(string address, string accountName, string password, string spaceName)
        {
            _connection.Open(address, accountName, password);
            InternalOpen(accountName, spaceName);
        }

        protected void InternalOpen(string accountName, string spaceName)
        {
            if (Account != null || Space != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            Account = GetAccount(accountName);
            if (Account == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectUsingAccount);
            }

            Space = GetSpace(Account, spaceName);
            if (Space == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToSpace);
            }
        }

        public void Close()
        {
            if (Account == null || Space == null)
            {
                throw new InvalidInfrastructureOperationException("The connection is already closed");
            }

            Account = null;
            Space = null;

            _connection.Close();
        }

        private Account GetAccount(string accountName)
        {
            var address = _addressFactory.Create(Storage, RelativeUri.Accounts, UriParameter.AccountName, accountName);
            var account = _infrastructure.Get<Account>(address);
            if (account == null)
            {
                string message = String.Format("Unable to connect using the specified account ({0})", accountName);
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return account;
        }

        private Space GetSpace(Account currentAccount, string spaceName)
        {
            var address = _addressFactory.Create(Storage, RelativeUri.Spaces, UriParameter.AccountId, currentAccount.Id.ToString());
            var spaces = _infrastructure.Get<IEnumerable<Space>>(address);
            return spaces.FirstOrDefault(s => s.Name == spaceName);
        }

    }
}
