namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.NetCore
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Admin.NetCore;
    using EtAlii.Ubigia.Infrastructure.Transport.User.NetCore;
    using EtAlii.Ubigia.Storage;
    using EtAlii.xTechnology.Hosting;

    public interface IInfrastructureTestHost : IHost
    {
        IInfrastructure Infrastructure { get; }

        IStorage Storage { get; }

        AdminModule AdminModule { get; }
        UserModule UserModule { get; }
    }
}