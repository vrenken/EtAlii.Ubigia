namespace EtAlii.Servus.Api.Logical.Tests
{
    using EtAlii.Servus.Api.Fabric.Tests;
    using EtAlii.Servus.Infrastructure.Hosting;
    using EtAlii.xTechnology.MicroContainer;

    public class LogicalTestContextFactory
    {
        public ILogicalTestContext Create()
        {
            var container = new Container();

            container.Register<ILogicalTestContext,LogicalTestContext2>();
            container.Register<IFabricTestContext>(() => new FabricTestContextFactory().Create());

            return container.GetInstance<ILogicalTestContext>();
        }
    }
}