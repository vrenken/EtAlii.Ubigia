namespace EtAlii.Ubigia.Api.Transport
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
        IPropertiesContext Properties { get; }

        bool IsConnected { get; }
        /// <summary>
        /// The Configuration used to instantiate this DataConnection.
        /// </summary>
        IDataConnectionConfiguration Configuration { get; }

        Task Open();
        Task Close();
    }
}
