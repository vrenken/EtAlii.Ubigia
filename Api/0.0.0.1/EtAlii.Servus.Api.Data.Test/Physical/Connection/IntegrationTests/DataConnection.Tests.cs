namespace EtAlii.Servus.Api.Data.IntegrationTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using TestAssembly = EtAlii.Servus.Api.Data.Tests.TestAssembly;


    [TestClass]
    public class DataConnection_Tests : EtAlii.Servus.Api.Data.Tests.TestBase
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Open()
        {
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, false);
            connection.Open(Host.Configuration.Address, AccountName, AccountPassword, SpaceName);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Open_Twice()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, false);
            connection.Open(Host.Configuration.Address, AccountName, AccountPassword, SpaceName);

            // Act.
            var act = new Action(() =>
            {
                connection.Open(Host.Configuration.Address, AccountName, AccountPassword, SpaceName);
            });

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Open_Invalid_Password()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, false);

            // Act.
            var act = new Action(() =>
            {
                connection.Open(Host.Configuration.Address, AccountName, AccountPassword + "BAAD", SpaceName);
            });

            // Assert.
            ExceptionAssert.Throws<UnauthorizedInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Open_Invalid_Account()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, false);

            // Act.
            var act = new Action(() =>
            {
                connection.Open(Host.Configuration.Address, AccountName + "BAAD", AccountPassword, SpaceName);
            });

            // Assert.
            ExceptionAssert.Throws<UnauthorizedInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Open_Invalid_Space()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, false);

            // Act.
            var act = new Action(() =>
            {
                connection.Open(Host.Configuration.Address, AccountName, AccountPassword, SpaceName + "BAAD");
            });

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Open_Invalid_Account_And_Password()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, false);

            // Act.
            var act = new Action(() =>
            {
                connection.Open(Host.Configuration.Address, AccountName + "BAAD", AccountPassword + "BAAD", SpaceName);
            });

            // Assert.
            ExceptionAssert.Throws<UnauthorizedInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Open_Invalid_Account_And_Password_And_Space()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, false);

            // Act.
            var act = new Action(() =>
            {
                connection.Open(Host.Configuration.Address, AccountName + "BAAD", AccountPassword + "BAAD", SpaceName + "BAAD");
            });

            // Assert.
            ExceptionAssert.Throws<UnauthorizedInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Open_Already_Open()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, false);
            connection.Open(Host.Configuration.Address, AccountName, AccountPassword, SpaceName);

            // Act.
            var act = new Action(() =>
            {
                connection.Open(Host.Configuration.Address, AccountName, AccountPassword, SpaceName);
            });

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Open_And_Close()
        {
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, false);
            connection.Open(Host.Infrastructure.Configuration.Address, AccountName, AccountPassword, SpaceName);
            connection.Close();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void DataConnection_Close()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, false);

            // Act.
            var act = new Action(connection.Close);

            // Assert.
            ExceptionAssert.Throws<InvalidInfrastructureOperationException>(act);
        }
    }
}
