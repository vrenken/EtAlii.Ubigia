// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;
    using UnitTestSettings = EtAlii.Ubigia.Api.Functional.Tests.UnitTestSettings;

    public class TraversalUnitTestContext : IAsyncLifetime
    {
        public ILogicalTestContext LogicalTestContext { get; private set; }
        public FileComparer FileComparer { get; }
        public FolderComparer FolderComparer { get; }

        public TraversalUnitTestContext()
        {
            FileComparer = new FileComparer();
            FolderComparer = new FolderComparer(FileComparer);
        }

        public async Task InitializeAsync()
        {
            LogicalTestContext = new LogicalTestContextFactory().Create();
            await LogicalTestContext.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await LogicalTestContext.Stop().ConfigureAwait(false);
            LogicalTestContext = null;
        }
    }
}
