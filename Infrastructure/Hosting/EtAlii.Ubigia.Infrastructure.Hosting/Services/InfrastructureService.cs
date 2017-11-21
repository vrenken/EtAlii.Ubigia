namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;
    using System.Text;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;

    public class InfrastructureService : IInfrastructureService
    {
        private readonly IInfrastructure _infrastructure;
        public HostStatus Status { get; } = new HostStatus(nameof(InfrastructureService));
        public IHostCommand[] Commands { get; } = Array.Empty<IHostCommand>();

        public InfrastructureService(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public void Start()
        {
            Status.Title = "Ubigia infrastructure";

            Status.Description = "Starting...";
            Status.Summary = Status.Description;

            _infrastructure.Start();

            var sb = new StringBuilder();
            sb.AppendLine("All OK. Ubigia is serving the storage specified below.");
            sb.AppendLine($"Name: {_infrastructure.Configuration.Name}");
            sb.AppendLine($"Address: {_infrastructure.Configuration.Address}");
            Status.Summary = sb.ToString();
            Status.Summary = Status.Description;
        }

        public void Stop()
        {
            Status.Description = "Stopping...";
            Status.Summary = Status.Description;

            _infrastructure.Stop();

            Status.Description = "Stopped.";
            Status.Summary = Status.Description;
        }
    }
}
