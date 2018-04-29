namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.AspNetCore
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Admin.AspNetCore;
    using EtAlii.Ubigia.Infrastructure.Transport.User.AspNetCore;
    using EtAlii.Ubigia.Storage;
    using EtAlii.xTechnology.Hosting;

    public interface IInfrastructureTestHost : IAspNetCoreHost
    {
        IInfrastructure Infrastructure { get; }

        IStorage Storage { get; }

        AdminModule AdminModule { get; }
        UserModule UserModule { get; }
    }
}