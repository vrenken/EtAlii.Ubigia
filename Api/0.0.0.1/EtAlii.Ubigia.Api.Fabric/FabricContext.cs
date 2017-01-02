namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using EtAlii.Ubigia.Api.Transport;

    public class FabricContext : IFabricContext
    {
        public IFabricContextConfiguration Configuration { get { return _configuration; } }
        private readonly IFabricContextConfiguration _configuration;

        public IDataConnection Connection { get { return _connection; } }
        private readonly IDataConnection _connection;

        public IRootContext Roots { get { return _roots; } }
        private readonly IRootContext _roots;

        public IEntryContext Entries { get { return _entries; } }
        private readonly IEntryContext _entries;

        public IContentContext Content { get { return _content; } }
        private readonly IContentContext _content;

        public IPropertyContext Properties { get { return _properties; } }
        private readonly IPropertyContext _properties;

        public FabricContext(
            IFabricContextConfiguration configuration,
            IEntryContext entries,
            IRootContext roots,
            IContentContext content, 
            IDataConnection connection, 
            IPropertyContext properties)
        {
            _configuration = configuration;
            _entries = entries;
            _roots = roots;
            _content = content;
            _connection = connection;
            _properties = properties;
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
                    _connection.Close();
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                _disposed = true;
            }
        }

        // Use C# destructor syntax for finalization code.
        ~FabricContext()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion Disposable
    }
}
