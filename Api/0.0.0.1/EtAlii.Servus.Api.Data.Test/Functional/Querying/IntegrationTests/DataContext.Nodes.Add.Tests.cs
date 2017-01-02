namespace EtAlii.Servus.Api.Data.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Data.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using TestAssembly = EtAlii.Servus.Api.Data.Tests.TestAssembly;

    public partial class DataContext_Nodes_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Cast_Single()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            var monthPath = ApiTestHelper.SetupMonth(connection);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var path = String.Format("{0}", monthPath);
            var items = context.Nodes.Select(path);

            // Act.
            var single = items.Add("01").Cast<NamedObject>().Single();

            // Assert.
            Assert.AreEqual("01", single.Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            var monthPath = ApiTestHelper.SetupMonth(connection);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var path = String.Format("{0}", monthPath);
            var items = context.Nodes.Select(path);

            // Act.
            dynamic single = items.Add("01").Single();

            // Assert.
            Assert.AreEqual("01", single.Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Timed_01()
        {
            // Arrange.
            var start = Environment.TickCount;
            
            var delta = start;
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);

            Console.WriteLine("ApiTestHelper.CreateDataConnection: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            var monthPath = ApiTestHelper.SetupMonth(connection);

            Console.WriteLine("ApiTestHelper.SetupMonth: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            var context = new DataContextFactory().Create(connection, _diagnostics);

            Console.WriteLine("DataContextFactory().Create: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            var path = String.Format("{0}", monthPath);
            var items = context.Nodes.Select(path);

            Console.WriteLine("context.Nodes.Select: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            // Act.
            dynamic single = items.Add("01").Single();

            Console.WriteLine("items.Add: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            // Assert.
            Assert.AreEqual("01", single.Type);
            Assert.IsTrue(3000 > Environment.TickCount - start);

            // Assure.
            context.Dispose();
            connection.Close();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Timed_02()
        {
            // Arrange.
            var start = Environment.TickCount;

            var delta = start;
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);

            Console.WriteLine("ApiTestHelper.CreateDataConnection: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            var monthPath = ApiTestHelper.SetupMonth(connection);

            Console.WriteLine("ApiTestHelper.SetupMonth: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            var context = new DataContextFactory().Create(connection, _diagnostics);

            Console.WriteLine("DataContextFactory().Create: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            var path = String.Format("{0}", monthPath);
            var items = context.Nodes.Select(path);

            Console.WriteLine("context.Nodes.Select: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            // Act.
            dynamic single = items.Add("01").Single();

            Console.WriteLine("items.Add: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            // Assert.
            Assert.AreEqual("01", single.Type);
            Assert.IsTrue(3000 > Environment.TickCount - start);

            // Assure.
            context.Dispose();
            connection.Close();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Timed_03()
        {
            // Arrange.
            var start = Environment.TickCount;

            var delta = start;
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);

            Console.WriteLine("ApiTestHelper.CreateDataConnection: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            var monthPath = ApiTestHelper.SetupMonth(connection);

            Console.WriteLine("ApiTestHelper.SetupMonth: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            var context = new DataContextFactory().Create(connection, _diagnostics);

            Console.WriteLine("DataContextFactory().Create: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            var path = String.Format("{0}", monthPath);
            var items = context.Nodes.Select(path);

            Console.WriteLine("context.Nodes.Select: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            // Act.
            dynamic single = items.Add("01").Single();

            Console.WriteLine("items.Add: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            // Assert.
            Assert.AreEqual("01", single.Type);
            Assert.IsTrue(3000 > Environment.TickCount - start);

            // Assure.
            context.Dispose();
            connection.Close();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Three_Times()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            var monthPath = ApiTestHelper.SetupMonth(connection);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var path = String.Format("{0}", monthPath);
            var items = context.Nodes.Select(path);

            // Act.
            dynamic single = items.Add("01").Single();

            // Assert.
            Assert.AreEqual("01", single.Type);

            Cleanup();
            Initialize();

            // Arrange.
            connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            monthPath = ApiTestHelper.SetupMonth(connection);
            context = new DataContextFactory().Create(connection, _diagnostics);
            path = String.Format("{0}", monthPath);
            items = context.Nodes.Select(path);

            // Act.
            single = items.Add("01").Single();

            // Assert.
            Assert.AreEqual("01", single.Type);

            Cleanup();
            Initialize();

            // Arrange.
            connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            monthPath = ApiTestHelper.SetupMonth(connection);
            context = new DataContextFactory().Create(connection, _diagnostics);
            path = String.Format("{0}", monthPath);
            items = context.Nodes.Select(path);

            // Act.
            single = items.Add("01").Single();

            // Assert.
            Assert.AreEqual("01", single.Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Invalid_Invalid_Character()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            var monthPath = ApiTestHelper.SetupMonth(connection);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var path = String.Format("{0}", monthPath);
            var items = context.Nodes.Select(path);

            // Act.
            var act = new Action(() => items.Add("\"01").Single());

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);
        }
    }
}
