namespace EtAlii.Ubigia.Provisioning.Twitter
{
    public class TwitterProvider : IProvider
    {
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

        public void Stop()
        {
            foreach (var importer in _importers)
            {
                importer.Stop();
            }
        }

        public void Start()
        {
            foreach (var importer in _importers)
            {
                importer.Start();
            }
        }
    }
}
