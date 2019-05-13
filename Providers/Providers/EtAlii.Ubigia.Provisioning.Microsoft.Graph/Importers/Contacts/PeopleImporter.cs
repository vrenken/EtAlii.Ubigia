namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Threading.Tasks;

    public class PeopleImporter : IPeopleImporter
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