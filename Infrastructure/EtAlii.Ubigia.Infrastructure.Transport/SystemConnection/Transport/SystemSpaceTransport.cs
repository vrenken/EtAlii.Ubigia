namespace EtAlii.Ubigia.Infrastructure.Transport
{
	using System;
	using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class SystemSpaceTransport : ISystemSpaceTransport
    {
        public bool IsConnected { get; private set; }

        private readonly IInfrastructure _infrastructure;

        public Uri Address { get; }
        
        public SystemSpaceTransport(Uri address, IInfrastructure infrastructure)
        {
            Address = address;
            _infrastructure = infrastructure;
        }

        public SystemSpaceTransport(Uri address)
        {
            Address = address;
        }

        public async Task Start()
        {
            await Task.Run(() => IsConnected = true);
        }

        public async Task Stop()
        {
            await Task.Run(() => IsConnected = false);
        }

        xTechnology.MicroContainer.IScaffolding[] ISpaceTransport.CreateScaffolding()
        {
            return new xTechnology.MicroContainer.IScaffolding[]
            {
                new SystemClientsScaffolding(_infrastructure)
            };
        }
    }
}
