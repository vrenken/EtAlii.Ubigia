namespace EtAlii.Servus.Infrastructure.WebApi.Portal.User
{
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Infrastructure.WebApi.Portal.Admin;
    using EtAlii.Servus.Provisioning.Google.PeopleApi;
    using SimpleInjector;

    internal partial class UserPortalScaffolding : IScaffolding
    {

        public void RegisterForGoogle(Container container)
        {
            container.Register<IUserSettingsGetter, UserSettingsGetter>(Lifestyle.Singleton);
            container.Register<IUserSettingsSetter, UserSettingsSetter>(Lifestyle.Singleton);
            container.Register<IUserSettingsClearer, UserSettingsClearer>(Lifestyle.Singleton);

            container.Register<IUserSettingsPostHandler, UserSettingsPostHandler>(Lifestyle.Singleton);
            container.Register<IUserSettingsGetHandler, UserSettingsGetHandler>(Lifestyle.Singleton);
            container.Register<IUserSettingsDeleteHandler, UserSettingsDeleteHandler>(Lifestyle.Singleton);
            container.Register<ISystemSettingsGetHandler, SystemSettingsGetHandler>(Lifestyle.Singleton);

            container.Register<IGoogleAuthenticationTokenProvider, GoogleAuthenticationTokenProvider>(Lifestyle.Singleton);
            container.Register<IGoogleIdentityProvider, GoogleIdentityProvider>(Lifestyle.Singleton);
            container.Register<IGoogleNameConverter, GoogleNameConverter>(Lifestyle.Singleton);
            container.Register<IGoogleMailAddressConverter, GoogleMailAddressConverter>(Lifestyle.Singleton);
        }
    }
}