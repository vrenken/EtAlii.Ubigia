// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Tests;
    using Xunit;
    using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

    public class LogicalUnitTestContext : IAsyncLifetime
    {
        public IFabricTestContext Fabric { get; private set; }

        public IConfiguration ClientConfiguration => Fabric.Transport.Host.ClientConfiguration;
        public IConfiguration HostConfiguration => Fabric.Transport.Host.HostConfiguration;

        public async Task InitializeAsync()
        {
            Fabric = new FabricTestContextFactory().Create();
            await Fabric.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await Fabric.Stop().ConfigureAwait(false);
            Fabric = null;
        }
    }
}
