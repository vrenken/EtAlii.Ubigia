namespace EtAlii.Ubigia.Provisioning.Facebook
{
    using EtAlii.Ubigia.Api.Functional;

    public class FacebookProvider : IProvider
    {
        public IProviderConfiguration Configuration { get { return _configuration; } }
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
