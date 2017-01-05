namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin
{
    using EtAlii.Ubigia.Provisioning.Google.PeopleApi;
    using EtAlii.xTechnology.MicroContainer;

    internal partial class AdminPortalScaffolding : IScaffolding
    {

        public void RegisterForGoogle(Container container)
        {
            container.Register<ISystemSettingsGetter, SystemSettingsGetter>();
            container.Register<ISystemSettingsSetter, SystemSettingsSetter>();
        }
    }
}