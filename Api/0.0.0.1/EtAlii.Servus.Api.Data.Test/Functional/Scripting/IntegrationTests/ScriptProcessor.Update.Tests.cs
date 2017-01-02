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
    public class ScriptProcessor_Update_Tests : EtAlii.Servus.Api.Data.Tests.TestBase
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
        public void ScriptProcessor_Update_Update()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var now = DateTime.Now;
            object result = null;
            var addQueries = new[]
            {
                "/Time+=/2014",
                "/Time/2014+=/09",
                "/Time/2014/09+=/06",
                "/Time/2014/09/06+=/16",
                "<= /Time/2014/09/06/16+=/33"
            };

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var updateQuery = String.Format("<= /Time/2014/09/06/16/33 <= $data");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery);
            var updateScript = _parser.Parse(updateQuery);
            var selectScript = _parser.Parse(selectQuery);

            var scope = new ScriptScope(o => result = o);
            scope.Variables["data"] = new ScopeVariable(data, "data");

            // Act.
            _processor.Process(addScript, scope, connection);
            dynamic addResult = result;
            _processor.Process(updateScript, scope, connection);
            dynamic updateResult = result;
            _processor.Process(selectScript, scope, connection);
            dynamic selectResult = result;

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult);
            Assert.IsNotNull(selectResult);
            Assert.AreEqual("Test1", selectResult.StringValue);
            Assert.AreEqual(12, selectResult.IntValue);
            //Assert.AreNotEqual(addResult.Id, updateResult.Id);
            //Assert.AreNotEqual(updateResult.Id, selectResult.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Update_Update_Spaced()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var now = DateTime.Now;
            object result = null;
            var addQueries = new[]
            {
                "/Time += /2014",
                "/Time/2014 += /09",
                "/Time/2014/09 += /06",
                "/Time/2014/09/06 += /16",
                "<= /Time/2014/09/06/16 += /33"
            };

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var updateQuery = String.Format("<= /Time/2014/09/06/16/33 <= $data");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery);
            var updateScript = _parser.Parse(updateQuery);
            var selectScript = _parser.Parse(selectQuery);

            var scope = new ScriptScope(o => result = o);
            scope.Variables["data"] = new ScopeVariable(data, "data");

            // Act.
            _processor.Process(addScript, scope, connection);
            dynamic addResult = result;
            _processor.Process(updateScript, scope, connection);
            dynamic updateResult = result;
            _processor.Process(selectScript, scope, connection);
            dynamic selectResult = result;

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult);
            Assert.IsNotNull(selectResult);
            Assert.AreEqual("Test1", selectResult.StringValue);
            Assert.AreEqual(12, selectResult.IntValue);
            //Assert.AreNotEqual(addResult.Id, updateResult.Id);
            //Assert.AreNotEqual(updateResult.Id, selectResult.Id);
        }
    }
}