namespace EtAlii.Servus.Api.Transport.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DataConnection_Tests
    {
        private static ITransportTestContext _testContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var task = Task.Run(async () =>
            {
                _testContext = new TransportTestContextFactory().Create();
                await _testContext.Start();
            });
            task.Wait();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            var task = Task.Run(async () =>
            {
                await _testContext.Stop();
                _testContext = null;
            });
            task.Wait();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var task = Task.Run(() =>
            {
            });
            task.Wait();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var task = Task.Run(() =>
            {
            });
            task.Wait();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataConnection_Open_Space_System()
        {
            // Arrange.

            // Act.
            var connection = await _testContext.CreateDataConnection(_testContext.Context.SystemAccountName, _testContext.Context.SystemAccountPassword, SpaceName.System, false, false);
            await connection.Open();

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataConnection_Open_Space_Data()
        {
            // Arrange.
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();

            // Act.
            var connection = await _testContext.CreateDataConnection(accountName, password, spaceName, false, true, SpaceTemplate.Data);
            await connection.Open();

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataConnection_Open_Space_Configuration()
        {
            // Arrange.
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();

            // Act.
            var connection = await _testContext.CreateDataConnection(accountName, password, spaceName, false, true, SpaceTemplate.Configuration);
            await connection.Open();

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataConnection_Open_Twice()
        {
            // Arrange.
            var connection = await _testContext.CreateDataConnection(_testContext.Context.SystemAccountName, _testContext.Context.SystemAccountPassword, SpaceName.System, false, false);
            await connection.Open();

            // Act.
            var act = connection.Open();

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataConnection_Open_Invalid_Password()
        {
            // Arrange.
            var connection = await _testContext.CreateDataConnection(_testContext.Context.SystemAccountName, _testContext.Context.SystemAccountPassword + "BAAD", SpaceName.System, false, false);

            // Act.
            var act = connection.Open();

            // Assert.
            await ExceptionAssert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataConnection_Open_Invalid_Account()
        {
            // Arrange.
            var connection = await _testContext.CreateDataConnection(_testContext.Context.SystemAccountName + "BAAD", _testContext.Context.SystemAccountPassword, SpaceName.System, false, false);

            // Act.
            var act = connection.Open();

            // Assert.
            await ExceptionAssert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataConnection_Open_Invalid_Space()
        {
            // Arrange.
            var connection = await _testContext.CreateDataConnection(_testContext.Context.SystemAccountName, _testContext.Context.SystemAccountPassword, SpaceName.System + "BAAD", false, false);

            // Act.
            var act = connection.Open();

            // Assert.
            await ExceptionAssert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataConnection_Open_Invalid_Account_And_Password()
        {
            // Arrange.
            var connection = await _testContext.CreateDataConnection(_testContext.Context.SystemAccountName + "BAAD", _testContext.Context.SystemAccountPassword + "BAAD", SpaceName.System, false, false);

            // Act.
            var act = connection.Open();

            // Assert.
            await ExceptionAssert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataConnection_Open_Invalid_Account_And_Password_And_Space()
        {
            // Arrange.
            var connection = await _testContext.CreateDataConnection(_testContext.Context.SystemAccountName + "BAAD", _testContext.Context.SystemAccountPassword + "BAAD", SpaceName.System + "BAAD", false, false);

            // Act.
            var act = connection.Open();

            // Assert.
            await ExceptionAssert.ThrowsAsync<UnauthorizedInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataConnection_Open_Already_Open()
        {
            // Arrange.
            var connection = await _testContext.CreateDataConnection(_testContext.Context.SystemAccountName, _testContext.Context.SystemAccountPassword, SpaceName.System, false, false);
            await connection.Open();

            // Act.
            var act = connection.Open();

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataConnection_Open_And_Close_System()
        {
            var connection = await _testContext.CreateDataConnection(_testContext.Context.SystemAccountName, _testContext.Context.SystemAccountPassword, SpaceName.System, false, false);
            await connection.Open();
            await connection.Close();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataConnection_Open_And_Close_Data()
        {
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();

            var connection = await _testContext.CreateDataConnection(accountName, password, spaceName, false, true);
            await connection.Open();
            await connection.Close();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task DataConnection_Close()
        {
            // Arrange.
            var connection = await _testContext.CreateDataConnection(false);

            // Act.
            var act = connection.Close();

            // Assert.
            await ExceptionAssert.ThrowsAsync<InvalidInfrastructureOperationException>(act);
        }
    }
}
