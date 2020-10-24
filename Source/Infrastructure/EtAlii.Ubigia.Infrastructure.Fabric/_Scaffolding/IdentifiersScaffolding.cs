namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    internal class IdentifiersScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IIdentifierSet, IdentifierSet>();
        }
    }
}