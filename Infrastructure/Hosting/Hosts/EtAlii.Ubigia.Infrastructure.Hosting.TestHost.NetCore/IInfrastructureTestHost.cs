namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.NetCore
{
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;
    using EtAlii.Ubigia.Infrastructure.Transport.Admin.NetCore;
    using EtAlii.Ubigia.Infrastructure.Transport.User.NetCore;
    using EtAlii.Ubigia.Storage;

    public interface IInfrastructureTestHost : IInfrastructureTestHostBase
    {
        IStorage Storage { get; }

        AdminModule AdminModule { get; }
        UserModule UserModule { get; }
    }
}