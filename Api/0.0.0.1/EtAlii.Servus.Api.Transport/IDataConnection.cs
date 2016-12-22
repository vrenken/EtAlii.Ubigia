namespace EtAlii.Servus.Api.Transport
{
    using System.Threading.Tasks;

    public interface IDataConnection
    {
        Storage Storage { get; }
        Account Account { get; }
        Space Space { get; }

        IEntryContext Entries { get; }
        IRootContext Roots { get; }
        IContentContext Content { get; }
        IPropertyContext Properties { get; }

        bool IsConnected { get; }
        IDataConnectionConfiguration Configuration { get; }

        Task Open();
        Task Close();
    }
}
