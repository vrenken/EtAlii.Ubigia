namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.xTechnology.MicroContainer;

    internal class GraphScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IGraphPathBuilder, GraphPathBuilder>();
            container.Register<IGraphPathTraverserFactory, GraphPathTraverserFactory>();
            container.Register<IGraphComposerFactory, GraphComposerFactory>();
            container.Register<IGraphPathAssignerFactory, GraphPathAssignerFactory>();
        }
    }
}