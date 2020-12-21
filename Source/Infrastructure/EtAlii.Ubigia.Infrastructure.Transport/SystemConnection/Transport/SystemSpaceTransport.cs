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

        public Task Start()
        {
            IsConnected = true;
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            IsConnected = false;
            return Task.CompletedTask;
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
