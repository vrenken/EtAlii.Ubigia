namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class SystemStorageTransport : ISystemStorageTransport
    {
        public bool IsConnected => _isConnected;
        private bool _isConnected;

        private readonly IInfrastructure _infrastructure;

        public SystemStorageTransport(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public void Initialize(IStorageConnection storageConnection)
        {
        }

        public void Initialize(IStorageConnection storageConnection, string address)
        {
        }

        public async Task Start(IStorageConnection storageConnection)
        {
            await Task.Run(() => _isConnected = true);
        }

        public async Task Start(IStorageConnection storageConnection, string address)
        {
            await Task.Run(() => _isConnected = true);
        }

        public async Task Stop(IStorageConnection storageConnection)
        {
            await Task.Run(() => _isConnected = false);
        }

        EtAlii.xTechnology.MicroContainer.IScaffolding[] IStorageTransport.CreateScaffolding()
        {
            return new EtAlii.xTechnology.MicroContainer.IScaffolding[]
            {
                new SystemClientsScaffolding(_infrastructure)
            };
        }
    }
}
