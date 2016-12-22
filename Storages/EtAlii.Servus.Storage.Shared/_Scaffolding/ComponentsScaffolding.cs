namespace EtAlii.Servus.Storage
{
    using EtAlii.xTechnology.MicroContainer;

    public class ComponentsScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IComponentStorage, ComponentStorage>();
            container.Register<IComponentStorer, ComponentStorer>();
            container.Register<ICompositeComponentStorer, CompositeComponentStorer>();
            container.Register<IComponentRetriever, ComponentRetriever>();

            container.Register<INextCompositeComponentIdAlgorithm, NextCompositeComponentIdAlgorithm>();
        }
    }
}
