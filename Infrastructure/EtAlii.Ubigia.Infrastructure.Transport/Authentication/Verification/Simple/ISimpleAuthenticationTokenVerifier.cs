namespace EtAlii.Ubigia.Infrastructure.Transport
{
    public interface ISimpleAuthenticationTokenVerifier
    {
        void Verify(string authenticationTokenAsString, params string[] requiredRoles);
    }
}