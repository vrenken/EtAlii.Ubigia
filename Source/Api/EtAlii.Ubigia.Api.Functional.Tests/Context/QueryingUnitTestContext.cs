// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using Xunit;
    using UnitTestSettings = EtAlii.Ubigia.Api.Functional.Tests.UnitTestSettings;

    public class QueryingUnitTestContext : IAsyncLifetime
    {
        public IFunctionalTestContext FunctionalTestContext { get; private set; }

        public async Task InitializeAsync()
        {
            FunctionalTestContext = new FunctionalTestContextFactory().Create();
            await FunctionalTestContext.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await FunctionalTestContext.Stop().ConfigureAwait(false);
            FunctionalTestContext = null;
        }
    }
}
