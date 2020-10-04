namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using EtAlii.Ubigia.Api.Fabric.Tests;
    using EtAlii.xTechnology.MicroContainer;

    public class LogicalTestContextFactory
    {
        public ILogicalTestContext Create()
        {
            var container = new Container();

            container.Register<ILogicalTestContext, LogicalTestContext>();
            container.Register(() => new FabricTestContextFactory().Create());

            return container.GetInstance<ILogicalTestContext>();
        }
    }
}