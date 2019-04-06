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

        public Task Connect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }

        public Task Disconnect(IStorageConnection storageConnection)
        {
            return Task.CompletedTask;
        }
    }
}
