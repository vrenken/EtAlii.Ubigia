namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    internal partial class SystemAuthenticationDataClient : IAuthenticationDataClient
    {
        private readonly IInfrastructure _infrastructure;

        public SystemAuthenticationDataClient(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public async Task Connect(ISpaceConnection spaceConnection)
        {
            await Task.CompletedTask;
        }

        public async Task Disconnect(ISpaceConnection spaceConnection)
        {
            await Task.CompletedTask;
        }
    }
}
