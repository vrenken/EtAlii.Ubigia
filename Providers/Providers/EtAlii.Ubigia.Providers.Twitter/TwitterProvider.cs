namespace EtAlii.Ubigia.Provisioning.Twitter
{
    using EtAlii.Ubigia.Api.Functional;

    public class TwitterProvider : IProvider
    {
        public IProviderConfiguration Configuration { get { return _configuration; } }
        private readonly IProviderConfiguration _configuration;

        private readonly IImporter[] _importers;

        public TwitterProvider(
            IProviderConfiguration configuration, 
            ITweetImporter tweetImporter)
        {
            _configuration = configuration;

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
