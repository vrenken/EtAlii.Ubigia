namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using System.Text;
    using EtAlii.xTechnology.Hosting;

    public class ProvisioningService : ServiceBase, IProvisioningService
    {
        private readonly IProvisioning _provisioning;

        public ProvisioningService(IProvisioning provisioning)
        {
            _provisioning = provisioning;
        }

        public override void Start()
        {
            Status.Title = "Ubigia provisioning";

            Status.Description = "Starting...";
            Status.Summary = Status.Description;

            _provisioning.Start();

            
            var sb = new StringBuilder();
            sb.AppendLine("All OK. Ubigia is being fed by the providers specified below.");
            sb.AppendLine($"Address: {_provisioning.Configuration.Address}");

            foreach (var providerConfiguration in _provisioning.Configuration.ProviderConfigurations)
            {
                sb.AppendLine($"- {providerConfiguration}");
            }

            sb.AppendLine();
            sb.AppendLine("Output:");
            sb.AppendLine(_provisioning.Status);

            Status.Description = sb.ToString();
            Status.Summary = Status.Description;

        }

        public override void Stop()
        {
            Status.Description = "Stopping...";
            Status.Summary = Status.Description;

            _provisioning.Stop();

            var sb = new StringBuilder();
            sb.AppendLine("Stopped.");
            sb.AppendLine();
            sb.AppendLine("Output:");
            sb.AppendLine(_provisioning.Status);

            Status.Description = sb.ToString();
            Status.Summary = Status.Description;
        }

	    protected override void Initialize(IHost host, ISystem system, IModule[] moduleChain, out Status status)
	    {
			status = new Status(nameof(ProvisioningService));

	    }
    }
}