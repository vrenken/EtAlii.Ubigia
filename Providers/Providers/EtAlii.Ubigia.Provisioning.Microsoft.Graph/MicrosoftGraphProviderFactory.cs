namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using EtAlii.xTechnology.MicroContainer;

    public class MicrosoftGraphProviderFactory : IProviderFactory
    {
        public IProvider Create(IProviderConfiguration configuration)
        {
            var container = new Container();

            container.Register(() => configuration);
            container.Register(() => configuration.SystemScriptContext);
            container.Register(() => configuration.ManagementConnection);
            container.Register<IProviderContext, ProviderContext>();
            container.Register<IProvider, MicrosoftGraphProvider>();

            container.Register<ISystemSettingsProvider, SystemSettingsProvider>();
            container.Register<ISystemSettingsGetter, SystemSettingsGetter>();
            container.Register<ISystemSettingsSetter, SystemSettingsSetter>();

            container.Register<IMailImporter, MailImporter>();
            container.Register<ICalendarImporter, CalendarImporter>();
            container.Register<IPeopleImporter, PeopleImporter>();
            container.Register<IOneDriveImporter, OneDriveImporter>();
            container.Register<IOneNoteImporter, OneNoteImporter>();

            container.Register(() => configuration.LogFactory.Create("Microsoft.Graph", "Provider"));

            return container.GetInstance<IProvider>();
        }
    }
}
