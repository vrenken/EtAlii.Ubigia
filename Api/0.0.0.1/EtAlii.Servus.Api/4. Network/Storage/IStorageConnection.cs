namespace EtAlii.Servus.Api.Transport
{
    using System;
    using System.Threading.Tasks;

    public interface IStorageConnection : IDisposable
    {
        Storage Storage { get; }
        bool IsConnected  { get; }

        IInfrastructureClient Client { get; }
        IAddressFactory AddressFactory { get; }
        ITransport Transport { get; }

        Task Close();

        Task Open(string address);
        Task Open(string address, string accountName, string password);
    }
}
