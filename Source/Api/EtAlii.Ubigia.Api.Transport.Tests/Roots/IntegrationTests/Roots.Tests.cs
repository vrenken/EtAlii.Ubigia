// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public class RootsTests : IClassFixture<TransportUnitTestContext>
    {
        private readonly TransportUnitTestContext _testContext;

        public RootsTests(TransportUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task Root_Add_By_Name()
        {
            // Arrange.
            var (connection, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Host.SystemAccountName, _testContext.TransportTestContext.Host.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);
            await connection.Open().ConfigureAwait(false);
            var name = "TestRoot";
            // Act.
            var root = await connection.Roots.Data.Add(name).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(root);
            Assert.NotEqual(Guid.Empty,root.Id);
            Assert.Equal(name,root.Name);
        }

        [Fact(Skip = "Results differ between SignalR/Grpc/REST")]
        public async Task Root_Add_By_Name_Empty()
        {
            // Arrange.
            var (connection, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Host.SystemAccountName, _testContext.TransportTestContext.Host.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);
            await connection.Open().ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Roots.Data.Add(null).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<ArgumentNullException>(act).ConfigureAwait(false);
        }
    }
}
