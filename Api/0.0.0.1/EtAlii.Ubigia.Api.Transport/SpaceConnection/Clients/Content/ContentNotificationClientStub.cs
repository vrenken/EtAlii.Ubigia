namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Threading.Tasks;

    public class ContentNotificationClientStub : IContentNotificationClient
    {
        public event Action<Identifier> Updated = delegate { };
        public event Action<Identifier> Stored = delegate { };

        public Task Connect(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

        public Task Disconnect(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }
    }
}
