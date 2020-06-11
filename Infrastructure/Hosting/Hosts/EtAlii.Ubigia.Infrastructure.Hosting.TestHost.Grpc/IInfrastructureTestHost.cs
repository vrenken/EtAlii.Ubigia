namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc
{
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;
    using EtAlii.Ubigia.Infrastructure.Transport.Admin.Grpc;
    using EtAlii.Ubigia.Infrastructure.Transport.User.Grpc;
    using EtAlii.Ubigia.Storage;

    public interface IInfrastructureTestHost : IInfrastructureTestHostBase
    {
        IStorage Storage { get; }

        AdminModule AdminModule { get; }
        UserModule UserModule { get; }
    }
}