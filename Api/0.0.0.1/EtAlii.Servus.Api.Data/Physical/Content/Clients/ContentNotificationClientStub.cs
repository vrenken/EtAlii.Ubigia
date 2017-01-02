namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;

    public class ContentNotificationClientStub : IContentNotificationClient
    {
        public event Action<Identifier> Updated = delegate { };
        public event Action<Identifier> Stored = delegate { };

        public ContentNotificationClientStub()
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
