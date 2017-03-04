namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Threading.Tasks;

    public abstract class SpaceConnection<TTransport> : ISpaceConnection<TTransport>
        where TTransport : ISpaceTransport
    {
        public Storage Storage { get { return _storage; } private set { _storage = value; } }
        private Storage _storage;

        public Space Space { get { return _space; } private set { _space = value; } }
        private Space _space;

        public Account Account { get { return _account; } private set { _account = value; } }
        private Account _account;

        public TTransport Transport => _transport;
        private readonly TTransport _transport;

        public bool IsConnected => _storage != null && _space != null;

        public ISpaceConnectionConfiguration Configuration => _configuration;
        private readonly ISpaceConnectionConfiguration _configuration;

        public IRootContext Roots => _roots;
        private readonly IRootContext _roots;

        public IEntryContext Entries => _entries;
        private readonly IEntryContext _entries;

        public IContentContext Content => _content;
        private readonly IContentContext _content;

        public IPropertyContext Properties => _properties;
        private readonly IPropertyContext _properties;

        public IAuthenticationContext Authentication => _authentication;
        private readonly IAuthenticationContext _authentication;

        protected SpaceConnection(
            ISpaceTransport transport,
            ISpaceConnectionConfiguration configuration, 
            IRootContext roots, 
            IEntryContext entries, 
            IContentContext content, 
            IPropertyContext properties, 
            IAuthenticationContext authentication)
        {
            _transport = (TTransport)transport;
            _configuration = configuration;
            _roots = roots;
            _entries = entries;
            _content = content;
            _properties = properties;
            _authentication = authentication;
        }

        public async Task Close()
        {
            if (!IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyClosed);
            }

            await _content.Close(this);
            await _properties.Close(this);
            await _entries.Close(this);
            await _roots.Close(this);
            await _authentication.Close(this);

            await _transport.Stop(this);
            _storage = null;
            _space = null;
        }

        public async Task Open()
        {
            if (IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            await _authentication.Data.Authenticate(this);

            _storage = await _authentication.Data.GetConnectedStorage(this);

            _transport.Initialize(this, _configuration.Address);

            await _authentication.Open(this);
            await _roots.Open(this);
            await _entries.Open(this);
            await _properties.Open(this);
            await _content.Open(this);

            await _transport.Start(this, _configuration.Address);

            _account = await _authentication.Data.GetAccount(this);
            _space = await _authentication.Data.GetSpace(this);
        }
        
        #region Disposable

        private bool _disposed = false;

        //Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                    if (IsConnected)
                    {
                        var task = Task.Run(async () =>
                        {
                            await Close();
                        });
                        task.Wait();
                        _storage = null;
                    }
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                _disposed = true;
            }
        }

        // Use C# destructor syntax for finalization code.
        ~SpaceConnection()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion Disposable
    }
}
