namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    internal class RootContextScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IRootContext, RootContext>();
        }
    }
}