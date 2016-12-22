namespace EtAlii.Servus.Infrastructure.Functional
{
    public interface IInfrastructure
    {
        IInfrastructureConfiguration Configuration { get; }

        IStorageRepository Storages { get; }
        ISpaceRepository Spaces { get; }
        IIdentifierRepository Identifiers { get; }
        IEntryRepository Entries { get; }
        IPropertiesRepository Properties { get; }
        IRootRepository Roots { get; }
        IAccountRepository Accounts { get; }
        IContentRepository Content { get; }
        IContentDefinitionRepository ContentDefinition { get ; }

        void Start();
        void Stop();
    }
}