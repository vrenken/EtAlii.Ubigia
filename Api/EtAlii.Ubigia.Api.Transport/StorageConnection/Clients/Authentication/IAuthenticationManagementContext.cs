namespace EtAlii.Ubigia.Api.Transport
{
    public interface IAuthenticationManagementContext : IStorageClientContext
    {
        IAuthenticationManagementDataClient Data { get; }
    }
}