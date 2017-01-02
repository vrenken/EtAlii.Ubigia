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
        public void Linq_Nodes_Select()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            var context = new DataContextFactory().Create(connection, _diagnostics);

            // Act.
            var vacation = context.Nodes.Select("/Documents/Vacation/");

            // Assert.
            Assert.IsNotNull(vacation);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Cast_Single_With_Single_Item()
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
            var single = items.Cast<NamedObject>().Single();

            // Assert.
            Assert.AreEqual("01", single.Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Cast_With_Single_Item()
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
            dynamic single = items.Single();

            // Assert.
            Assert.AreEqual("01", single.Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Any_With_Multiple_Items()
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
            var any = items.Any();

            // Assert.
            Assert.IsTrue(any);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Any_With_Multiple_Items_Fail()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            var monthPath = ApiTestHelper.SetupMonth(connection);
            var path = String.Format("{0}/", monthPath);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var items = context.Nodes.Select(path);

            // Act.
            var any = items.Any();

            // Assert.
            Assert.IsFalse(any);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Any_With_Single_Item()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            IEditableEntry monthEntry;
            var monthPath = ApiTestHelper.AddYearMonth(connection, out monthEntry);
            ApiTestHelper.AddDays(connection, monthEntry, 1);
            var path = String.Format("{0}/01", monthPath);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var items = context.Nodes.Select(path);

            // Act.
            var any = items.Any();

            // Assert.
            Assert.IsTrue(any);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Any_With_Single_Item_Fail()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            var monthPath = ApiTestHelper.SetupMonth(connection);
            var path = String.Format("{0}/01", monthPath);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var items = context.Nodes.Select(path);
            var any = false;

            // Act.
            var act = new Action(() =>
            {
                any = items.Any();
            });

            // Assert.
            ExceptionAssert.Throws<ScriptProcessingException>(act);
            Assert.IsFalse(any);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Count_With_Single_Item()
        {
            // Arrange.
            const int days = 1;
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            IEditableEntry monthEntry;
            var monthPath = ApiTestHelper.AddYearMonth(connection, out monthEntry);
            ApiTestHelper.AddDays(connection, monthEntry, days);
            var path = String.Format("{0}/", monthPath);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var items = context.Nodes.Select(path);

            // Act.
            var count = items.Count();

            // Assert.
            Assert.AreEqual(days, count);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Count_With_Multiple_Items_2()
        {
            // Arrange.
            const int days = 2;
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            IEditableEntry monthEntry;
            var monthPath = ApiTestHelper.AddYearMonth(connection, out monthEntry);
            ApiTestHelper.AddDays(connection, monthEntry, days);
            var path = String.Format("{0}/", monthPath);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var items = context.Nodes.Select(path);

            // Act.
            var count = items.Count();

            // Assert.
            Assert.AreEqual(days, count);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Count_With_Multiple_Items_5()
        {
            // Arrange.
            const int days = 5;
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            IEditableEntry monthEntry;
            var monthPath = ApiTestHelper.AddYearMonth(connection, out monthEntry);
            ApiTestHelper.AddDays(connection, monthEntry, days);
            var path = String.Format("{0}/", monthPath);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var items = context.Nodes.Select(path);

            // Act.
            var count = items.Count();

            // Assert.
            Assert.AreEqual(days, count);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Count_With_Multiple_Items_20()
        {
            // Arrange.
            const int days = 20;
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            IEditableEntry monthEntry;
            var monthPath = ApiTestHelper.AddYearMonth(connection, out monthEntry);
            ApiTestHelper.AddDays(connection, monthEntry, days);
            var path = String.Format("{0}/", monthPath);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var items = context.Nodes.Select(path);

            // Act.
            var count = items.Count();

            // Assert.
            Assert.AreEqual(days, count);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Count_With_Multiple_Items_50()
        {
            // Arrange.
            const int days = 50;
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            IEditableEntry monthEntry;
            var monthPath = ApiTestHelper.AddYearMonth(connection, out monthEntry);
            ApiTestHelper.AddDays(connection, monthEntry, days);
            var path = String.Format("{0}/", monthPath);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var items = context.Nodes.Select(path);

            // Act.
            var count = items.Count();

            // Assert.
            Assert.AreEqual(days, count);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Count_With_Multiple_Items_50_Multiple_requests_20()
        {
            // Arrange.
            const int days = 50;
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            IEditableEntry monthEntry;
            var monthPath = ApiTestHelper.AddYearMonth(connection, out monthEntry);
            ApiTestHelper.AddDays(connection, monthEntry, days);
            var path = String.Format("{0}/", monthPath);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var items = context.Nodes.Select(path);
            const int iterations = 20;
            var counts = new int[iterations];

            // Act.
            for (int i = 0; i < iterations; i++)
            {
                counts[i] = items.Count();
            }

            // Assert.
            for (int i = 0; i < iterations; i++)
            {
                Assert.AreEqual(days, counts[i]);
            }
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Any()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword, true, true);
            var timeRoot = connection.Roots.Get(DefaultRoot.Time);
            var time = connection.Entries.Get(timeRoot);
            var now = DateTime.Now;
            var year = now.ToString("yyyy");
            var month = now.ToString("MM");
            var day = now.ToString("dd");
            ApiTestHelper.CreateHierarchy(connection, (IEditableEntry)time, year, month, day);
            var path = String.Format("/Time/{0}/{1}/{2}", year, month, day);
            var context = new DataContextFactory().Create(connection, _diagnostics);
            var items = context.Nodes.Select(path);

            // Act.
            var any = items.Any();

            // Assert.
            Assert.IsTrue(any);
        }
    }
}
