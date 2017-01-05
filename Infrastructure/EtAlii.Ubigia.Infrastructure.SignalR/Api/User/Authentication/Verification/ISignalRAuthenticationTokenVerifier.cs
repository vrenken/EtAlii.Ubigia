namespace EtAlii.Ubigia.Infrastructure.Transport.SignalR
{
    public interface ISignalRAuthenticationTokenVerifier
    {
        void Verify(string authenticationTokenAsString, string requiredRole);
    }
}