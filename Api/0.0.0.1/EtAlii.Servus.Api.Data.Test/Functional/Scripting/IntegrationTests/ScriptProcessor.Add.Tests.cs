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
    public class ScriptProcessor_Add_Tests : EtAlii.Servus.Api.Data.Tests.TestBase
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
        public void ScriptProcessor_Add_1()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var now = DateTime.Now;
            object result = null;
            var addQueries = new[]
            {
                "/Time+=/{0:yyyy}",
                "/Time/{0:yyyy}+=/{0:MM}",
                "/Time/{0:yyyy}/{0:MM}+=/{0:dd}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}+=/{0:HH}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}+=/{0:mm}"
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery);
            var selectScript = _parser.Parse(selectQuery);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(addScript, scope, connection);
            dynamic firstMinuteEntry = result;
            result = null;
            _processor.Process(selectScript, scope, connection);
            dynamic secondMinuteEntry = result;

            // Assert.
            Assert.IsNotNull(firstMinuteEntry);
            Assert.IsNotNull(secondMinuteEntry);
            Assert.AreEqual(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Add_2()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var now = DateTime.Now;
            object result = null;
            var addQueries = new[]
            {
                "/Time+=/{0:yyyy}",
                "/Time/{0:yyyy}+=/{0:MM}",
                "/Time/{0:yyyy}/{0:MM}+=/{0:dd}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}+=/{0:HH}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}+=/{0:mm}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery);
            var selectScript = _parser.Parse(selectQuery);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(addScript, scope, connection);
            dynamic firstMinuteEntry = result;
            result = null;
            _processor.Process(selectScript, scope, connection);
            dynamic secondMinuteEntry = result;

            // Assert.
            Assert.IsNotNull(firstMinuteEntry);
            Assert.IsNotNull(secondMinuteEntry);
            Assert.AreEqual(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Add_3()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var now = DateTime.Now;
            object result = null;
            var addQueries = new[]
            {
                "/Time+=/{0:yyyy}",
                "/Time/{0:yyyy}+=/{0:MM}",
                "/Time/{0:yyyy}/{0:MM}+=/{0:dd}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}+=/{0:HH}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}+=/{0:mm}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}"
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery);
            var selectScript = _parser.Parse(selectQuery);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(addScript, scope, connection);
            dynamic firstMinuteEntry = result;
            result = null;
            _processor.Process(selectScript, scope, connection);
            dynamic secondMinuteEntry = result;

            // Assert.
            Assert.IsNotNull(firstMinuteEntry);
            Assert.IsNotNull(secondMinuteEntry);
            Assert.AreEqual(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Add_4()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var now = DateTime.Now;
            object result = null;
            var addQueries = new[]
            {
                "/Time+=/{0:yyyy}",
                "/Time/{0:yyyy}+=/{0:MM}",
                "/Time/{0:yyyy}/{0:MM}+=/{0:dd}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}+=/{0:HH}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}+=/{0:mm}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery);
            var selectScript = _parser.Parse(selectQuery);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(addScript, scope, connection);
            dynamic firstMinuteEntry = result;
            result = null;
            _processor.Process(selectScript, scope, connection);
            dynamic secondMinuteEntry = result;

            // Assert.
            Assert.IsNotNull(firstMinuteEntry);
            Assert.IsNotNull(secondMinuteEntry);
            Assert.AreEqual(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Add_At_Once_1()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var now = DateTime.Now;
            object result = null;
            var addQueries = new[]
            {
                "<= /Time+=/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}"
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery);
            var selectScript = _parser.Parse(selectQuery);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(addScript, scope, connection);
            dynamic firstMinuteEntry = result;
            result = null;
            _processor.Process(selectScript, scope, connection);
            dynamic secondMinuteEntry = result;

            // Assert.
            Assert.IsNotNull(firstMinuteEntry);
            Assert.IsNotNull(secondMinuteEntry);
            Assert.AreEqual(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Add_At_Once_2()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var now = DateTime.Now;
            object result = null;
            var addQueries = new[]
            {
                "/Time+=/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery);
            var selectScript = _parser.Parse(selectQuery);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(addScript, scope, connection);
            dynamic firstMinuteEntry = result;
            result = null;
            _processor.Process(selectScript, scope, connection);
            dynamic secondMinuteEntry = result;

            // Assert.
            Assert.IsNotNull(firstMinuteEntry);
            Assert.IsNotNull(secondMinuteEntry);
            Assert.AreEqual(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Add_At_Once_3()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var now = DateTime.Now;
            object result = null;
            var addQueries = new[]
            {
                "/Time+=/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery);
            var selectScript = _parser.Parse(selectQuery);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(addScript, scope, connection);
            dynamic firstMinuteEntry = result;
            result = null;
            _processor.Process(selectScript, scope, connection);
            dynamic secondMinuteEntry = result;

            // Assert.
            Assert.IsNotNull(firstMinuteEntry);
            Assert.IsNotNull(secondMinuteEntry);
            Assert.AreEqual(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Add_At_Once_4()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var now = DateTime.Now;
            object result = null;
            var addQueries = new[]
            {
                "/Time+=/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery);
            var selectScript = _parser.Parse(selectQuery);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(addScript, scope, connection);
            dynamic firstMinuteEntry = result;
            result = null;
            _processor.Process(selectScript, scope, connection);
            dynamic secondMinuteEntry = result;

            // Assert.
            Assert.IsNotNull(firstMinuteEntry);
            Assert.IsNotNull(secondMinuteEntry);
            Assert.AreEqual(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Add_Spaced_1()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var now = DateTime.Now;
            object result = null;
            var addQueries = new[]
            {
                "/Time +=/{0:yyyy}",
                "/Time/{0:yyyy} +=/{0:MM}",
                "/Time/{0:yyyy}/{0:MM} +=/{0:dd}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd} +=/{0:HH}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH} +=/{0:mm}",
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery);
            var selectScript = _parser.Parse(selectQuery);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(addScript, scope, connection);
            dynamic firstMinuteEntry = result;
            result = null;
            _processor.Process(selectScript, scope, connection);
            dynamic secondMinuteEntry = result;

            // Assert.
            Assert.IsNotNull(firstMinuteEntry);
            Assert.IsNotNull(secondMinuteEntry);
            Assert.AreEqual(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Add_Spaced_2()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var now = DateTime.Now;
            object result = null;
            var addQueries = new[]
            {
                "/Time+= /{0:yyyy}",
                "/Time/{0:yyyy}+= /{0:MM}",
                "/Time/{0:yyyy}/{0:MM}+= /{0:dd}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}+= /{0:HH}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}+= /{0:mm}"
            };

            var addQuery = String.Format(String.Join("\r\n", addQueries), now);
            var selectQuery = String.Format("/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery);
            var selectScript = _parser.Parse(selectQuery);
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(addScript, scope, connection);
            dynamic firstMinuteEntry = result;
            result = null;
            _processor.Process(selectScript, scope, connection);
            dynamic secondMinuteEntry = result;

            // Assert.
            Assert.IsNotNull(firstMinuteEntry);
            Assert.IsNotNull(secondMinuteEntry);
            Assert.AreEqual(((INode)firstMinuteEntry).Id, ((INode)secondMinuteEntry).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Advanced_Add_Tree_At_Once()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var addQuery = "<= /Contacts+=/Doe/John";
            var selectQuery = "/Contacts/Doe/John";

            var addScript = _parser.Parse(addQuery);
            var selectScript = _parser.Parse(selectQuery);
            object result = null;
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(addScript, scope, connection);
            dynamic firstJohnEntry = result;
            result = null;
            _processor.Process(selectScript, scope, connection);
            dynamic secondJohnEntry = result;

            // Assert.
            Assert.IsNotNull(firstJohnEntry);
            Assert.IsNotNull(secondJohnEntry);
            Assert.AreEqual(((INode)firstJohnEntry).Id, ((INode)secondJohnEntry).Id);

        }

        [TestMethod, TestCategory(TestAssembly.Category), Ignore]
        public void ScriptProcessor_Advanced_Add_Tree_At_Once_From_Root()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var addQuery = "/+=Contacts/Doe/John";
            var selectQuery = "/Contacts/Doe/John";

            var addScript = _parser.Parse(addQuery);
            var selectScript = _parser.Parse(selectQuery);
            object result = null;
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(addScript, scope, connection);
            dynamic firstJohnEntry = result;
            result = null;
            _processor.Process(selectScript, scope, connection);
            dynamic secondJohnEntry = result;

            // Assert.
            Assert.IsNotNull(firstJohnEntry);
            Assert.IsNotNull(secondJohnEntry);
            Assert.AreEqual(((INode)firstJohnEntry).Id, ((INode)secondJohnEntry).Id);

        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptProcessor_Advanced_Add_Tree_At_Once_Spaced()
        {
            // Arrange.
            var connection = ApiTestHelper.CreateDataConnection(Host, SpaceName, AccountName, AccountPassword);

            var addQuery = "<= /Contacts += /Doe/John";
            var selectQuery = "/Contacts/Doe/John";

            var addScript = _parser.Parse(addQuery);
            var selectScript = _parser.Parse(selectQuery);
            object result = null;
            var scope = new ScriptScope(o => result = o);

            // Act.
            _processor.Process(addScript, scope, connection);
            dynamic firstJohnEntry = result;
            result = null;
            _processor.Process(selectScript, scope, connection);
            dynamic secondJohnEntry = result;

            // Assert.
            Assert.IsNotNull(firstJohnEntry);
            Assert.IsNotNull(secondJohnEntry);
            Assert.AreEqual(((INode)firstJohnEntry).Id, ((INode)secondJohnEntry).Id);

        }
    }
}