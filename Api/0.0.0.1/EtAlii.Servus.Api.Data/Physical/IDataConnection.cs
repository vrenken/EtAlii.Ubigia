namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;

    public interface IDataConnection
    {
        Storage Storage { get; }
        Account Account { get; }
        Space Space { get; }

        IRootContext Roots { get; }
        IEntryContext Entries { get; }
        IContentContext Content { get; }

        bool IsConnected { get; }

        void Open(string address, string accountName, string spaceName);
        void Open(string address, string accountName, string password, string spaceName);
        void Close();
    }
}
