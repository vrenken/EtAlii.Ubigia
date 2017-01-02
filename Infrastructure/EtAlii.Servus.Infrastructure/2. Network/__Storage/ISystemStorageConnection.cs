namespace EtAlii.Servus.Infrastructure
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;

    public interface ISystemStorageConnection
    {
        Storage Storage { get; }
        bool IsConnected { get; }

        IInfrastructureClient Client { get; }
        IAddressFactory AddressFactory { get; }
        ITransport Transport { get; }

        //Task Close();
        //Task Open(string accountName);
        //Task Open();

    }
}