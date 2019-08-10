namespace EtAlii.Ubigia.Provisioning.Twitter
{
    using System.Threading.Tasks;

    public class TwitterProvider : IProvider
    {
        /// <summary>
        /// The Configuration used to instantiate this Provider.
        /// </summary>
        public IProviderConfiguration Configuration { get; }

        private readonly IImporter[] _importers;

        public TwitterProvider(
            IProviderConfiguration configuration, 
            ITweetImporter tweetImporter)
        {
            Configuration = configuration;

            _importers = new IImporter[]
            {
                tweetImporter,
            };
        }

        public Task Stop()
        {
            foreach (var importer in _importers)
            {
                importer.Stop();
            }
            return Task.CompletedTask;
        }

        public async Task Start()
        {
            foreach (var importer in _importers)
            {
                await importer.Start();
            }
        }
    }
}
