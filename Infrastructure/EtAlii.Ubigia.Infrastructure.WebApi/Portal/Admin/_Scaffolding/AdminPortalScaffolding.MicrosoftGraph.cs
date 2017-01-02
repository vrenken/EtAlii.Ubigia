namespace EtAlii.Ubigia.Infrastructure.WebApi.Portal.Admin
{
    using EtAlii.Ubigia.Infrastructure;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Provisioning.Microsoft.Graph;
    using SimpleInjector;

    internal partial class AdminPortalScaffolding : IScaffolding
    {
  
        public void RegisterForMicrosoftGraph(Container container)
        {
            container.Register<ISystemSettingsGetter, SystemSettingsGetter>(Lifestyle.Singleton);
            container.Register<ISystemSettingsSetter, SystemSettingsSetter>(Lifestyle.Singleton);
        }
    }
}