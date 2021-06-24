// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Threading.Tasks;

    public abstract class SpaceConnection<TTransport> : ISpaceConnection<TTransport>
        where TTransport : ISpaceTransport
    {
        public Storage Storage { get; private set; }

        public Space Space { get; private set; }

        public Account Account { get; private set; }

        ISpaceTransport ISpaceConnection.Transport => Transport;
        public TTransport Transport { get; }

        public bool IsConnected => Storage != null && Space != null;

        /// <summary>
        /// The Configuration used to instantiate this SpaceConnection.
        /// </summary>
        public ISpaceConnectionConfiguration Configuration { get; }

        public IRootContext Roots { get; }

        public IEntryContext Entries { get; }

        public IContentContext Content { get; }

        public IPropertiesContext Properties { get; }

        public IAuthenticationContext Authentication { get; }

        protected SpaceConnection(
            ISpaceTransport transport,
            ISpaceConnectionConfiguration configuration,
            IRootContext roots,
            IEntryContext entries,
            IContentContext content,
            IPropertiesContext properties,
            IAuthenticationContext authentication)
        {
            Transport = (TTransport)transport;
            Configuration = configuration;
            Roots = roots;
            Entries = entries;
            Content = content;
            Properties = properties;
            Authentication = authentication;
        }

        public async Task Close()
        {
            if (!IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyClosed);
            }

            await Content.Close(this).ConfigureAwait(false);
            await Properties.Close(this).ConfigureAwait(false);
            await Entries.Close(this).ConfigureAwait(false);
            await Roots.Close(this).ConfigureAwait(false);
            await Authentication.Close(this).ConfigureAwait(false);

            await Transport.Stop().ConfigureAwait(false);
            Storage = null;
            Space = null;
        }

        public async Task Open(string accountName, string password)
        {
            if (IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            await Authentication.Data.Authenticate(this, accountName, password).ConfigureAwait(false);

            Storage = await Authentication.Data.GetConnectedStorage(this).ConfigureAwait(false);

            await Authentication.Open(this).ConfigureAwait(false);
            await Roots.Open(this).ConfigureAwait(false);
            await Entries.Open(this).ConfigureAwait(false);
            await Properties.Open(this).ConfigureAwait(false);
            await Content.Open(this).ConfigureAwait(false);

            await Transport.Start().ConfigureAwait(false);

			Account = await Authentication.Data.GetAccount(this, accountName).ConfigureAwait(false);
	        Space = await Authentication.Data.GetSpace(this).ConfigureAwait(false);
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

            #pragma warning disable S1066
            if (disposing)
            {
                // Free other state (managed objects).
                if (IsConnected)
                {
                    var task = Close();
                    task.Wait(); // TODO: HIGH PRIORITY Refactor the dispose into a Disconnect or something similar.
                    Storage = null;
                }
            }
            #pragma warning restore S1066

            // Free your own state (unmanaged objects).
            // Set large fields to null.
            _disposed = true;
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
