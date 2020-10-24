namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Threading.Tasks;

    public class OneDriveImporter : IOneDriveImporter
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