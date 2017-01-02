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
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Cast_ToArray_With_Single_Item()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            IEditableEntry monthEntry;
            var monthPath = ApiTestHelper.AddYearMonth(connection, out monthEntry);
            ApiTestHelper.AddDays(connection, monthEntry, 1);
            var path = String.Format("{0}/", monthPath);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var items = context.Nodes.Select(path);

            // Act.
            var single = items.Cast<NamedObject>().ToArray();

            // Assert.
            Assert.AreEqual("01", single[0].Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Cast_ToArray_With_Multiple_Item()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            IEditableEntry monthEntry;
            var monthPath = ApiTestHelper.AddYearMonth(connection, out monthEntry);
            ApiTestHelper.AddDays(connection, monthEntry, 2);
            var path = String.Format("{0}/", monthPath);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var items = context.Nodes.Select(path);

            // Act.
            var single = items.Cast<NamedObject>().ToArray();

            // Assert.
            Assert.AreEqual("01", single[0].Type);
            Assert.AreEqual("02", single[1].Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_ToArray_With_Multiple_Item()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            IEditableEntry monthEntry;
            var monthPath = ApiTestHelper.AddYearMonth(connection, out monthEntry);
            ApiTestHelper.AddDays(connection, monthEntry, 2);
            var path = String.Format("{0}/", monthPath);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var items = context.Nodes.Select(path);

            // Act.
            dynamic single = items.ToArray();

            // Assert.
            Assert.AreEqual("01", single[0].Type);
            Assert.AreEqual("02", single[1].Type);
        }
    }
}
