namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    public class HostTestContextFactory : IHostTestContextFactory
    {
        public IHostTestContext Create()
        {
            return new HostTestContext();
        }
    }
}