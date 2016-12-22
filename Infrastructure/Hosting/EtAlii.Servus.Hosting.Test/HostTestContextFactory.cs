namespace EtAlii.Servus.Infrastructure.Hosting.Tests
{
    public class HostTestContextFactory : IHostTestContextFactory
    {
        public IHostTestContext Create()
        {
            return new HostTestContext();
        }
    }
}