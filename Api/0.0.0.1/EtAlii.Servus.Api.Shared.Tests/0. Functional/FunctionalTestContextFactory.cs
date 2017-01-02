namespace EtAlii.Servus.Api.Functional.Tests
{
    using EtAlii.Servus.Api.Logical.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class FunctionalTestContextFactory
    {
        public IFunctionalTestContext Create()
        {
            var container = new Container();

            container.Register<IFunctionalTestContext, FunctionalTestContext>();
            container.Register<ILogicalTestContext>(() => new LogicalTestContextFactory().Create());
            container.Register<IDiagnosticsConfiguration>(() => TestDiagnostics.Create());
            return container.GetInstance<IFunctionalTestContext>();
        }
    }
}