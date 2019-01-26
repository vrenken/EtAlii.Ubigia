namespace EtAlii.Ubigia.Infrastructure.Functional
{
    public class InfrastructureDataContext : IInfrastructureDataContext
    {
        public IIdentifierRepository Identifiers { get; }

        public IEntryRepository Entries { get; }

        public IRootRepository Roots { get; }

        public IContentRepository Content { get; }

        public IContentDefinitionRepository ContentDefinition { get; }

        public IPropertiesRepository Properties { get; }

        protected InfrastructureDataContext(
            IIdentifierRepository identifiers,
            IEntryRepository entries,
            IRootRepository roots,
            IContentRepository content,
            IContentDefinitionRepository contentDefinition,
            IPropertiesRepository properties)
        {
            Identifiers = identifiers;
            Entries = entries;
            Roots = roots;
            Content = content;
            ContentDefinition = contentDefinition;
            Properties = properties;
        }
    }
}