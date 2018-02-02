namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.User
{
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin;
    using EtAlii.Ubigia.Provisioning.Google.PeopleApi;
    using EtAlii.xTechnology.MicroContainer;

    internal partial class UserPortalScaffolding : IScaffolding
    {

        public void RegisterForGoogle(Container container)
        {
            container.Register<IUserSettingsGetter, UserSettingsGetter>();
            container.Register<IUserSettingsSetter, UserSettingsSetter>();
            container.Register<IUserSettingsClearer, UserSettingsClearer>();

            container.Register<IUserSettingsPostHandler, UserSettingsPostHandler>();
            container.Register<IUserSettingsGetHandler, UserSettingsGetHandler>();
            container.Register<IUserSettingsDeleteHandler, UserSettingsDeleteHandler>();
            container.Register<ISystemSettingsGetHandler, SystemSettingsGetHandler>();

            container.Register<IGoogleAuthenticationTokenProvider, GoogleAuthenticationTokenProvider>();
            container.Register<IGoogleIdentityProvider, GoogleIdentityProvider>();
            container.Register<IGoogleNameConverter, GoogleNameConverter>();
            container.Register<IGoogleMailAddressConverter, GoogleMailAddressConverter>();
        }
    }
}