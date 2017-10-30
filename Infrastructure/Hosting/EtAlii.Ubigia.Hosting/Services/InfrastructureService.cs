namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using Functional;
    using Storage;

    public class InfrastructureService : IInfrastructureService
    {
        private readonly IInfrastructure _infrastructure;

        public InfrastructureService(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public void Start()
        {
            _infrastructure.Start();
        }

        public void Stop()
        {
            _infrastructure.Stop();
        }
    }
}
