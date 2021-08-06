// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using Microsoft.Extensions.Configuration;
    using Xunit;
    using UnitTestSettings = EtAlii.Ubigia.Api.Functional.Tests.UnitTestSettings;

    public class QueryingUnitTestContext : IAsyncLifetime
    {
        public IFunctionalTestContext Functional { get; private set; }

        public IConfigurationRoot ClientConfiguration => Functional.Logical.Fabric.Transport.Host.ClientConfiguration;
        public IConfigurationRoot HostConfiguration => Functional.Logical.Fabric.Transport.Host.HostConfiguration;

        public async Task InitializeAsync()
        {
            Functional = new FunctionalTestContextFactory().Create();
            await Functional.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await Functional.Stop().ConfigureAwait(false);
            Functional = null;
        }
    }
}
