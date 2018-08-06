namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;

    public class QueryingUnitTestContext : IDisposable
    {
        public IFunctionalTestContext FunctionalTestContext { get; private set; }

        public QueryingUnitTestContext()
        {
            var task = Task.Run(async () =>
            {
                FunctionalTestContext = new FunctionalTestContextFactory().Create();
                await FunctionalTestContext.Start();
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(async () =>
            {
                await FunctionalTestContext.Stop();
                FunctionalTestContext = null;
            });
            task.Wait();
        }
    }
}