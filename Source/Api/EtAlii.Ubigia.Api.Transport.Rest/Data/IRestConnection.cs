namespace EtAlii.Ubigia.Api.Transport.Rest
{
    public interface IRestConnection : IConnection
    {
        IInfrastructureClient Client { get; }
        IAddressFactory AddressFactory { get; }
    }
}
