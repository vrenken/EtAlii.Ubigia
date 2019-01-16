namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    internal class PropertiesScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IPropertiesSet, PropertiesSet>();

            container.Register<IPropertiesGetter, PropertiesGetter>();
            container.Register<IPropertiesStorer, PropertiesStorer>();
        }
    }
}