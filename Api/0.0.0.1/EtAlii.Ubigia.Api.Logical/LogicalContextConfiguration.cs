namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public class LogicalContextConfiguration : FabricContextConfiguration, ILogicalContextConfiguration, IEditableLogicalContextConfiguration
    { 
        bool IEditableLogicalContextConfiguration.CachingEnabled { get => CachingEnabled; set => CachingEnabled = value; }
        public bool CachingEnabled { get; private set; }

        public LogicalContextConfiguration()
        {
            CachingEnabled = true;
        }
    }
}