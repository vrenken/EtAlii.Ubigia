namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.Ubigia.Api;

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
