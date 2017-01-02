namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.Ubigia.Infrastructure;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Storage;

    public class WebsiteHost : HostBase
    {
        public WebsiteHost(
            IInfrastructure infrastructure,
            IHostConfiguration configuration,
            IStorage storage)
            : base(configuration, infrastructure, storage)
        {
        }

        public override void Start()
        {
            Infrastructure.Start();
        }

        public override void Stop()
        {
            Infrastructure.Stop();
        }
    }
}
