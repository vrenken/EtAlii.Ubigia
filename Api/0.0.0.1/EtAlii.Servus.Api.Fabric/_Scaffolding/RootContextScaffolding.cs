namespace EtAlii.Servus.Api.Fabric
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