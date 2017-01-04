namespace EtAlii.Ubigia.Infrastructure.WebApi.Portal.User
{
    using EtAlii.Ubigia.Provisioning.Microsoft.Graph;
    using EtAlii.xTechnology.MicroContainer;

    internal partial class UserPortalScaffolding : IScaffolding
    {
        public void RegisterForMicrosoftGraph(Container container)
        {
            container.Register<IUserSettingsGetter, UserSettingsGetter>();
            container.Register<IUserSettingsSetter, UserSettingsSetter>();
            container.Register<IUserSettingsClearer, UserSettingsClearer>();
        }
    }
}