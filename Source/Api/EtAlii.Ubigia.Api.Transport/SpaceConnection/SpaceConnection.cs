// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Threading.Tasks;

    public abstract class SpaceConnection<TTransport> : ISpaceConnection<TTransport>
        where TTransport : ISpaceTransport
    {
        private bool _disposed;

        public Storage Storage { get; private set; }

        /// <inheritdoc />
        public Space Space { get; private set; }

        public Account Account { get; private set; }

        ISpaceTransport ISpaceConnection.Transport => Transport;

        public TTransport Transport { get; }

        public bool IsConnected => Storage != null && Space != null;

        /// <inheritdoc />
        public SpaceConnectionOptions Options { get; }

        /// <inheritdoc />
        public IRootContext Roots { get; }

        /// <inheritdoc />
        public IEntryContext Entries { get; }

        /// <inheritdoc />
        public IContentContext Content { get; }

        /// <inheritdoc />
        public IPropertiesContext Properties { get; }

        public IAuthenticationContext Authentication { get; }

        protected SpaceConnection(
            ISpaceTransport transport,
            SpaceConnectionOptions options,
            IRootContext roots,
            IEntryContext entries,
            IContentContext content,
            IPropertiesContext properties,
            IAuthenticationContext authentication)
        {
            Transport = (TTransport)transport;
            Options = options;
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

        public async ValueTask DisposeAsync()
        {
            if (_disposed) return;
            GC.SuppressFinalize(this);

            if (IsConnected)
            {
                await Close().ConfigureAwait(false);
                Storage = null;
            }

            _disposed = true;
        }
    }
}
