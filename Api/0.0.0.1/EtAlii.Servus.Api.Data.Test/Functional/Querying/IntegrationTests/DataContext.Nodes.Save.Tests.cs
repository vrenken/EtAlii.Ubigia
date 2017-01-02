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
        public void Linq_Nodes_Select_Add_Single_Save_Check_IsModified()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            var monthPath = ApiTestHelper.SetupMonth(connection);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var path = String.Format("{0}", monthPath);
            var items = context.Nodes.Select(path);
            var value = new Random().Next();
            var single = items.Add("01").Cast<NamedObject>().Single();

            // Act.
            single.Value = value;
            var wasModified = ((INode)single).IsModified;
            context.Nodes.Save(single);
            var isModified = ((INode)single).IsModified;

            // Assert.
            Assert.IsTrue(wasModified);
            Assert.IsFalse(isModified);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Save_Check_Id()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            var monthPath = ApiTestHelper.SetupMonth(connection);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var path = String.Format("{0}", monthPath);
            var items = context.Nodes.Select(path);
            var value = new Random().Next();
            var single = items.Add("01").Cast<NamedObject>().Single();
            var originalId = ((INode)single).Id;

            // Act.
            single.Value = value;
            context.Nodes.Save(single);
            var newId = ((INode)single).Id;

            // Assert.
            Assert.AreNotEqual(originalId, newId);
        }
    }
}
