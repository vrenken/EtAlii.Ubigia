// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public class DataConnectionTests : IClassFixture<TransportUnitTestContext>, IDisposable
    {
        private readonly TransportUnitTestContext _testContext;

        public DataConnectionTests(TransportUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public void Dispose()
        {
            // Dispose any relevant resources.
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task DataConnection_Open_Space_System()
        {
            // Arrange.

            // Act.
            var (connection, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Host.SystemAccountName, _testContext.TransportTestContext.Host.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);
            await connection.Open().ConfigureAwait(false);

            // Assert.
        }

        [Fact]
        public async Task DataConnection_Open_Space_Data()
        {
            // Arrange.
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            // Act.
            var (connection, _) = await _testContext.TransportTestContext.CreateDataConnectionToNewSpace(accountName, password, false, SpaceTemplate.Data).ConfigureAwait(false);
            await connection.Open().ConfigureAwait(false);

            // Assert.
        }

        [Fact]
        public async Task DataConnection_Open_Space_Configuration()
        {
            // Arrange.
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            // Act.
            var (connection, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToNewSpace(accountName, password, false, SpaceTemplate.Configuration)
                .ConfigureAwait(false);
            await connection
                .Open()
                .ConfigureAwait(false);

            // Assert.
        }

        [Fact]
        public async Task DataConnection_Open_Twice_Same()
        {
            // Arrange.
            var (connection, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Host.SystemAccountName, _testContext.TransportTestContext.Host.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);
            await connection.Open().ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task DataConnection_Open_Twice_New()
        {
            // Arrange.
            var (connection1, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Host.SystemAccountName,_testContext.TransportTestContext.Host.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);
            await connection1
                .Open()
                .ConfigureAwait(false);

            // Act.
            var (connection2, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Host.SystemAccountName, _testContext.TransportTestContext.Host.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);
            await connection2
                .Open()
                .ConfigureAwait(false);

            // Assert.
            Assert.True(connection1.IsConnected);
            Assert.True(connection2.IsConnected);
        }

        [Fact]
        public async Task DataConnection_Open()
        {
            // Arrange.
            var (connection, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Host.SystemAccountName, _testContext.TransportTestContext.Host.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);

            // Act.
            await connection
                .Open()
                .ConfigureAwait(false);

            // Assert.
            Assert.True(connection.IsConnected);
        }

        [Fact]
        public async Task DataConnection_Open_Invalid_Password()
        {
            // Arrange.
            var (connection, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Host.SystemAccountName, _testContext.TransportTestContext.Host.SystemAccountPassword + "BAAD", SpaceName.System, false)
                .ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task DataConnection_Open_Invalid_Account()
        {
            // Arrange.
            var (connection, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Host.SystemAccountName + "BAAD", _testContext.TransportTestContext.Host.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task DataConnection_Open_Invalid_Space()
        {
            // Arrange.
            var (connection, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Host.SystemAccountName, _testContext.TransportTestContext.Host.SystemAccountPassword, SpaceName.System + "BAAD", false)
                .ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task DataConnection_Open_Invalid_Account_And_Password()
        {
            // Arrange.
            var (connection, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Host.SystemAccountName + "BAAD", _testContext.TransportTestContext.Host.SystemAccountPassword + "BAAD", SpaceName.System, false)
                .ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task DataConnection_Open_Invalid_Account_And_Password_And_Space()
        {
            // Arrange.
            var (connection, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Host.SystemAccountName + "BAAD",_testContext.TransportTestContext.Host.SystemAccountPassword + "BAAD", SpaceName.System + "BAAD", false)
                .ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert
                .ThrowsAsync<UnauthorizedInfrastructureOperationException>(act)
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task DataConnection_Open_Already_Open()
        {
            // Arrange.
            var (connection, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Host.SystemAccountName, _testContext.TransportTestContext.Host.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);
            await connection
                .Open()
                .ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task DataConnection_Open_And_Close_System()
        {
            var (connection, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Host.SystemAccountName, _testContext.TransportTestContext.Host.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);
            await connection
                .Open()
                .ConfigureAwait(false);
            await connection
                .Close()
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task DataConnection_Open_And_Close_Data()
        {
            // Arrange.
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            // Act.
            var (connection, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToNewSpace(accountName, password, false)
                .ConfigureAwait(false);

            // Assert.
            await connection
                .Open()
                .ConfigureAwait(false);
            await connection
                .Close()
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task DataConnection_Close()
        {
            // Arrange.
            var (connection, _) = await _testContext.TransportTestContext
                .CreateDataConnectionToNewSpace(false)
                .ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Close().ConfigureAwait(false));

            // Assert.
            await Assert
                .ThrowsAsync<InvalidInfrastructureOperationException>(act)
                .ConfigureAwait(false);
        }
    }
}
