namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Threading.Tasks;

    public class MicrosoftGraphProvider : IProvider
    {
        private readonly ISystemSettingsProvider _systemSettingsProvider;
        private readonly IImporter[] _importers;

        /// <summary>
        /// The Configuration used to instantiate this Provider.
        /// </summary>
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

        public async Task Stop()
        {
            foreach (var importer in _importers)
            {
                await importer.Stop().ConfigureAwait(false);
            }
        }

        public async Task Start()
        {
            await _systemSettingsProvider.Update().ConfigureAwait(false);

            foreach (var importer in _importers)
            {
                await importer.Start().ConfigureAwait(false);
            }
        }
    }
}
