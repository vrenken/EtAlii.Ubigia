namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Admin.Grpc;
    using EtAlii.Ubigia.Infrastructure.Transport.User.Grpc;
    using EtAlii.Ubigia.Storage;
    using EtAlii.xTechnology.Hosting.Grpc;

    public interface IInfrastructureTestHost : IGrpcHost
    {
        IInfrastructure Infrastructure { get; }

        IStorage Storage { get; }

        AdminModule AdminModule { get; }
        UserModule UserModule { get; }
    }
}