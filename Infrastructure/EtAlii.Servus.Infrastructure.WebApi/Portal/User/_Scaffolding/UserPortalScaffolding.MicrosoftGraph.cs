namespace EtAlii.Servus.Infrastructure.WebApi.Portal.User
{
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Provisioning.Microsoft.Graph;
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