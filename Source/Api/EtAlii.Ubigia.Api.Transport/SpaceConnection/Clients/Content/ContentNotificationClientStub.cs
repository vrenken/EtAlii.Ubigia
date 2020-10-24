namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Threading.Tasks;

    public class ContentNotificationClientStub : IContentNotificationClient
    {
        /// <inheritdoc />
        public event Action<Identifier> Updated = delegate { };

        /// <inheritdoc />
        public event Action<Identifier> Stored = delegate { };

        /// <inheritdoc />
        public Task Connect(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task Disconnect()
        {
            return Task.CompletedTask;
        }
    }
}
