namespace EtAlii.Servus.Infrastructure.Logical
{
    public interface ILogicalContext
    {
        ILogicalStorageSet Storages { get; }
        ILogicalSpaceSet Spaces { get; }
        ILogicalAccountSet Accounts { get; }

        ILogicalRootSet Roots { get; }
        ILogicalEntrySet Entries { get; }

        ILogicalContentSet Content { get; }
        ILogicalContentDefinitionSet ContentDefinition { get; }

        ILogicalPropertiesSet Properties { get; }
        ILogicalIdentifierSet Identifiers { get; }

        void Start();
        void Stop();

        void Initialize(
            ILogicalStorageSet storages,
            ILogicalSpaceSet spaces,
            ILogicalAccountSet accounts,
            ILogicalRootSet roots,
            ILogicalEntrySet entries,
            ILogicalContentSet content,
            ILogicalContentDefinitionSet contentDefinition,
            ILogicalPropertiesSet properties,
            ILogicalIdentifierSet identifiers);
    }
}