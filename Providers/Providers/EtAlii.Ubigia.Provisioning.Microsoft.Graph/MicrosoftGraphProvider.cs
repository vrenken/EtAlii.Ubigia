namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    public class MicrosoftGraphProvider : IProvider
    {
        private readonly ISystemSettingsProvider _systemSettingsProvider;
        private readonly IImporter[] _importers;

        public IProviderConfiguration Configuration { get; }

        public MicrosoftGraphProvider(
            IProviderConfiguration configuration, 
            ISystemSettingsProvider systemSettingsProvider,
            IMailImporter mailImporter,
            ICalendarImporter calendarImporter,
            IPeopleImporter peopleImporter,
            IOneDriveImporter oneDriveImporter,
            IOneNoteImporter oneNoteImporter)
        {
            Configuration = configuration;
            _systemSettingsProvider = systemSettingsProvider;
            _importers = new IImporter[]
            {
                mailImporter,
                calendarImporter,
                peopleImporter,
                oneDriveImporter,
                oneNoteImporter
            };
        }

        public void Stop()
        {
            foreach (var importer in _importers)
            {
                importer.Stop();
            }
        }

        public void Start()
        {
            _systemSettingsProvider.Update();

            foreach (var importer in _importers)
            {
                importer.Start();
            }
        }
    }
}
