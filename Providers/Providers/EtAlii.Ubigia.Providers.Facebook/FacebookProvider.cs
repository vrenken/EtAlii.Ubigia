namespace EtAlii.Ubigia.Provisioning.Facebook
{
    public class FacebookProvider : IProvider
    {
        public IProviderConfiguration Configuration => _configuration;
        private readonly IProviderConfiguration _configuration;

        public FacebookProvider(IProviderConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Stop()
        {
        }

        public void Start()
        {
        }
    }
}
