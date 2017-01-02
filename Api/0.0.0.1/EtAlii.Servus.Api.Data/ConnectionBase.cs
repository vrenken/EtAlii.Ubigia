namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    public class ConnectionBase 
    {
        internal Storage CurrentStorage { get; private set; }

        public Infrastructure Infrastructure { get { return _infrastructure.Value; } }
        private readonly Lazy<Infrastructure> _infrastructure;

        public AddressFactory AddressFactory { get { return _addressFactory.Value; } }
        private readonly Lazy<AddressFactory> _addressFactory;

        internal AccountResolver AccountResolver { get { return _accountResolver.Value; } }
        private readonly Lazy<AccountResolver> _accountResolver;

        internal StorageResolver StorageResolver { get { return _storageResolver.Value; } }
        private readonly Lazy<StorageResolver> _storageResolver;

        internal SpaceResolver SpaceResolver { get { return _spaceResolver.Value; } }
        private readonly Lazy<SpaceResolver> _spaceResolver;

        internal RootResolver RootResolver { get { return _rootResolver.Value; } }
        private readonly Lazy<RootResolver> _rootResolver;

        internal EntryResolver EntryResolver { get { return _entryResolver.Value; } }
        private readonly Lazy<EntryResolver> _entryResolver;

        public ConnectionBase()
        {
            _infrastructure = new Lazy<Infrastructure>(GetInfrastructure);
            _addressFactory = new Lazy<AddressFactory>(GetAddressFactory);

            _storageResolver = new Lazy<StorageResolver>(GetStorageResolver);
            _accountResolver = new Lazy<AccountResolver>(GetAccountResolver);
            _spaceResolver = new Lazy<SpaceResolver>(GetSpaceResolver);
            _rootResolver = new Lazy<RootResolver>(GetRootResolver);
            _entryResolver = new Lazy<EntryResolver>(GetEntryResolver);
        }

        public virtual void Close()
        {
            if (CurrentStorage == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyClosed);
            }
            CurrentStorage = null;
        }

        protected virtual void Open(string address, string accountName, string password)
        {
            if (CurrentStorage != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            CurrentStorage = GetStorage(address, accountName, password);
        }

        private Storage GetStorage(string address, string accountName, string password)
        {
            var storage = (Storage)null;

            string authenticationToken = Authenticate(address, accountName, password);

            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                Infrastructure.AuthenticationToken = authenticationToken;

                address = BuildFullAddress(address, "management/storage?local");
                storage = Infrastructure.Get<Storage>(address);
            }
            else
            {
                string message = String.Format("Unable to connect to the specified storage ({0})", address);
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return storage;
        }

        protected string Authenticate(string address, string accountName, string password)
        {
            var credentials = new NetworkCredential(accountName, password);
            address = BuildFullAddress(address, "authenticate");
            //var address = BuildFullAddress("authenticate");
            var authenticationToken = Infrastructure.Get<string>(address, credentials);
            return authenticationToken;
        }

        protected string BuildFullAddress(string address, params string[] fragments)
        {
            var builder = new UriBuilder(address);
            builder.Path = String.Join("/", fragments);
            return builder.ToString();
        }

        private Infrastructure GetInfrastructure()
        {
            return new Infrastructure();
        }

        private AddressFactory GetAddressFactory()
        {
            return new AddressFactory();
        }

        private StorageResolver GetStorageResolver()
        {
            return new StorageResolver(Infrastructure, AddressFactory);
        }

        private AccountResolver GetAccountResolver()
        {
            return new AccountResolver(Infrastructure, AddressFactory);
        }

        private SpaceResolver GetSpaceResolver()
        {
            return new SpaceResolver(Infrastructure, AddressFactory, AccountResolver);
        }

        private RootResolver GetRootResolver()
        {
            return new RootResolver(Infrastructure, AddressFactory, SpaceResolver);
        }

        private EntryResolver GetEntryResolver()
        {
            return new Api.EntryResolver(Infrastructure, AddressFactory, SpaceResolver);
        }
    }
}
