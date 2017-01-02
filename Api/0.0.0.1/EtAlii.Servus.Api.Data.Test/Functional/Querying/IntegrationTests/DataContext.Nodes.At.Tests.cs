namespace EtAlii.Servus.Api.Data.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Data.Tests;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using TestAssembly = EtAlii.Servus.Api.Data.Tests.TestAssembly;

    public partial class DataContext_Nodes_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category), Ignore]
        public void Linq_Nodes_Select_Add_At()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            var monthPath = ApiTestHelper.SetupMonth(connection);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var path = String.Format("{0}", monthPath);
            var items = context.Nodes.Select(path);

            // Act.
            dynamic single = items.Add("01").At(DateTime.Now).Single();

            // Assert.
            Assert.AreEqual("01", single.Label);
        }

        [TestMethod, TestCategory(TestAssembly.Category), Ignore]
        public void Linq_Nodes_Select_Add_At_Cast()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            var monthPath = ApiTestHelper.SetupMonth(connection);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var path = String.Format("{0}", monthPath);
            var items = context.Nodes.Select(path);

            // Act.
            var single = items.Add("01").At(DateTime.Now).Cast<NamedObject>().Single();

            // Assert.
            Assert.AreEqual("01", single.Type);
        }
    }
}
