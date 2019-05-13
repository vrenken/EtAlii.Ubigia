namespace EtAlii.Ubigia.Provisioning.Google
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