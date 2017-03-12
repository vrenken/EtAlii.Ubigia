namespace EtAlii.Ubigia.Provisioning.Mail
{
    public class MailProvider : IProvider
    {
        public IProviderConfiguration Configuration => _configuration;
        private readonly IProviderConfiguration _configuration;

        public MailProvider(IProviderConfiguration configuration)
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
