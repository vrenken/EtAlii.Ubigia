namespace EtAlii.Ubigia.Provisioning
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Infrastructure.Hosting;

    public interface IProvisioningTestContext
    {
        IHostTestContext<InProcessInfrastructureTestHost> Context { get; }

        Task<IDataConnection> CreateDataConnection(string accountName, string accountPassword, string spaceName);

        Task<IDataContext> CreateDataContext(string accountName, string accountPassword, string spaceName);

        Task<IManagementConnection> OpenManagementConnection();

        Task Start();
        Task Stop();
    }
}