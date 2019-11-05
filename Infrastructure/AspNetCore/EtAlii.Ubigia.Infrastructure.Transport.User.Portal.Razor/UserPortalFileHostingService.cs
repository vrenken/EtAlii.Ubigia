namespace EtAlii.Ubigia.Infrastructure.Transport.User.Portal.Razor
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;

    public class UserPortalFileHostingService : ServiceBase
    {
        public override Task Start()
        {
            // Handle Start.
            return Task.CompletedTask;
        }

        public override Task Stop()
        {
            // Handle Stop.
            return Task.CompletedTask;
        }

        protected override Task Initialize(IHost host, ISystem system, IModule[] moduleChain, out Status status)
        {
            status = new Status(nameof(UserPortalFileHostingService));
            return Task.CompletedTask;
        }
    }
}
