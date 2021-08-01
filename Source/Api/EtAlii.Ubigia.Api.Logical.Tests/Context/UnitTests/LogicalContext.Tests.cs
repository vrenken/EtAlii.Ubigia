// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests.UnitTests
{
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
        public void LogicalContext_Create()
        {
            // Arrange.
            var configuration = new LogicalContextConfiguration()
                .UseLogicalDiagnostics(_testContext.ClientConfiguration);

            // Act.
            var context = new LogicalContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void LogicalContext_Dispose()
        {
            // Arrange.
            var configuration = new LogicalContextConfiguration()
                .UseLogicalDiagnostics(_testContext.ClientConfiguration);

            // Act.
            using var context = new LogicalContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void LogicalContext_Create_Check_Components()
        {
            // Arrange.
            var configuration = new LogicalContextConfiguration()
                .UseLogicalDiagnostics(_testContext.ClientConfiguration);

            // Act.
            var context = new LogicalContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }
    }
}
