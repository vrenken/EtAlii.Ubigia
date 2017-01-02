namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.Ubigia.Infrastructure;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Storage;

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