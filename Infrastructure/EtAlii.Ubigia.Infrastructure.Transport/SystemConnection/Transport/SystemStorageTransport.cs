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

        public Uri Address { get; }
        
        public SystemStorageTransport(Uri address, IInfrastructure infrastructure)
        {
            Address = address;
            _infrastructure = infrastructure;
        }

        public async Task Start()
        {
            await Task.Run(() => IsConnected = true);
        }

        public async Task Stop()
        {
            await Task.Run(() => IsConnected = false);
        }

        xTechnology.MicroContainer.IScaffolding[] IStorageTransport.CreateScaffolding()
        {
            return new xTechnology.MicroContainer.IScaffolding[]
            {
                new SystemClientsScaffolding(_infrastructure)
            };
        }
    }
}
