namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    public interface IHostTestContextFactory
    {
        THostTestContext Create<THostTestContext>()
            where THostTestContext : class, IHostTestContext, new();

        IHostTestContext Create();
    }
}