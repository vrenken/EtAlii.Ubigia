namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class FunctionalTestContextFactory
    {
        public IFunctionalTestContext Create()
        {
            var container = new Container();

            container.Register<IFunctionalTestContext, FunctionalTestContext>();
            container.Register(() => new LogicalTestContextFactory().Create());
            container.Register(() => DiagnosticsConfiguration.Default);
            return container.GetInstance<IFunctionalTestContext>();
        }
    }
}
