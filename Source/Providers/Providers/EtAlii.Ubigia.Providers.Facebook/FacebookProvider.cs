namespace EtAlii.Ubigia.Provisioning.Facebook
{
    using System.Threading.Tasks;

    public class FacebookProvider : IProvider
    {
        /// <summary>
        /// The Configuration used to instantiate this Provider.
        /// </summary>
        public IProviderConfiguration Configuration { get; }

        public FacebookProvider(IProviderConfiguration configuration)
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
