namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    internal class IdentifiersScaffolding : EtAlii.xTechnology.MicroContainer.IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IIdentifierSet, IdentifierSet>();
        }
    }
}