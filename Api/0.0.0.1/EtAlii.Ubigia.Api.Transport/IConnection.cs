namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public interface IConnection
    {
        Storage Storage { get; }

        // TODO: is a must.
        //Account Account { get; }
        bool IsConnected { get; }

        Task Close();
        Task Open();
    }
}