namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;

    /// <summary>
    /// A stub to use disabled entry notifications.
    /// </summary>
    public class EntryNotificationClientStub : IEntryNotificationClient
    {
        public event Action<Identifier> Prepared = delegate { };
        public event Action<Identifier> Stored = delegate { };

        public EntryNotificationClientStub()
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
