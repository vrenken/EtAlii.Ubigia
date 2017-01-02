namespace EtAlii.Servus.Api.Data.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Infrastructure.Hosting.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using TestAssembly = EtAlii.Servus.Api.Data.Tests.TestAssembly;


    [TestClass]
    public class ScriptProcessor_Get_Tests : EtAlii.Servus.Api.Data.Tests.TestBase
    {
        private IScriptProcessor _processor;
        private IScriptParser _parser;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            var diagnostics = ApiTestHelper.CreateDiagnostics();
            _parser = new ScriptParserFactory().Create(diagnostics);
            _processor = new ScriptProcessorFactory().Create(diagnostics);
        }

        [TestCleanup]
        public override void Cleanup()
        {
            _parser = null;
            _processor = null;

            base.Cleanup();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Get_GetItem()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            object result = null;
            const string query = "/Time";
            var script = _parser.Parse(query);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(script, scope, connection);

            // Assert.
            Assert.IsNotNull(script);
            Assert.IsNotNull(result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Get_GetItemByVariable_1()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            dynamic result = null;
            var queries = new[]
            {
                "$var1 <= /Time",
                "$var1"
            };

            var query = String.Join("\r\n", queries);
            var script = _parser.Parse(query);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(script, scope, connection);

            // Assert.
            Assert.IsNotNull(result);
            Assert.IsTrue(result is DynamicNode);
            Assert.AreEqual("Time", result.Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Get_GetItemByVariable_2()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            dynamic result = null;
            var queries = new[]
            {
                "$var1 <= /\"Time\"",
                "$var1"
            };

            var query = String.Join("\r\n", queries);
            var script = _parser.Parse(query);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(script, scope, connection);

            // Assert.
            Assert.IsNotNull(result);
            Assert.IsTrue(result is DynamicNode);
            Assert.AreEqual("Time", result.Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Get_GetItemByVariables_1()
        {
            // Arrange.
            var now = DateTime.Now;
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            dynamic result = null;
            var queries = new[]
            {
                "/Time+=/{0:yyyy}",
                "$var1 <= Time",
                "$var2 <= {0:yyyy}",
                "/$var1/$var2"
            };

            var query = String.Format(String.Join("\r\n", queries), now);
            var script = _parser.Parse(query);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(script, scope, connection);

            // Assert.
            Assert.IsNotNull(result);
            Assert.IsTrue(result is DynamicNode);
            Assert.AreEqual(string.Format("{0:yyyy}", now), result.Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Get_GetItemByVariables_2()
        {
            // Arrange.
            var now = DateTime.Now;
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            dynamic result = null;
            var queries = new[]
            {
                "/Time+=/{0:yyyy}",
                "$var1 <= \"Time\"",
                "$var2 <= \"{0:yyyy}\"",
                "/$var1/$var2"
            };

            var query = String.Format(String.Join("\r\n", queries), now);
            var script = _parser.Parse(query);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(script, scope, connection);

            // Assert.
            Assert.IsNotNull(result);
            Assert.IsTrue(result is DynamicNode);
            Assert.AreEqual(string.Format("{0:yyyy}", now), result.Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Get_GetItemByVariables_Spaced()
        {
            // Arrange.
            var now = DateTime.Now;
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            dynamic result = null;
            var queries = new[]
            {
                "/Time += /{0:yyyy}",
                "$var1 <= \"Time\"",
                "$var2 <= \"{0:yyyy}\"",
                "/$var1/$var2"
            };

            var query = String.Format(String.Join("\r\n", queries), now);
            var script = _parser.Parse(query);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(script, scope, connection);

            // Assert.
            Assert.IsNotNull(result);
            Assert.IsTrue(result is DynamicNode);
            Assert.AreEqual(string.Format("{0:yyyy}", now), result.Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Get_GetItemByCompositeVariable()
        {
            // Arrange.
            var now = DateTime.Now;
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            dynamic result = null;
            var queries = new[]
            {
                "/Time+=/{0:yyyy}",
                "$var1 <= \"Time/{0:yyyy}\"",
                "/$var1"
            };

            var query = String.Format(String.Join("\r\n", queries), now);
            var script = _parser.Parse(query);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(script, scope, connection);

            // Assert.
            Assert.IsNotNull(result);
            Assert.IsTrue(result is DynamicNode);
            Assert.AreEqual(String.Format("{0:yyyy}", now), result.Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Get_GetItemByCompositeVariable_Spaced()
        {
            // Arrange.
            var now = DateTime.Now;
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);
            dynamic result = null;
            var queries = new[]
            {
                "/Time += /{0:yyyy}",
                "$var1 <= \"Time/{0:yyyy}\"",
                "/$var1"
            };

            var query = String.Format(String.Join("\r\n", queries), now);
            var script = _parser.Parse(query);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(script, scope, connection);

            // Assert.
            Assert.IsNotNull(result);
            Assert.IsTrue(result is DynamicNode);
            Assert.AreEqual(String.Format("{0:yyyy}", now), result.Type);
        }
    }
}