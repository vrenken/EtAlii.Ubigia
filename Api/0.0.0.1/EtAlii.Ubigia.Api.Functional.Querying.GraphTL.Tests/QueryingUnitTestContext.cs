﻿namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting.Tests;
    using Xunit;

    public class QueryingUnitTestContext : IAsyncLifetime
    {
        public IFunctionalTestContext FunctionalTestContext { get; private set; }

        public async Task InitializeAsync()
        {
            FunctionalTestContext = new FunctionalTestContextFactory().Create();
            await FunctionalTestContext.Start();
        }

        public async Task DisposeAsync()
        {
            await FunctionalTestContext.Stop();
            FunctionalTestContext = null;
        }
    }
}