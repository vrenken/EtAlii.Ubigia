namespace EtAlii.Ubigia.Provisioning.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Management;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;

    public interface IProvisioningTestContext
    {
        IHostTestContext Context { get; }

        Task<IDataConnection> CreateDataConnection(string accountName, string accountPassword, string spaceName);

        Task<IDataContext> CreateDataContext(string accountName, string accountPassword, string spaceName);

        Task<IManagementConnection> OpenManagementConnection();

        Task Start();
        Task Stop();
    }
}