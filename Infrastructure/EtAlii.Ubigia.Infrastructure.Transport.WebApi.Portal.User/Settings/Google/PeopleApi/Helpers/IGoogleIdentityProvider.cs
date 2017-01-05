namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi.Portal.Admin
{
    public interface IGoogleIdentityProvider
    {
        GoogleIdentity GetGoogleIdentity(GoogleAuthenticationToken googleAuthenticationToken);
    }
}