namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using EtAlii.Ubigia.Api.Functional;

    public class MicrosoftGraphProvider : IProvider
    {
        private readonly ISystemSettingsProvider _systemSettingsProvider;
        private readonly IImporter[] _importers;

        public IProviderConfiguration Configuration => _configuration;
        private readonly IProviderConfiguration _configuration;

        public MicrosoftGraphProvider(
            IProviderConfiguration configuration, 
            ISystemSettingsProvider systemSettingsProvider,
            IMailImporter mailImporter,
            ICalendarImporter calendarImporter,
            IPeopleImporter peopleImporter,
            IOneDriveImporter oneDriveImporter,
            IOneNoteImporter oneNoteImporter)
        {
            _configuration = configuration;
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
