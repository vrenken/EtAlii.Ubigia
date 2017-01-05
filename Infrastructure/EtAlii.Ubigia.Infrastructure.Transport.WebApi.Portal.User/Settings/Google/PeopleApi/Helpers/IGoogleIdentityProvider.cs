namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin
{
    public interface IGoogleIdentityProvider
    {
        GoogleIdentity GetGoogleIdentity(GoogleAuthenticationToken googleAuthenticationToken);
    }
}