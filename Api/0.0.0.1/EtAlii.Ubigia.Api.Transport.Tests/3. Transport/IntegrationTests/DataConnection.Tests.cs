namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Tests;
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
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Space_System()
        {
            // Arrange.

            // Act.
            var connection = await _testContext.TransportTestContext.CreateDataConnection(
                _testContext.TransportTestContext.Context.SystemAccountName, 
                _testContext.TransportTestContext.Context.SystemAccountPassword, SpaceName.System, false, false);
            await connection.Open();

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Space_Data()
        {
            // Arrange.
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();

            // Act.
            var connection = await _testContext.TransportTestContext.CreateDataConnection(accountName, password, spaceName, false, true, SpaceTemplate.Data);
            await connection.Open();

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Space_Configuration()
        {
            // Arrange.
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();

            // Act.
            var connection = await _testContext.TransportTestContext.CreateDataConnection(accountName, password, spaceName, false, true, SpaceTemplate.Configuration);
            await connection.Open();

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Twice()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateDataConnection(
                _testContext.TransportTestContext.Context.SystemAccountName, 
                _testContext.TransportTestContext.Context.SystemAccountPassword, SpaceName.System, false, false);
            await connection.Open();

            // Act.
            var act = connection.Open();

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Invalid_Password()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateDataConnection(
                _testContext.TransportTestContext.Context.SystemAccountName, 
                _testContext.TransportTestContext.Context.SystemAccountPassword + "BAAD", SpaceName.System, false, false);

            // Act.
            var act = connection.Open();

            // Assert.
            await ExceptionAssert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Invalid_Account()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateDataConnection(
                _testContext.TransportTestContext.Context.SystemAccountName + "BAAD", 
                _testContext.TransportTestContext.Context.SystemAccountPassword, SpaceName.System, false, false);

            // Act.
            var act = connection.Open();

            // Assert.
            await ExceptionAssert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Invalid_Space()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateDataConnection(
                _testContext.TransportTestContext.Context.SystemAccountName, 
                _testContext.TransportTestContext.Context.SystemAccountPassword, SpaceName.System + "BAAD", false, false);

            // Act.
            var act = connection.Open();

            // Assert.
            await ExceptionAssert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Invalid_Account_And_Password()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateDataConnection(
                _testContext.TransportTestContext.Context.SystemAccountName + "BAAD", 
                _testContext.TransportTestContext.Context.SystemAccountPassword + "BAAD", SpaceName.System, false, false);

            // Act.
            var act = connection.Open();

            // Assert.
            await ExceptionAssert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Invalid_Account_And_Password_And_Space()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateDataConnection(
                _testContext.TransportTestContext.Context.SystemAccountName + "BAAD", 
                _testContext.TransportTestContext.Context.SystemAccountPassword + "BAAD", SpaceName.System + "BAAD", false, false);

            // Act.
            var act = connection.Open();

            // Assert.
            await ExceptionAssert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_Already_Open()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateDataConnection(
                _testContext.TransportTestContext.Context.SystemAccountName, 
                _testContext.TransportTestContext.Context.SystemAccountPassword, SpaceName.System, false, false);
            await connection.Open();

            // Act.
            var act = connection.Open();

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_And_Close_System()
        {
            var connection = await _testContext.TransportTestContext.CreateDataConnection(
                _testContext.TransportTestContext.Context.SystemAccountName, 
                _testContext.TransportTestContext.Context.SystemAccountPassword, SpaceName.System, false, false);
            await connection.Open();
            await connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Open_And_Close_Data()
        {
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();

            var connection = await _testContext.TransportTestContext.CreateDataConnection(accountName, password, spaceName, false, true);
            await connection.Open();
            await connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task DataConnection_Close()
        {
            // Arrange.
            var connection = await _testContext.TransportTestContext.CreateDataConnection(false);

            // Act.
            var act = connection.Close();

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }
    }
}
