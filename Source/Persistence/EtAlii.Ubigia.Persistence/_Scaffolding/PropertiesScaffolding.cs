namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.MicroContainer;

    public class PropertiesScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IPropertiesStorage, PropertiesStorage>();
            container.Register<IPropertiesStorer, PropertiesStorer>();
            container.Register<IPropertiesRetriever, PropertiesRetriever>();
        }
    }
}
