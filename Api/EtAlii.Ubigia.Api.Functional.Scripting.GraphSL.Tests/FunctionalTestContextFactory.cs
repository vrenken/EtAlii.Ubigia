namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class FunctionalTestContextFactory
    {
        public IFunctionalTestContext Create()
        {
            var container = new Container();

            container.Register<IFunctionalTestContext, FunctionalTestContext>();
            container.Register(() => new LogicalTestContextFactory().Create());
            container.Register(() => UbigiaDiagnostics.DefaultConfiguration);
            return container.GetInstance<IFunctionalTestContext>();
        }
    }
}