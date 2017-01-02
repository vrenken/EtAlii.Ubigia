namespace EtAlii.Servus.Api.Transport
{
    public interface IDataConnectionContext
    {
        IDataConnection DataConnection { get; }
        IAddressFactory AddressFactory { get; }
        IInfrastructureClient Client { get; }

        void Initialize(IDataConnection dataConnection);
    }
}