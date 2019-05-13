namespace EtAlii.Ubigia.Provisioning.Mail
{
    using System.Threading.Tasks;

    public class MailProvider : IProvider
    {
        public IProviderConfiguration Configuration { get; }

        public MailProvider(IProviderConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Task Stop()
        {
            // Handle Stop.
            return Task.CompletedTask;
        }

        public Task Start()
        {
            // Handle Start.
            return Task.CompletedTask;
        }
    }
}
