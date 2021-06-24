// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

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

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Space_System()
        {
            // Arrange.

            // Act.
            var connection = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Context.SystemAccountName, _testContext.TransportTestContext.Context.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);
            await connection.Open().ConfigureAwait(false);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Space_Data()
        {
            // Arrange.
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            // Act.
            var connection = await _testContext.TransportTestContext.CreateDataConnectionToNewSpace(accountName, password, false, SpaceTemplate.Data).ConfigureAwait(false);
            await connection.Open().ConfigureAwait(false);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Space_Configuration()
        {
            // Arrange.
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            // Act.
            var connection = await _testContext.TransportTestContext.CreateDataConnectionToNewSpace(accountName, password, false, SpaceTemplate.Configuration).ConfigureAwait(false);
            await connection.Open().ConfigureAwait(false);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Twice_Same()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Context.SystemAccountName, _testContext.TransportTestContext.Context.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);
            await connection.Open().ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Twice_New()
        {
            // Arrange.
            var connection1 = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Context.SystemAccountName,_testContext.TransportTestContext.Context.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);
            await connection1.Open().ConfigureAwait(false);

            // Act.
            var connection2 = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Context.SystemAccountName, _testContext.TransportTestContext.Context.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);
            await connection2.Open().ConfigureAwait(false);

            // Assert.
            Assert.True(connection1.IsConnected);
            Assert.True(connection2.IsConnected);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Context.SystemAccountName, _testContext.TransportTestContext.Context.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);

            // Act.
            await connection.Open().ConfigureAwait(false);

            // Assert.
            Assert.True(connection.IsConnected);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Invalid_Password()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Context.SystemAccountName, _testContext.TransportTestContext.Context.SystemAccountPassword + "BAAD", SpaceName.System, false)
                .ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Invalid_Account()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Context.SystemAccountName + "BAAD", _testContext.TransportTestContext.Context.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Invalid_Space()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Context.SystemAccountName, _testContext.TransportTestContext.Context.SystemAccountPassword, SpaceName.System + "BAAD", false)
                .ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Invalid_Account_And_Password()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Context.SystemAccountName + "BAAD", _testContext.TransportTestContext.Context.SystemAccountPassword + "BAAD", SpaceName.System, false)
                .ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Invalid_Account_And_Password_And_Space()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Context.SystemAccountName + "BAAD",_testContext.TransportTestContext.Context.SystemAccountPassword + "BAAD", SpaceName.System + "BAAD", false)
                .ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Already_Open()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Context.SystemAccountName, _testContext.TransportTestContext.Context.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);
            await connection.Open().ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Open().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_And_Close_System()
        {
            var connection = await _testContext.TransportTestContext
                .CreateDataConnectionToExistingSpace(_testContext.TransportTestContext.Context.SystemAccountName, _testContext.TransportTestContext.Context.SystemAccountPassword, SpaceName.System, false)
                .ConfigureAwait(false);
            await connection.Open().ConfigureAwait(false);
            await connection.Close().ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_And_Close_Data()
        {
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var connection = await _testContext.TransportTestContext.CreateDataConnectionToNewSpace(accountName, password, false).ConfigureAwait(false);
            await connection.Open().ConfigureAwait(false);
            await connection.Close().ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Close()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateDataConnectionToNewSpace(false).ConfigureAwait(false);

            // Act.
            var act = new Func<Task>(async () => await connection.Close().ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }
    }
}
