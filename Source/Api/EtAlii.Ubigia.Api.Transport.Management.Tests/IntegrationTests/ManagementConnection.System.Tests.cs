﻿namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ManagementConnectionSystemTests : IClassFixture<StartedTransportUnitTestContext>, IDisposable
    {
        private readonly StartedTransportUnitTestContext _testContext;

        public ManagementConnectionSystemTests(StartedTransportUnitTestContext testContext)
        {
            _testContext = testContext;

        }

        public void Dispose()
        {
            // Dispose any relevant resources.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_System_Account()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection().ConfigureAwait(false);

            // Act.
            var systemAccount = await connection.Accounts.Get("System").ConfigureAwait(false);

            // Assert.
            Assert.NotNull(systemAccount);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ManagementConnection_System_Spaces()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection().ConfigureAwait(false);
            var systemAccount = await connection.Accounts.Get("System").ConfigureAwait(false);

            // Act.
            var spaces = await connection.Spaces
                .GetAll(systemAccount.Id)
                .ToArrayAsync();

            // Assert.
            // Each user is initialized with at least a configuration and a data space. so we need to expect two spaces .
            Assert.Equal(2, spaces.Count());
            Assert.True(spaces.SingleOrDefault(s => s.Name == "System") != null, "System space not found");
            Assert.True(spaces.SingleOrDefault(s => s.Name == "Metrics") != null, "Metrics space not found");
        }
        [Fact, Trait("Category", TestAssembly.Category)]

        public async Task ManagementConnection_Administrator_Spaces()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateManagementConnection().ConfigureAwait(false);
            var administratorAccount = await connection.Accounts.Get("Administrator").ConfigureAwait(false);

            // Act.
            var spaces = await connection.Spaces
                .GetAll(administratorAccount.Id)
                .ToArrayAsync();

            // Assert.
            Assert.Equal(2, spaces.Count());
            //Assert.True(spaces.SingleOrDefault(s => s.Name == "System") != null, "System space not found")
            Assert.True(spaces.SingleOrDefault(s => s.Name == "Configuration") != null, "Configuration space not found");
            Assert.True(spaces.SingleOrDefault(s => s.Name == "Data") != null, "Data space not found");
        }
    }
}
