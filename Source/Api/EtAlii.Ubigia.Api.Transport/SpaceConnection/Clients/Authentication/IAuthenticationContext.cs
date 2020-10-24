namespace EtAlii.Ubigia.Api.Transport
{
    public interface IAuthenticationContext : ISpaceClientContext
    {
        IAuthenticationDataClient Data { get; }
    }
}