namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.MicroContainer;

    public class FunctionalTestContextFactory
    {
        public IFunctionalTestContext Create()
        {
            var container = new Container();

            container.Register<IFunctionalTestContext, FunctionalTestContext>();
            container.Register(() => new LogicalTestContextFactory().Create());
            container.Register(TestDiagnostics.Create);
            return container.GetInstance<IFunctionalTestContext>();
        }
    }
}