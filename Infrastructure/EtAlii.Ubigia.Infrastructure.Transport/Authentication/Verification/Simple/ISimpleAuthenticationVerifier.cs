namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.Ubigia.Api;

    public interface ISimpleAuthenticationVerifier
    {
        string Verify(string accountName, string password, string hostIdentifier, params string[] requiredRoles);
        string Verify(string accountName, string password, string hostIdentifier, out Account account, params string[] requiredRoles);
    }
}