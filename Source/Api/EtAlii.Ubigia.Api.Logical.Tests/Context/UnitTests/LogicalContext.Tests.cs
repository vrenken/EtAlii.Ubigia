// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests.UnitTests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using Xunit;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class LogicalContextTests : IClassFixture<LogicalUnitTestContext>
    {
        private readonly LogicalUnitTestContext _testContext;

        public LogicalContextTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task LogicalContext_Create()
        {
            // Arrange.
            var fabricOptions = new FabricContextOptions(_testContext.ClientConfiguration)
                .UseDiagnostics();
            var fabricContext = new FabricContextFactory().Create(fabricOptions);

            var options = await new LogicalOptions(_testContext.ClientConfiguration)
                .UseFabricContext(fabricContext)
                .UseDiagnostics()
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);

            // Act.
            var context = new LogicalContextFactory().Create(options);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task LogicalContext_Dispose()
        {
            // Arrange.
            var fabricOptions = new FabricContextOptions(_testContext.ClientConfiguration)
                .UseDiagnostics();
            var fabricContext = new FabricContextFactory().Create(fabricOptions);

            var options = await new LogicalOptions(_testContext.ClientConfiguration)
                .UseFabricContext(fabricContext)
                .UseDiagnostics()
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);

            // Act.
            using var context = new LogicalContextFactory().Create(options);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task LogicalContext_Create_Check_Components()
        {
            // Arrange.
            var fabricOptions = new FabricContextOptions(_testContext.ClientConfiguration)
                .UseDiagnostics();
            var fabricContext = new FabricContextFactory().Create(fabricOptions);

            var options = await new LogicalOptions(_testContext.ClientConfiguration)
                .UseFabricContext(fabricContext)
                .UseDiagnostics()
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);

            // Act.
            var context = new LogicalContextFactory().Create(options);

            // Assert.
            Assert.NotNull(context);
        }
    }
}
