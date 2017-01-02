namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    public class CollectionBase<T>
        where T: ConnectionBase
    {
        protected Infrastructure Infrastructure { get { return _infrastructure.Value; } }
        private readonly Lazy<Infrastructure> _infrastructure;

        protected AddressFactory AddressFactory { get { return _addressFactory.Value; } }
        private readonly Lazy<AddressFactory> _addressFactory;

        protected AccountResolver AccountResolver { get { return _accountResolver.Value; } }
        private readonly Lazy<AccountResolver> _accountResolver;

        protected StorageResolver StorageResolver { get { return _storageResolver.Value; } }
        private readonly Lazy<StorageResolver> _storageResolver;

        protected SpaceResolver SpaceResolver { get { return _spaceResolver.Value; } }
        private readonly Lazy<SpaceResolver> _spaceResolver;

        protected RootResolver RootResolver { get { return _rootResolver.Value; } }
        private readonly Lazy<RootResolver> _rootResolver;

        protected EntryResolver EntryResolver { get { return _entryResolver.Value; } }
        private readonly Lazy<EntryResolver> _entryResolver;

        protected T Connection { get { return _connection; } }
        private readonly T _connection;

        internal CollectionBase(T connection)
        {
            _connection = connection;

            _infrastructure = new Lazy<Infrastructure>(GetInfrastructure);
            _addressFactory = new Lazy<AddressFactory>(GetAddressFactory);

            _storageResolver = new Lazy<StorageResolver>(GetStorageResolver);
            _accountResolver = new Lazy<AccountResolver>(GetAccountResolver);
            _spaceResolver = new Lazy<SpaceResolver>(GetSpaceResolver);
            _rootResolver = new Lazy<RootResolver>(GetRootResolver);
            _entryResolver = new Lazy<EntryResolver>(GetEntryResolver);
        }

        private Infrastructure GetInfrastructure()
        {
            return Connection.Infrastructure;
        }

        private AddressFactory GetAddressFactory()
        {
            return Connection.AddressFactory;
        }

        private StorageResolver GetStorageResolver()
        {
            return Connection.StorageResolver;
        }

        private AccountResolver GetAccountResolver()
        {
            return Connection.AccountResolver;
        }

        private SpaceResolver GetSpaceResolver()
        {
            return Connection.SpaceResolver;
        }

        private RootResolver GetRootResolver()
        {
            return Connection.RootResolver;
        }

        private EntryResolver GetEntryResolver()
        {
            return Connection.EntryResolver;
        }
    }
}
