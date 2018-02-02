namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin
{
    using EtAlii.Ubigia.Provisioning.Microsoft.Graph;
    using EtAlii.xTechnology.MicroContainer;

    internal partial class AdminPortalScaffolding : IScaffolding
    {
  
        public void RegisterForMicrosoftGraph(Container container)
        {
            container.Register<ISystemSettingsGetter, SystemSettingsGetter>();
            container.Register<ISystemSettingsSetter, SystemSettingsSetter>();
        }
    }
}