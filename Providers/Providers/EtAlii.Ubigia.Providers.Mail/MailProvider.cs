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
            // Start the Mail provisioning subsystems.
        }

        public void Start()
        {
            // Stop the Mail provisioning subsystems.
        }

    }
}
