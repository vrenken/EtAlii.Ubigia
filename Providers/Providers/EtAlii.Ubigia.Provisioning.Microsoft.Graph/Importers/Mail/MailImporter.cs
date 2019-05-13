namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Threading.Tasks;

    public class MailImporter  : IMailImporter
    {
        public Task Start()
        {
            // Handle Start.
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            // Handle Stop.
            return Task.CompletedTask;
        }
    }
}