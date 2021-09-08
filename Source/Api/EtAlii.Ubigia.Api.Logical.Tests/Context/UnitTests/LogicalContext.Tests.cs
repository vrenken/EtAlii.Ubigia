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
            var fabricOptions = await new FabricOptions(_testContext.ClientConfiguration)
                .UseDiagnostics()
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);
            using var fabricContext = Factory.Create<IFabricContext>(fabricOptions);

            var options = new LogicalOptions(_testContext.ClientConfiguration)
                .UseFabricContext(fabricContext)
                .UseDiagnostics();

            // Act.
            using var context = Factory.Create<ILogicalContext>(options);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task LogicalContext_Dispose()
        {
            // Arrange.
            var fabricOptions = await new FabricOptions(_testContext.ClientConfiguration)
                .UseDiagnostics()
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);
            using var fabricContext = Factory.Create<IFabricContext>(fabricOptions);

            var logicalOptions = new LogicalOptions(_testContext.ClientConfiguration)
                .UseFabricContext(fabricContext)
                .UseDiagnostics();

            // Act.
            using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);

            // Assert.
            Assert.NotNull(logicalContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task LogicalContext_Create_Check_Components()
        {
            // Arrange.
            var fabricOptions = await new FabricOptions(_testContext.ClientConfiguration)
                .UseDiagnostics()
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);
            using var fabricContext = Factory.Create<IFabricContext>(fabricOptions);

            var logicalOptions = new LogicalOptions(_testContext.ClientConfiguration)
                .UseFabricContext(fabricContext)
                .UseDiagnostics();

            // Act.
            using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);

            // Assert.
            Assert.NotNull(logicalContext);
        }
    }
}
