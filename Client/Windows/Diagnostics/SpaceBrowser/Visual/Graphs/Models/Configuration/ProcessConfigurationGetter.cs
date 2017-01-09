namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    public class ProcessConfigurationGetter : IProcessConfigurationGetter
    {
        private readonly NewEntryConfiguration _newEntryConfiguration;
        private readonly DiscoveredEntryConfiguration _discoveredEntryConfiguration;
        private readonly InspectedEntryConfiguration _inspectedEntryConfiguration;

        public ProcessConfigurationGetter(NewEntryConfiguration newEntryConfiguration, DiscoveredEntryConfiguration discoveredEntryConfiguration, InspectedEntryConfiguration inspectedEntryConfiguration)
        {
            _newEntryConfiguration = newEntryConfiguration;
            _discoveredEntryConfiguration = discoveredEntryConfiguration;
            _inspectedEntryConfiguration = inspectedEntryConfiguration;
        }

        public EntryConfiguration GetConfiguration(ProcessReason processReason)
        {
            var configuration = (EntryConfiguration)null;
            switch (processReason)
            {
                case ProcessReason.Discovered:
                    configuration = _discoveredEntryConfiguration;
                    break;
                case ProcessReason.Retrieved:
                    configuration = _newEntryConfiguration;
                    break;
                case ProcessReason.Inspected:
                    configuration = _inspectedEntryConfiguration;
                    break;
            }
            return configuration;
        }

    }
}
