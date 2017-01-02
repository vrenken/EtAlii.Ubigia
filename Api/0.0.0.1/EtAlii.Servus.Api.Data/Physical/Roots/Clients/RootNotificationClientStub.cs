namespace EtAlii.Servus.Api.Data
{
    using System;

    public class RootNotificationClientStub : IRootNotificationClient
    {
        public event Action<Guid> Added = delegate { };
        public event Action<Guid> Changed = delegate { };
        public event Action<Guid> Removed = delegate { };

        public RootNotificationClientStub()
        {
        }
        
        public void Connect()
        {
        }
        
        public void Disconnect()
        {
        }
    }
}
