namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi.Portal.Admin
{
    public interface IGoogleAuthenticationTokenProvider
    {
        GoogleAuthenticationToken GetRefreshToken(string authorizationCode);
    }
}