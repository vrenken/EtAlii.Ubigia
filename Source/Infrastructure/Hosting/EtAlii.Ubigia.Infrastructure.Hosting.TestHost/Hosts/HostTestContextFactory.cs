namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    public class HostTestContextFactory : IHostTestContextFactory
    {
        public THostTestContext Create<THostTestContext>()
            where THostTestContext : class, IHostTestContext, new()
        {
            return new();
        }
    }
}
