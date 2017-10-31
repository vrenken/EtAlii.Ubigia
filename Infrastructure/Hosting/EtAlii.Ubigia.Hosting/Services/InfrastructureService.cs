namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class InfrastructureService : IInfrastructureService
    {
        private readonly IInfrastructure _infrastructure;

        public InfrastructureService(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public void Start()
        {
            Console.WriteLine("Starting Ubigia infrastructure...");

            _infrastructure.Start();

            Console.WriteLine("All OK. Ubigia is serving the storage specified below.");
            Console.WriteLine("Name: " + _infrastructure.Configuration.Name);
            Console.WriteLine("Address: " + _infrastructure.Configuration.Address);

        }

        public void Stop()
        {
            _infrastructure.Stop();
        }
    }
}
