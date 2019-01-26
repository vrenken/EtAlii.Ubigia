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
            // Start the Facebook provisioning subsystems.
        }

        public void Start()
        {
            // Stop the Facebook provisioning subsystems.
        }
    }
}
