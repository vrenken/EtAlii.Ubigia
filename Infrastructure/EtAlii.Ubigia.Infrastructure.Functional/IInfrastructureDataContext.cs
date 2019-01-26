namespace EtAlii.Ubigia.Infrastructure.Functional
{
    public interface IInfrastructureDataContext
    {
        IIdentifierRepository Identifiers { get; }
        IEntryRepository Entries { get; }
        IPropertiesRepository Properties { get; }
        IRootRepository Roots { get; }
        IContentRepository Content { get; }
        IContentDefinitionRepository ContentDefinition { get ; }
    }
}