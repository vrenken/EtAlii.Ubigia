namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using System.Text;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public class ProvisioningService : ServiceBase, IProvisioningService
    {
        private readonly IProvisioning _provisioning;

        public ProvisioningService(IConfigurationSection configurationSection, IProvisioning provisioning)
            : base(configurationSection)
        {
            _provisioning = provisioning;
        }

        public override async Task Start()
        {
            Status.Title = "Ubigia provisioning";

            Status.Description = "Starting...";
            Status.Summary = Status.Description;

            await _provisioning.Start().ConfigureAwait(false);

            
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

        public override async Task Stop()
        {
            Status.Description = "Stopping...";
            Status.Summary = Status.Description;

            await _provisioning.Stop().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("Stopped.");
            sb.AppendLine();
            sb.AppendLine("Output:");
            sb.AppendLine(_provisioning.Status);

            Status.Description = sb.ToString();
            Status.Summary = Status.Description;
        }
    }
}