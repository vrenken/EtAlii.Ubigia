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

    [TestClass]
    public class ScriptProcessor_Advanced_Tests
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private static ILogicalTestContext _testContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var task = Task.Run(() =>
            {
            });
            task.Wait();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            var task = Task.Run(() =>
            {
            });
            task.Wait();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var task = Task.Run(async () =>
            {
                _testContext = new LogicalTestContextFactory().Create();
                await _testContext.Start();

                _diagnostics = TestDiagnostics.Create();
                var scriptParserConfiguration = new ScriptParserConfiguration()
                    .Use(_diagnostics);
                _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
            });
            task.Wait();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var task = Task.Run(async () =>
            {
                _parser = null;

                await _testContext.Stop();
                _testContext = null;
            });
            task.Wait();
        }

        [TestMethod]
        public async Task ScriptProcessor_Advanced_Create()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Should_Not_Clear_Children()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
                "/Contacts+=Doe/Jane",
                "/Contacts+=Doe/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/";
            var assignQuery = "/Contacts/Doe <= { Type: 'Family' }";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var assignScript = _parser.Parse(assignQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var personsBefore = await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(assignScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var personsAfter = await lastSequence.Output.ToArray();

            // Assert.
            Assert.IsNotNull(personsBefore);
            Assert.IsNotNull(personsAfter);
            Assert.AreEqual(3, personsBefore.Count());
            Assert.AreEqual(3, personsAfter.Count());
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_To_Variable_And_Then_ReUse_01()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
                "/Contacts+=Doe/Jane",
                "/Contacts+=Doe/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var select1Query = "/Contacts/Doe/Jane";
            var select2Query = "$contacts <= /Contacts/Doe\r\n/$contacts/Jane";

            var addScript = _parser.Parse(addQuery).Script;
            var select1Script = _parser.Parse(select1Query).Script;
            var select2Script = _parser.Parse(select2Query).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(select1Script);
            var firstResult = await lastSequence.Output.Cast<INode>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(select2Script);
            var secondResult = await lastSequence.Output.Cast<INode>().SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(firstResult);
            Assert.IsNotNull(secondResult);
            Assert.AreEqual(firstResult.Id, secondResult.Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_To_Variable_And_Then_ReUse_02()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
                "/Contacts+=Doe/Jane",
                "/Contacts+=Doe/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var select1Query = "/Contacts/Doe/";
            var select2Query = "$contacts <= /Contacts/Doe\r\n/$contacts/";

            var addScript = _parser.Parse(addQuery).Script;
            var select1Script = _parser.Parse(select1Query).Script;
            var select2Script = _parser.Parse(select2Query).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(select1Script);
            var firstResult = await lastSequence.Output.Cast<INode>().ToArray();
            lastSequence = await processor.Process(select2Script);
            var secondResult = await lastSequence.Output.Cast<INode>().ToArray();

            // Assert.
            Assert.IsNotNull(firstResult);
            Assert.IsNotNull(secondResult);
            Assert.AreEqual(3, firstResult.Count());
            Assert.AreEqual(3, secondResult.Count());
            Assert.AreEqual(firstResult[0].Id, secondResult[0].Id);
            Assert.AreEqual(firstResult[1].Id, secondResult[1].Id);
            Assert.AreEqual(firstResult[2].Id, secondResult[2].Id);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_To_Variable_And_Then_ReUse_03()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
                "/Contacts+=Doe/Jane",
                "/Contacts+=Doe/Johnny",
                "/Contacts+=Janssen/Jan",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var select1Query = "/Contacts/Doe/";
            var select2Query = "$contacts <= /Contacts\r\n/$contacts/Doe/";

            var addScript = _parser.Parse(addQuery).Script;
            var select1Script = _parser.Parse(select1Query).Script;
            var select2Script = _parser.Parse(select2Query).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(select1Script);
            var firstResult = await lastSequence.Output.Cast<INode>().ToArray();
            lastSequence = await processor.Process(select2Script);
            var secondResult = await lastSequence.Output.Cast<INode>().ToArray();

            // Assert.
            Assert.IsNotNull(firstResult);
            Assert.IsNotNull(secondResult);
            Assert.AreEqual(3, firstResult.Count());
            Assert.AreEqual(3, secondResult.Count());
            Assert.AreEqual(firstResult[0].Id, secondResult[0].Id);
            Assert.AreEqual(firstResult[1].Id, secondResult[1].Id);
            Assert.AreEqual(firstResult[2].Id, secondResult[2].Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_To_Variable_And_Then_ReUse_04()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
                "/Contacts+=Doe/Jane",
                "/Contacts+=Doe/Johnny",
                "/Contacts+=Janssen/Jan",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var select1Query = "/Contacts/Doe/";
            var select2Query = "$contacts <= /Contacts\r\n<= id() <= /$contacts/Doe/";

            var addScript = _parser.Parse(addQuery).Script;
            var select1Script = _parser.Parse(select1Query).Script;
            var select2Script = _parser.Parse(select2Query).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(select1Script);
            var firstResult = await lastSequence.Output.Cast<INode>().ToArray();
            lastSequence = await processor.Process(select2Script);
            var secondResult = await lastSequence.Output.Cast<Identifier>().ToArray();

            // Assert.
            Assert.IsNotNull(firstResult);
            Assert.IsNotNull(secondResult);
            Assert.AreEqual(3, firstResult.Count(), "First result is not correct");
            Assert.AreEqual(3, secondResult.Count(), "Second result is not correct");
            Assert.AreEqual(firstResult[0].Id, secondResult[0]);
            Assert.AreEqual(firstResult[1].Id, secondResult[1]);
            Assert.AreEqual(firstResult[2].Id, secondResult[2]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Special_Characters()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/\"Jöhn\"",
                "/Contacts+=Doe/\"Jóhn\"",
                "/Contacts+=Doe/\"Jähn\"",
                "/Contacts+=Doe/\"Jánê\"",
                "/Contacts+=Doe/\"Jöhnny\"",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.AreEqual(5, result.Count());
            Assert.AreEqual("Jöhn", result.Skip(0).First().ToString());
            Assert.AreEqual("Jóhn", result.Skip(1).First().ToString());
            Assert.AreEqual("Jähn", result.Skip(2).First().ToString());
            Assert.AreEqual("Jánê", result.Skip(3).First().ToString());
            Assert.AreEqual("Jöhnny", result.Skip(4).First().ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Children_Should_Not_Clear_Assign()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQuery1 = "/Contacts += Doe";
            var addQueries2 = new[]
            {
                "/Contacts+=Doe/John",
                "/Contacts+=Doe/Jane",
                "/Contacts+=Doe/Johnny",
            };
            var addQuery2 = String.Join("\r\n", addQueries2);
            var selectQuery = "/Contacts/Doe";
            var assignQuery = "/Contacts/Doe <= { ObjectType: 'Family' }";

            var addScript1 = _parser.Parse(addQuery1).Script;
            var addScript2 = _parser.Parse(addQuery2).Script;
            var assignScript = _parser.Parse(assignQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(assignScript);
            var result = await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic familyBefore = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(addScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic familyAfter = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(familyBefore);
            Assert.IsNotNull(familyAfter);
            Assert.AreEqual("Family", familyBefore.ObjectType);
            Assert.AreEqual("Family", familyAfter.ObjectType);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Move_Child()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries1 = new []
            {
              "/Contacts += Doe",
              "/Contacts += Does"
            };
            var addQueries2 = new[]
            {
                "/Contacts/Doe += John",
                "/Contacts/Doe += Johnny",
                "/Contacts/Does += Jane",
            };

            var moveQueries = new[]
            {
                "$john <= /Contacts/Doe/John",
                "/Contacts/Does += $john",
                "/Contacts/Doe -= John",
            };

            var addQuery1 = String.Join("\r\n", addQueries1);
            var addQuery2 = String.Join("\r\n", addQueries2);
            var moveQuery = String.Join("\r\n", moveQueries);
            var selectQuery1 = "/Contacts/Doe/";
            var selectQuery2 = "/Contacts/Does/";

            var addScript1 = _parser.Parse(addQuery1).Script;
            var addScript2 = _parser.Parse(addQuery2).Script;
            var selectScript1 = _parser.Parse(selectQuery1).Script;
            var selectScript2 = _parser.Parse(selectQuery2).Script;
            var moveScript = _parser.Parse(moveQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(addScript2);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(selectScript1);
            var result = await lastSequence.Output.ToArray();
            var beforeCount1 = result.Length;
            lastSequence = await processor.Process(selectScript2);
            result = await lastSequence.Output.ToArray();
            var beforeCount2 = result.Length;

            lastSequence = await processor.Process(moveScript);
            await lastSequence.Output.ToArray();
            
            lastSequence = await processor.Process(selectScript1);
            result = await lastSequence.Output.ToArray();
            var afterCount1 = result.Length;
            lastSequence = await processor.Process(selectScript2);
            result = await lastSequence.Output.ToArray();
            var afterCount2 = result.Length;


            // Assert.
            Assert.AreEqual(2, beforeCount1);
            Assert.AreEqual(1, beforeCount2);
            Assert.AreEqual(1, afterCount1);
            Assert.AreEqual(2, afterCount2);
        }
    }
}