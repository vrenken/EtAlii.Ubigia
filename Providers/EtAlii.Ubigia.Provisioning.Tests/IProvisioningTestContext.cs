namespace EtAlii.Ubigia.Provisioning.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.AspNetCore;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;

    public interface IProvisioningTestContext
    {
        IHostTestContext<InProcessInfrastructureTestHost> Context { get; }

        Task<IGraphSLScriptContext> CreateScriptContext(string accountName, string accountPassword, string spaceName);

        Task<IManagementConnection> OpenManagementConnection();

        Task Start();
        Task Stop();
    }
}