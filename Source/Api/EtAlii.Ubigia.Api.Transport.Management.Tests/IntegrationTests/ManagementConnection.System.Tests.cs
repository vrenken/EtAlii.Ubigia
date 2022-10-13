// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
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
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task ManagementConnection_System_Account()
        {
            // Arrange.
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);

            // Act.
            var systemAccount = await connection.Accounts.Get("System").ConfigureAwait(false);

            // Assert.
            Assert.NotNull(systemAccount);
        }

        [Fact]
        public async Task ManagementConnection_System_Spaces()
        {
            // Arrange.
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);
            var systemAccount = await connection.Accounts.Get("System").ConfigureAwait(false);

            // Act.
            var spaces = await connection.Spaces
                .GetAll(systemAccount.Id)
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            // Each user is initialized with at least a configuration and a data space. so we need to expect two spaces .
            Assert.Equal(3, spaces.Length);
            Assert.True(spaces.SingleOrDefault(s => s.Name == "System") != null, "System space not found");
            Assert.True(spaces.SingleOrDefault(s => s.Name == "Metrics") != null, "Metrics space not found");
            Assert.True(spaces.SingleOrDefault(s => s.Name == "Configuration") != null, "Configuration space not found");
        }
        [Fact]

        public async Task ManagementConnection_Administrator_Spaces()
        {
            // Arrange.
            var (connection, _) = await _testContext.Transport.CreateManagementConnection().ConfigureAwait(false);
            var administratorAccount = await connection.Accounts.Get("Administrator").ConfigureAwait(false);

            // Act.
            var spaces = await connection.Spaces
                .GetAll(administratorAccount.Id)
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.Equal(2, spaces.Length);
            //Assert.True(spaces.SingleOrDefault(s => s.Name == "System") != null, "System space not found")
            Assert.True(spaces.SingleOrDefault(s => s.Name == "Configuration") != null, "Configuration space not found");
            Assert.True(spaces.SingleOrDefault(s => s.Name == "Data") != null, "Data space not found");
        }
    }
}
