namespace EtAlii.Ubigia.Provisioning.Facebook
{
    public class FacebookProvider : IProvider
    {
        public IProviderConfiguration Configuration { get; }

        public FacebookProvider(IProviderConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Stop()
        {
            // Handle Stop.
        }

        public void Start()
        {
            // Handle Start.
        }
    }
}
