namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public interface IConnection
    {
        // TODO: is a must.
        //Account Account { get; }
        bool IsConnected { get; }

        IInfrastructureClient Client { get; }
        IAddressFactory AddressFactory { get; }
        ITransport Transport { get; }

        Task Close();
        Task Open();
    }
}