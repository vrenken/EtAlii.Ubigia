namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc
{
    using EtAlii.Ubigia.Persistence;
    
    public interface IInfrastructureTestHost : IInfrastructureTestHostBase
    {
        IStorage Storage { get; }
    }
}