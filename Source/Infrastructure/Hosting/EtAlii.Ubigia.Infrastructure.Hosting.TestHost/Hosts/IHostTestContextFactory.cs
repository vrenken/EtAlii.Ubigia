namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    public interface IHostTestContextFactory
    {
        THostTestContext Create<THostTestContext>()
            where THostTestContext : class, IHostTestContext, new();
    } 
}