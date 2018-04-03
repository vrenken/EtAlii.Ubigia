namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.xTechnology.Hosting;

    public class HostTestContextFactory : IHostTestContextFactory
    {
        public IHostTestContext Create()
        {
            return new HostTestContext();
        }
    }
}