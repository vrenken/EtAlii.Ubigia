namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class QueryingUnitTestContext : IAsyncLifetime
    {
        public ILogicalTestContext LogicalTestContext { get; private set; }
        public IDiagnosticsConfiguration Diagnostics { get; private set; }

        public FileComparer FileComparer { get; }
        public FolderComparer FolderComparer { get; }

        public QueryingUnitTestContext()
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