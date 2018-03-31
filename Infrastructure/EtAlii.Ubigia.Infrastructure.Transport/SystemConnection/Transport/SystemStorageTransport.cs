namespace EtAlii.Ubigia.Infrastructure.Transport
{
	using System;
	using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class SystemStorageTransport : ISystemStorageTransport
    {
        public bool IsConnected { get; private set; }

        private readonly IInfrastructure _infrastructure;

        public SystemStorageTransport(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public void Initialize(IStorageConnection storageConnection)
        {
        }

        public void Initialize(IStorageConnection storageConnection, Uri address)
        {
        }

        public async Task Start(IStorageConnection storageConnection)
        {
            await Task.Run(() => IsConnected = true);
        }

        public async Task Start(IStorageConnection storageConnection, Uri address)
        {
            await Task.Run(() => IsConnected = true);
        }

        public async Task Stop(IStorageConnection storageConnection)
        {
            await Task.Run(() => IsConnected = false);
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
