namespace EtAlii.Servus.Api.Diagnostics.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;

    public class LogicalUnitTestContext : IDisposable
    {
        public ILogicalTestContext LogicalTestContext { get; private set; }
        public IDiagnosticsConfiguration Diagnostics { get; private set; }

        public LogicalUnitTestContext()
        {
            var task = Task.Run(async () =>
            {
                Diagnostics = TestDiagnostics.Create();
                LogicalTestContext = new LogicalTestContextFactory().Create();
                await LogicalTestContext.Start();
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(async () =>
            {
                await LogicalTestContext.Stop();
                LogicalTestContext = null;
                Diagnostics = null;
            });
            task.Wait();
        }
    }
}