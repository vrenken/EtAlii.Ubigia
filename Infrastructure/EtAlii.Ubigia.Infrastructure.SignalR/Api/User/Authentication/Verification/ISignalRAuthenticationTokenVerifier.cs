namespace EtAlii.Ubigia.Infrastructure.SignalR
{
    public interface ISignalRAuthenticationTokenVerifier
    {
        void Verify(string authenticationTokenAsString, string requiredRole);
    }
}