namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    internal partial class SystemAuthenticationManagementDataClient : IAuthenticationManagementDataClient
    {
        private readonly IInfrastructure _infrastructure;

        public SystemAuthenticationManagementDataClient(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public async Task Connect(IStorageConnection storageConnection)
        {
            await Task.CompletedTask;
        }

        public async Task Disconnect(IStorageConnection storageConnection)
        {
            await Task.CompletedTask;
        }
    }
}
