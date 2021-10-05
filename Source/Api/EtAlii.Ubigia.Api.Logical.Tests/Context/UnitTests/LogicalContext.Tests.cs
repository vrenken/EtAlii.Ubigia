// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests.UnitTests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using Xunit;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.MicroContainer;

    [CorrelateUnitTests]
    public class LogicalContextTests : IClassFixture<LogicalUnitTestContext>
    {
        private readonly LogicalUnitTestContext _testContext;

        public LogicalContextTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task LogicalContext_Create()
        {
            // Arrange.
            var logicalOptions = await new FabricOptions(_testContext.ClientConfiguration)
                .UseDiagnostics()
                .UseDataConnectionToNewSpace(_testContext, true)
                .UseLogicalContext()
                .UseDiagnostics()
                .ConfigureAwait(false);

            // Act.
            using var context = Factory.Create<ILogicalContext>(logicalOptions);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task LogicalContext_Dispose()
        {
            // Arrange.
            var logicalOptions = await new FabricOptions(_testContext.ClientConfiguration)
                .UseDiagnostics()
                .UseDataConnectionToNewSpace(_testContext, true)
                .UseLogicalContext()
                .UseDiagnostics()
                .ConfigureAwait(false);

            // Act.
            using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);

            // Assert.
            Assert.NotNull(logicalContext);
        }

        [Fact]
        public async Task LogicalContext_Create_Check_Components()
        {
            // Arrange.
            var logicalOptions = await new FabricOptions(_testContext.ClientConfiguration)
                .UseDiagnostics()
                .UseDataConnectionToNewSpace(_testContext, true)
                .UseLogicalContext()
                .UseDiagnostics()
                .ConfigureAwait(false);

            // Act.
            using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);

            // Assert.
            Assert.NotNull(logicalContext);
        }
    }
}
