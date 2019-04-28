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

        public Task Connect(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

        public Task Disconnect()
        {
            return Task.CompletedTask;
        }
    }
}
