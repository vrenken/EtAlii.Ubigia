namespace EtAlii.Servus.Api.Logical.Tests
{
    using EtAlii.Servus.Api.Fabric.Tests;
    using EtAlii.xTechnology.MicroContainer;

    public class LogicalTestContextFactory
    {
        public ILogicalTestContext Create()
        {
            var container = new Container();

            container.Register<ILogicalTestContext,LogicalTestContext>();
            container.Register<IFabricTestContext>(() => new FabricTestContextFactory().Create());

            return container.GetInstance<ILogicalTestContext>();
        }
    }
}