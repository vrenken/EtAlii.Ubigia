namespace EtAlii.Servus.Provisioning.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure.Hosting.Tests;

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