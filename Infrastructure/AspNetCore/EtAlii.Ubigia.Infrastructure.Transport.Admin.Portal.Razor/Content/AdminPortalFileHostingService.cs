namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Razor
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public class AdminPortalFileHostingService : ServiceBase
    {
        public AdminPortalFileHostingService(IConfigurationSection configuration) 
            : base(configuration)
        {
        }

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

        // protected override Task Initialize(IHost host, ISystem system, IModule[] moduleChain, out Status status)
        // {
        //     status = new Status(nameof(AdminPortalFileHostingService));
        //     return Task.CompletedTask;
        // }
    }
}
