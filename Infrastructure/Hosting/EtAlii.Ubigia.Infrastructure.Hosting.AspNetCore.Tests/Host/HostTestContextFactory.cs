namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.xTechnology.Hosting;

    public class HostTestContextFactory : IHostTestContextFactory
    {
        public THostTestContext Create<THostTestContext>()
            where THostTestContext : class, IHostTestContext, new()
        {
            return new THostTestContext();
        }

        public IHostTestContext Create()
        {
            return new HostTestContext();
        }
    }
}