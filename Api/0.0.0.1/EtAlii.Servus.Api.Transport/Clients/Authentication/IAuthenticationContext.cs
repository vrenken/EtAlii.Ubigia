namespace EtAlii.Servus.Api.Transport
{
    public interface IAuthenticationContext : ISpaceClientContext
    {
        IAuthenticationDataClient Data { get; }
    }
}