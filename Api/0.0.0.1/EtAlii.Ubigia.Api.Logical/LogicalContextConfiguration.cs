namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public class LogicalContextConfiguration : Configuration<ILogicalContextExtension, LogicalContextConfiguration>, ILogicalContextConfiguration, IEditableLogicalContextConfiguration
    {
        IFabricContext IEditableLogicalContextConfiguration.Fabric { get => Fabric; set => Fabric = value; }
        public IFabricContext Fabric { get; private set; }

        bool IEditableLogicalContextConfiguration.CachingEnabled { get => CachingEnabled; set => CachingEnabled = value; }
        public bool CachingEnabled { get; private set; }

        public LogicalContextConfiguration()
        {
            CachingEnabled = true;
        }
    }
}