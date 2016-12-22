namespace EtAlii.Servus.Infrastructure.WebApi.Portal.Admin
{
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Provisioning.Microsoft.Graph;
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