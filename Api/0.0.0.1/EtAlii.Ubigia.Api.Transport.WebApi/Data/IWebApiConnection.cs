namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    public interface IWebApiConnection : IConnection
    {
        IInfrastructureClient Client { get; }
        IAddressFactory AddressFactory { get; }
    }
}