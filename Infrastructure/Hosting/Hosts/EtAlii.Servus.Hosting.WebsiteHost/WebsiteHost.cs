namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Storage;

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
