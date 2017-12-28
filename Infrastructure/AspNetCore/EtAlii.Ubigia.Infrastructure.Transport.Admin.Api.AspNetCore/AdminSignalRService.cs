namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.AspNetCore
{
    public class AdminSignalRService : ServiceBase
    {
        public override void Start()
        {
        }

        public override void Stop()
        {
        }

        protected override void Initialize(IHost host, ISystem system, IModule[] moduleChain, out Status status)
        {
            status = new Status(nameof(AdminSignalRService));
        }
    }
}
