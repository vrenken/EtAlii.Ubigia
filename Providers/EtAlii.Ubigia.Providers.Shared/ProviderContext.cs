namespace EtAlii.Ubigia.Provisioning
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public class ProviderContext : IProviderContext
    {
        public IDataContext SystemDataContext { get; }

        public IManagementConnection ManagementConnection { get; }

        private readonly IProviderConfiguration _configuration;

        public ProviderContext(
            IDataContext systemDataContext, 
            IManagementConnection managementConnection,
            IProviderConfiguration configuration)
        {
            SystemDataContext = systemDataContext;
            ManagementConnection = managementConnection;
            _configuration = configuration;
        }

        public IDataContext CreateDataContext(Space space)
        {
            IDataConnection dataConnection = null;
            var task = Task.Run(async () =>
            {
                dataConnection = await ManagementConnection.OpenSpace(space);
            });
            task.Wait();
            return _configuration.CreateDataContext(dataConnection);
        }

        public IDataContext CreateDataContext(string accountName, string spaceName)
        {
            IDataConnection dataConnection = null;
            var task = Task.Run(async () =>
            {
                dataConnection = await ManagementConnection.OpenSpace(accountName, spaceName);
            });
            task.Wait();
            return _configuration.CreateDataContext(dataConnection);
        }
    }
}