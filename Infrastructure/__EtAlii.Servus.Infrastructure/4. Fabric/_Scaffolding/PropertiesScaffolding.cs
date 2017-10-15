namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    internal class PropertiesScaffolding : EtAlii.xTechnology.MicroContainer.IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IPropertiesSet, PropertiesSet>();

            container.Register<IPropertiesGetter, PropertiesGetter>();
            container.Register<IPropertiesStorer, PropertiesStorer>();
        }
    }
}