namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class SystemSpaceTransport : ISystemSpaceTransport
    {
        public bool IsConnected => _isConnected;
        private bool _isConnected;

        private readonly IInfrastructure _infrastructure;

        public SystemSpaceTransport(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public void Initialize(ISpaceConnection spaceConnection, string address)
        {
        }

        public async Task Start(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => _isConnected = true);
        }

        public async Task Start(ISpaceConnection spaceConnection, string address)
        {
            await Task.Run(() => _isConnected = true);
        }

        public async Task Stop(ISpaceConnection spaceConnection)
        {
            await Task.Run(() => _isConnected = false);
        }

        EtAlii.xTechnology.MicroContainer.IScaffolding[] ISpaceTransport.CreateScaffolding()
        {
            return new EtAlii.xTechnology.MicroContainer.IScaffolding[]
            {
                new SystemClientsScaffolding(_infrastructure)
            };
        }
    }
}
