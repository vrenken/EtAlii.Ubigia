namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Threading.Tasks;

    public class CalendarImporter : ICalendarImporter
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