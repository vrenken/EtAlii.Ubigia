namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Storage;

    public class DefaultHost : HostBase
    {
        protected DefaultHost(IHostConfiguration configuration, IInfrastructure infrastructure, IStorage storage) 
            : base(configuration, infrastructure, storage)
        {
        }

        public override void Start()
        {
        }

        public override void Stop()
        {
        }
    }
}