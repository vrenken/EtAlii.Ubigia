namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;

    public class GraphPathAssignerFactory : IGraphPathAssignerFactory
    {
        public GraphPathAssignerFactory()
        {
        }

        public IGraphPathAssigner Create(IFabricContext fabric)
        {
            var container = new Container();

            container.Register<IFabricContext>(() => fabric);
            container.Register<IGraphPathAssigner, GraphPathAssigner>();

            container.Register<IToIdentifierAssignerSelector, ToIdentifierAssignerSelector>();
            container.Register<IPropertiesToIdentifierAssigner, PropertiesToIdentifierAssigner>();
            container.Register<IDynamicObjectToIdentifierAssigner, DynamicObjectToIdentifierAssigner>();
            container.Register<INodeToIdentifierAssigner, NodeToIdentifierAssigner>();

            container.Register<IUpdateEntryFactory, UpdateEntryFactory>();

            container.Register<IAssignmentContext, AssignmentContext>();

            return container.GetInstance<IGraphPathAssigner>();
        }
    }
}
