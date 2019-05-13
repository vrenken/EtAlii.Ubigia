namespace EtAlii.Ubigia.Provisioning.Google
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Provisioning.Google.PeopleApi;

    public class GoogleProvider : IProvider
    {
        private readonly ISystemSettingsProvider _systemSettingsProvider;

        private readonly IImporter[] _importers;
        private readonly IUpdater[] _updaters;

        public IProviderConfiguration Configuration { get; }

        public GoogleProvider(
            IProviderConfiguration configuration, 
            ISystemSettingsProvider systemSettingsProvider,
            IMailImporter mailImporter,
            ICalendarImporter calendarImporter,
            IPeopleImporter peopleImporter,
            IPeopleApiUpdater peopleApiUpdater)
        {
            Configuration = configuration;
            _systemSettingsProvider = systemSettingsProvider;
            _importers = new IImporter[]
            {
                mailImporter,
                calendarImporter,
                peopleImporter,
            };

            _updaters = new IUpdater[]
            {
                peopleApiUpdater
            };
        }

        public async Task Stop()
        {
            foreach (var importer in _importers)
            {
                await importer.Stop();
            }
            foreach (var updater in _updaters)
            {
                await updater.Stop();
            }
        }

        public async Task Start()
        {
            await _systemSettingsProvider.Update();
            
            foreach (var updater in _updaters)
            {
                await updater.Start();
            }
            foreach (var importer in _importers)
            {
                await importer.Start();
            }
        }
    }
}
