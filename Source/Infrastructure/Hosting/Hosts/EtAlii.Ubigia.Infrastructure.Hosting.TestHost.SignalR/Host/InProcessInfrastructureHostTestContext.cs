// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.SignalR;

    public class InProcessInfrastructureHostTestContext : SignalRHostTestContext
    {
        public InProcessInfrastructureHostTestContext()
        {
            UseInProcessConnection = true;
        }
    }
}
