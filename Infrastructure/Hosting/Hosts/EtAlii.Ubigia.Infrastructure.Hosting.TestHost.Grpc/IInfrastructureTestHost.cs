namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc
{
    using EtAlii.Ubigia.Persistence;

    // using EtAlii.Ubigia.Infrastructure.Transport.Admin.Grpc;
    // using EtAlii.Ubigia.Infrastructure.Transport.User.Grpc;

    public interface IInfrastructureTestHost : IInfrastructureTestHostBase
    {
        IStorage Storage { get; }
    }
}