namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using EtAlii.Ubigia.Api.Transport;

    public class FabricContext : IFabricContext
    {
        /// <summary>
        /// The Configuration used to instantiate this Context.
        /// </summary>
        public IFabricContextConfiguration Configuration { get; }

        public IDataConnection Connection { get; }

        public IRootContext Roots { get; }

        public IEntryContext Entries { get; }

        public IContentContext Content { get; }

        public IPropertiesContext Properties { get; }

        public FabricContext(
            IFabricContextConfiguration configuration,
            IEntryContext entries,
            IRootContext roots,
            IContentContext content, 
            IDataConnection connection, 
            IPropertiesContext properties)
        {
            Configuration = configuration;
            Entries = entries;
            Roots = roots;
            Content = content;
            Connection = connection;
            Properties = properties;
        }

        #region Disposable

        private bool _disposed;

        //Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            
            if (disposing)
            {
                try
                {
                    // Free other state (managed objects).
                    if (Connection.IsConnected)
                    {
                        var task = Connection.Close();
                        task.Wait(); // TODO: HIGH PRIORITY Refactor the dispose into a Disconnect or something similar. 
                    }
                }
                catch //(Exception e)
                {
                    //throw
                }
            }
            // Free your own state (unmanaged objects).
            // Set large fields to null.
            _disposed = true;
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
