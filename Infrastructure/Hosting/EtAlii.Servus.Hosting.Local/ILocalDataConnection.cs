namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Api;

    public interface ILocalDataConnection
    {
        IRootContext Roots { get; }
        IEntryContext Entries { get; }
        IContentContext Content { get; }

        bool IsConnected { get; }

        void Open();
        void Close();
    }
}
