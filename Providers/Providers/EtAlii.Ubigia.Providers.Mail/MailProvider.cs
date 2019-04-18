namespace EtAlii.Ubigia.Provisioning.Mail
{
    public class MailProvider : IProvider
    {
        public IProviderConfiguration Configuration { get; }

        public MailProvider(IProviderConfiguration configuration)
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
