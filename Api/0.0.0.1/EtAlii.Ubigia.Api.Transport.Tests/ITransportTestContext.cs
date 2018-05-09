namespace EtAlii.Ubigia.Api.Tests
{
	using System;
	using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
	using EtAlii.Ubigia.Infrastructure.Hosting.Tests;

    public interface ITransportTestContext : ITransportTestContext<InProcessInfrastructureHostTestContext>
    {
    }

    public interface ITransportTestContext<out THostTestContext>
        where THostTestContext : class, IHostTestContext, new()
    {
        THostTestContext Context { get; }

        Task<IDataConnection> CreateDataConnection(bool openOnCreation = true);
        Task<IDataConnection> CreateDataConnection(string accountName, string accountPassword, string spaceName, bool openOnCreation, bool useNewSpace, SpaceTemplate spaceTemplate = null);
        Task<IDataConnection> CreateDataConnection(Uri address, string accountName, string accountPassword, string spaceName, bool openOnCreation, bool useNewSpace, SpaceTemplate spaceTemplate = null);
        Task<IManagementConnection> CreateManagementConnection(Uri address, string accountName, string password, bool openOnCreation);
        Task<IManagementConnection> CreateManagementConnection(bool openOnCreation = true);

        Task<Account> AddUserAccount(IManagementConnection connection);

        Task Start();
        Task Stop();
    }
}