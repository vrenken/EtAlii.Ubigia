namespace EtAlii.Servus.Api.Transport.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Infrastructure.Hosting.Tests;

    public interface ITransportTestContext
    {
        IHostTestContext Context { get; }

        Task<IDataConnection> CreateDataConnection(bool openOnCreation = true);
        Task<IDataConnection> CreateDataConnection(string accountName, string accountPassword, string spaceName, bool openOnCreation, bool useNewSpace, SpaceTemplate spaceTemplate = null);
        Task<IDataConnection> CreateDataConnection(string address, string accountName, string accountPassword, string spaceName, bool openOnCreation, bool useNewSpace, SpaceTemplate spaceTemplate = null);
        Task<IManagementConnection> CreateManagementConnection(string address, string accountName, string password, bool openOnCreation);
        Task<IManagementConnection> CreateManagementConnection(bool openOnCreation = true);

        Task<Account> AddUserAccount(IManagementConnection connection);

        Task Start();
        Task Stop();
    }
}