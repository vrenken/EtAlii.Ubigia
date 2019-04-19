namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class DataScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IRootRepository, RootRepository>();
            container.Register<IEntryRepository, EntryRepository>();
            container.Register<IContentRepository, ContentRepository>();
            container.Register<IContentDefinitionRepository, ContentDefinitionRepository>();
            container.Register<IPropertiesRepository, PropertiesRepository>();
            container.Register<IIdentifierRepository, IdentifierRepository>();

            container.Register<IAccountInitializer, AccountInitializer>();
            container.Register<ISpaceInitializer, DirectSpaceInitializer>();
        }
    }
}