namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Threading.Tasks;

    public class ContentNotificationClientStub : IContentNotificationClient
    {
        public event Action<Identifier> Updated = delegate { };
        public event Action<Identifier> Stored = delegate { };

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Task.CompletedTask;
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Task.CompletedTask;
        }
    }
}
