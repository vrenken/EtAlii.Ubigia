namespace EtAlii.Ubigia.Infrastructure.Transport
{
    public interface ISimpleAuthenticationTokenVerifier
    {
        void Verify(string authenticationTokenAsString, params string[] requiredRoles);
        void Verify(string authenticationTokenAsString, out Account account, params string[] requiredRoles);
    }
}