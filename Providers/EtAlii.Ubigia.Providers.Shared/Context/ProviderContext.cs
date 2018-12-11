namespace EtAlii.Ubigia.Provisioning
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;
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

        public IGraphSLScriptContext CreateScriptContext(string accountName, string spaceName)
        {
            IDataConnection dataConnection = null;
            var task = Task.Run(async () =>
            {
                dataConnection = await ManagementConnection.OpenSpace(accountName, spaceName);
            });
            task.Wait();
            return _configuration.CreateScriptContext(dataConnection);
        }

        public IGraphSLScriptContext CreateScriptContext(Space space)
        {
            IDataConnection dataConnection = null;
            var task = Task.Run(async () =>
            {
                dataConnection = await ManagementConnection.OpenSpace(space);
            });
            task.Wait();
            return _configuration.CreateScriptContext(dataConnection);
        }
    }
}