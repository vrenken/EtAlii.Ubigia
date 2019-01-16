namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using Ubigia.Tests;

    public class LogicalUnitTestContext : IDisposable
    {
        public ILogicalTestContext LogicalTestContext { get; private set; }
        public IDiagnosticsConfiguration Diagnostics { get; private set; }

        public FileComparer FileComparer { get; }
        public FolderComparer FolderComparer { get; }

        public LogicalUnitTestContext()
        {
            FileComparer = new FileComparer();
            FolderComparer = new FolderComparer(FileComparer);

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