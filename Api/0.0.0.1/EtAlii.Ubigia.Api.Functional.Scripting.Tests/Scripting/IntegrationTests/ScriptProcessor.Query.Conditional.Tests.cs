namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Ubigia.Api.Tests.TestAssembly;

    [TestClass]
    public class ScriptProcessor_Query_Conditional_IntegrationTests
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private static ILogicalTestContext _testContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var task = Task.Run(async () =>
            {
                _testContext = new LogicalTestContextFactory().Create();
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
            var task = Task.Run(async () =>
            {
                _diagnostics = TestDiagnostics.Create();
                var scriptParserConfiguration = new ScriptParserConfiguration()
                    .Use(_diagnostics);
                _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
                _logicalContext = await _testContext.CreateLogicalContext(true);
            });
            task.Wait();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var task = Task.Run(() =>
            {
                _parser = null;
                _logicalContext.Dispose();
                _logicalContext = null;
            });
            task.Wait();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Query_Conditional_Equals_Boolean_01()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Contacts+=Does",
                "/Contacts/Does+=John",
                "/Contacts/Does+=Johnny",
                "/Contacts/Does+=Jane",
                "/Contacts/Does+=Janet",
                "/Contacts/Does+=Joanne",
                "/Contacts/Does+=Joan",
                "/Contacts/Does/John <= { IsMale: true }",
                "/Contacts/Does/Johnny <= { IsMale: true }",
                "/Contacts/Does/Jane <= { IsMale: false }",
                "/Contacts/Does/Janet <= { IsMale: false }",
                "/Contacts/Does/Joanne <= { IsMale: false }",
                "/Contacts/Does/Joan <= { IsMale: false }",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Does/.IsMale=true";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            dynamic first = result.First();
            dynamic second = result.Skip(1).First();

            Assert.AreEqual(true, first.IsMale);
            Assert.AreEqual("John", first.ToString());
            Assert.AreEqual(true, second.IsMale);
            Assert.AreEqual("Johnny", second.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Query_Conditional_Equals_Boolean_02()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Contacts+=Does",
                "/Contacts/Does+=John",
                "/Contacts/Does+=Johnny",
                "/Contacts/Does+=Jane",
                "/Contacts/Does+=Janet",
                "/Contacts/Does+=Joanne",
                "/Contacts/Does+=Joan",
                "/Contacts/Does/John <= { IsMale: true }",
                "/Contacts/Does/Johnny <= { IsMale: true }",
                "/Contacts/Does/Jane <= { IsMale: false }",
                "/Contacts/Does/Janet <= { IsMale: false }",
                "/Contacts/Does/Joanne <= { IsMale: false }",
                "/Contacts/Does/Joan <= { IsMale: false }",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Does/.IsMale=false";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count());
            dynamic first = result.First();
            dynamic second = result.Skip(1).First();
            dynamic third = result.Skip(2).First();
            dynamic fourth = result.Skip(3).First();

            Assert.AreEqual(false, first.IsMale);
            Assert.AreEqual(false, second.IsMale);
            Assert.AreEqual(false, third.IsMale);
            Assert.AreEqual(false, fourth.IsMale);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Query_Conditional_Equals_Boolean_03()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Contacts+=Does",
                "/Contacts/Does+=John",
                "/Contacts/Does+=Johnny",
                "/Contacts/Does+=Jane",
                "/Contacts/Does+=Janet",
                "/Contacts/Does+=Joanne",
                "/Contacts/Does+=Joan",
                "/Contacts/Does/John <= { IsMale: true }",
                "/Contacts/Does/Johnny <= { IsMale: true }",
                "/Contacts/Does/Jane <= { IsMale: false }",
                "/Contacts/Does/Janet <= { IsMale: false }",
                "/Contacts/Does/Joanne <= { IsMale: false }",
                "/Contacts/Does/Joan <= { IsMale: false }",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Does/.IsMale!=true";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count());
            dynamic first = result.First();
            dynamic second = result.Skip(1).First();
            dynamic third = result.Skip(2).First();
            dynamic fourth = result.Skip(3).First();

            Assert.AreEqual(false, first.IsMale);
            Assert.AreEqual(false, second.IsMale);
            Assert.AreEqual(false, third.IsMale);
            Assert.AreEqual(false, fourth.IsMale);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Query_Conditional_Equals_Boolean_04()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Contacts+=Does",
                "/Contacts/Does+=John",
                "/Contacts/Does+=Johnny",
                "/Contacts/Does+=Jane",
                "/Contacts/Does+=Janet",
                "/Contacts/Does+=Joanne",
                "/Contacts/Does+=Joan",
                "/Contacts/Does/John <= { IsMale: true }",
                "/Contacts/Does/Johnny <= { IsMale: true }",
                "/Contacts/Does/Jane <= { IsMale: false }",
                "/Contacts/Does/Janet <= { IsMale: false }",
                "/Contacts/Does/Joanne <= { IsMale: false }",
                "/Contacts/Does/Joan <= { IsMale: false }",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Does/.IsMale!=false";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            dynamic first = result.First();
            dynamic second = result.Skip(1).First();

            Assert.AreEqual(true, first.IsMale);
            Assert.AreEqual("John", first.ToString());
            Assert.AreEqual(true, second.IsMale);
            Assert.AreEqual("Johnny", second.ToString());
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Query_Conditional_Equals_DateTime_01()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Contacts+=Does",
                "/Contacts/Does+=John",
                "/Contacts/Does+=Johnny",
                "/Contacts/Does+=Jane",
                "/Contacts/Does+=Janet",
                "/Contacts/Does+=Joanne",
                "/Contacts/Does+=Joan",
                "/Contacts/Does/John <= { IsMale: true, Birthdate: 1978-08-23 }",
                "/Contacts/Does/Johnny <= { IsMale: true }",
                "/Contacts/Does/Jane <= { IsMale: false, Birthdate: 1991-11-07  }",
                "/Contacts/Does/Janet <= { IsMale: false, Birthdate: 1976-04-09  }",
                "/Contacts/Does/Joanne <= { IsMale: false, Birthdate: 1978-08-23  }",
                "/Contacts/Does/Joan <= { IsMale: false, Birthdate: 1982-02-12  }",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Does/.Birthdate=1978-08-23";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            dynamic first = result.First();
            dynamic second = result.Skip(1).First();

            Assert.AreEqual(new DateTime(1978,08, 23), first.Birthdate);
            Assert.AreEqual("John", first.ToString());
            Assert.AreEqual(new DateTime(1978, 08, 23), second.Birthdate);
            Assert.AreEqual("Joanne", second.ToString());
        }
    }
}