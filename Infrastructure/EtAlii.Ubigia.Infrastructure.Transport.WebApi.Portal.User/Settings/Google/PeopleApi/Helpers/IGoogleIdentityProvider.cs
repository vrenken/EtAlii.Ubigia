namespace EtAlii.Ubigia.Infrastructure.WebApi.Portal.Admin
{
    public interface IGoogleIdentityProvider
    {
        GoogleIdentity GetGoogleIdentity(GoogleAuthenticationToken googleAuthenticationToken);
    }
}