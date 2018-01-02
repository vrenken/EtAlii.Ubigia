namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR.AspNetCore
{
    public interface ISignalRAuthenticationVerifier
    {
        string Verify(string accountName, string password, string hostIdentifier);
    }
}