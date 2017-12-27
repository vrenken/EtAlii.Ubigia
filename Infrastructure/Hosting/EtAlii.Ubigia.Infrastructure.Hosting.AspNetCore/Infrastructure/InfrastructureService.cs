namespace EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore
{
    using EtAlii.xTechnology.Hosting;

    public class InfrastructureService : ServiceBase
    {
        public override void Start()
        {
        }

        public override void Stop()
        {
        }

        protected override void Initialize(IHost host, ISystem system, IModule[] moduleChain, out Status status)
        {
            status = new Status(nameof(InfrastructureService));
        }
    }
}
