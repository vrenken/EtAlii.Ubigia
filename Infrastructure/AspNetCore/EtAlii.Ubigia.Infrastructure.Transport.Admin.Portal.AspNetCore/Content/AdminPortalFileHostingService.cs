namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.AspNetCore
{
    using EtAlii.xTechnology.Hosting;

    public class AdminPortalFileHostingService : ServiceBase
    {
        public override void Start()
        {
            // Handle Start.
        }

        public override void Stop()
        {
            // Handle Stop.
        }

        protected override void Initialize(IHost host, ISystem system, IModule[] moduleChain, out Status status)
        {
            status = new Status(nameof(AdminPortalFileHostingService));
        }
    }
}
