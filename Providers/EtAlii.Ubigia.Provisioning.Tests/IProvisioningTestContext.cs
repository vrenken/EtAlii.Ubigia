namespace EtAlii.Ubigia.Provisioning.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.NetCore;
    using EtAlii.xTechnology.Hosting;

    public interface IProvisioningTestContext
    {
        IHostTestContext<InfrastructureTestHost> Context { get; }

        Task<IGraphSLScriptContext> CreateScriptContext(string accountName, string accountPassword, string spaceName);

        Task<IManagementConnection> OpenManagementConnection();

        Task Start(PortRange portRange);
        Task Stop();
    }
}