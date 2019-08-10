namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;

    public interface IInfrastructure
    {
        /// <summary>
        /// The Configuration used to instantiate this Infrastructure.
        /// </summary>
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

        Task Start();
        Task Stop();
    }
}