namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR.AspNetCore
{
    public interface ISignalRAuthenticationTokenVerifier
    {
        void Verify(string authenticationTokenAsString, string requiredRole);
    }
}