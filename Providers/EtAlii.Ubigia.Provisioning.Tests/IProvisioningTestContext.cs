namespace EtAlii.Ubigia.Provisioning.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.AspNetCore;

    public interface IProvisioningTestContext
    {
        IHostTestContext<InProcessInfrastructureTestHost> Context { get; }

        Task<IDataConnection> CreateDataConnection(string accountName, string accountPassword, string spaceName);

        Task<IGraphSLScriptContext> CreateScriptContext(string accountName, string accountPassword, string spaceName);

        Task<IManagementConnection> OpenManagementConnection();

        Task Start();
        Task Stop();
    }
}