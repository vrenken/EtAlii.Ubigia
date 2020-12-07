namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using System.Text;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public class ProvisioningService : ServiceBase, IProvisioningService
    {
        private readonly IProvisioningManager _provisioningManager;

        public ProvisioningService(IConfigurationSection configurationSection, IProvisioningManager provisioningManager)
            : base(configurationSection)
        {
            _provisioningManager = provisioningManager;
        }

        public override async Task Start()
        {
            Status.Title = "Ubigia provisioning";

            Status.Description = "Starting...";
            Status.Summary = Status.Description;

            await _provisioningManager.Start().ConfigureAwait(false);

            
            var sb = new StringBuilder();
            sb.AppendLine("All OK. Ubigia is being fed by the providers specified below.");
            sb.AppendLine($"Address: {_provisioningManager.Configuration.Address}");

            foreach (var providerConfiguration in _provisioningManager.Configuration.ProviderConfigurations)
            {
                sb.AppendLine($"- {providerConfiguration}");
            }

            sb.AppendLine();
            sb.AppendLine("Output:");
            sb.AppendLine(_provisioningManager.Status);

            Status.Description = sb.ToString();
            Status.Summary = Status.Description;
        }

        public override async Task Stop()
        {
            Status.Description = "Stopping...";
            Status.Summary = Status.Description;

            await _provisioningManager.Stop().ConfigureAwait(false);

            var sb = new StringBuilder();
            sb.AppendLine("Stopped.");
            sb.AppendLine();
            sb.AppendLine("Output:");
            sb.AppendLine(_provisioningManager.Status);

            Status.Description = sb.ToString();
            Status.Summary = Status.Description;
        }
    }
}