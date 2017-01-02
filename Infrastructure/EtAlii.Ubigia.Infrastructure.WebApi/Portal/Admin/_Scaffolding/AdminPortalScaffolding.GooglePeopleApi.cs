namespace EtAlii.Ubigia.Infrastructure.WebApi.Portal.Admin
{
    using EtAlii.Ubigia.Infrastructure;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Provisioning.Google.PeopleApi;
    using SimpleInjector;

    internal partial class AdminPortalScaffolding : IScaffolding
    {

        public void RegisterForGoogle(Container container)
        {
            container.Register<ISystemSettingsGetter, SystemSettingsGetter>(Lifestyle.Singleton);
            container.Register<ISystemSettingsSetter, SystemSettingsSetter>(Lifestyle.Singleton);
        }
    }
}