namespace EtAlii.Ubigia.Infrastructure.Hosting.Grpc.Tests
{
    public interface IHostTestContextFactory
    {
        THostTestContext Create<THostTestContext>()
            where THostTestContext : class, IHostTestContext, new();

        IHostTestContext Create();
    }
}