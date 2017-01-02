namespace EtAlii.Servus.Infrastructure.Functional
{
    using HashLib;
    using SimpleInjector;

    internal class DataScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IRootRepository, RootRepository>(Lifestyle.Singleton);
            container.Register<IEntryRepository, EntryRepository>(Lifestyle.Singleton);
            container.Register<IContentRepository, ContentRepository>(Lifestyle.Singleton);
            container.Register<IContentDefinitionRepository, ContentDefinitionRepository>(Lifestyle.Singleton);
            container.Register<IPropertiesRepository, PropertiesRepository>(Lifestyle.Singleton);
            container.Register<IIdentifierRepository, IdentifierRepository>(Lifestyle.Singleton);
        }
    }
}