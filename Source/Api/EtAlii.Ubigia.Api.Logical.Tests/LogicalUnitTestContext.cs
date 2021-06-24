// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class LogicalUnitTestContext : IAsyncLifetime
    {
        public IFabricTestContext FabricTestContext { get; private set; }
        public IDiagnosticsConfiguration DiagnosticsConfiguration { get; private set; }

        public async Task InitializeAsync()
        {
            DiagnosticsConfiguration = EtAlii.xTechnology.Diagnostics.DiagnosticsConfiguration.Default;
            FabricTestContext = new FabricTestContextFactory().Create();
            await FabricTestContext.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await FabricTestContext.Stop().ConfigureAwait(false);
            FabricTestContext = null;
            DiagnosticsConfiguration = null;
        }
    }
}
