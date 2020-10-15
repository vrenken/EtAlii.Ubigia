namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class LogicalUnitTestContext : IAsyncLifetime
    {
        public ILogicalTestContext LogicalTestContext { get; private set; }
        public IDiagnosticsConfiguration Diagnostics { get; private set; }

        public FileComparer FileComparer { get; }
        public FolderComparer FolderComparer { get; }

        public LogicalUnitTestContext()
        {
            FileComparer = new FileComparer();
            FolderComparer = new FolderComparer(FileComparer);
        }

        public async Task InitializeAsync()
        {
            Diagnostics = DiagnosticsConfiguration.Default;
            LogicalTestContext = new LogicalTestContextFactory().Create();
            await LogicalTestContext.Start(UnitTestSettings.NetworkPortRange);
        }

        public async Task DisposeAsync()
        {
            await LogicalTestContext.Stop();
            LogicalTestContext = null;
            Diagnostics = null;
        }
    }
}