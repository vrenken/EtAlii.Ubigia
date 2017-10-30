namespace EtAlii.xTechnology.Hosting
{

    public abstract class HostBase : IHost
    {
        private readonly IServiceManager _serviceManager;

        public IHostConfiguration Configuration { get; }

        protected HostBase(
            IHostConfiguration configuration, 
            IServiceManager serviceManager)
        {
            Configuration = configuration;
            _serviceManager = serviceManager;
        }

        public virtual void Start()
        {
            _serviceManager.Start();
        }

        public virtual void Stop()
        {
            _serviceManager.Stop();
        }
    }
}
