namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin
{
    public interface IGoogleAuthenticationTokenProvider
    {
        GoogleAuthenticationToken GetRefreshToken(string authorizationCode);
    }
}