namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Threading.Tasks;

    public class PropertiesNotificationClientStub : IPropertiesNotificationClient
    {
        public event Action<Identifier> Stored = delegate { };

        public Task Connect(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

        public Task Disconnect()
        {
            return Task.CompletedTask;
        }
    }
}
