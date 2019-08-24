namespace EtAlii.Ubigia.Provisioning
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public class ProviderContext : IProviderContext
    {
        public IGraphSLScriptContext SystemScriptContext { get; }

        public IManagementConnection ManagementConnection { get; }

        private readonly IProviderConfiguration _configuration;

        public ProviderContext(
            IGraphSLScriptContext systemScriptContext,
            IManagementConnection managementConnection,
            IProviderConfiguration configuration)
        {
            SystemScriptContext = systemScriptContext;
            
            ManagementConnection = managementConnection;
            _configuration = configuration;
        }

        public async Task<IGraphSLScriptContext> CreateScriptContext(string accountName, string spaceName)
        {
            IDataConnection dataConnection = await ManagementConnection.OpenSpace(accountName, spaceName);
            return _configuration.CreateScriptContext(dataConnection);
        }

        public async Task<IGraphSLScriptContext> CreateScriptContext(Space space)
        {
            IDataConnection dataConnection = await ManagementConnection.OpenSpace(space);
            return _configuration.CreateScriptContext(dataConnection);
        }
    }
}