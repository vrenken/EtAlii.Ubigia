namespace EtAlii.Ubigia.Infrastructure.WebApi.Portal.User
{
    using EtAlii.Ubigia.Infrastructure;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Provisioning.Microsoft.Graph;
    using SimpleInjector;

    internal partial class UserPortalScaffolding : IScaffolding
    {
        public void RegisterForMicrosoftGraph(Container container)
        {
            container.Register<IUserSettingsGetter, UserSettingsGetter>(Lifestyle.Singleton);
            container.Register<IUserSettingsSetter, UserSettingsSetter>(Lifestyle.Singleton);
            container.Register<IUserSettingsClearer, UserSettingsClearer>(Lifestyle.Singleton);
        }
    }
}