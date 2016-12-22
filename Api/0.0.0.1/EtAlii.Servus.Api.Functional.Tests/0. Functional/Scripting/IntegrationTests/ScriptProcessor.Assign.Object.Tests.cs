namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.Servus.Api.Logical.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Servus.Api.Tests.TestAssembly;

    [TestClass]
    public class ScriptProcessor_Assign_Object_IntegrationTests
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
        public async Task ScriptProcessor_Assign_Object_To_Path_String_And_String()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time+=/2014",
                "/Time/2014+=/09",
                "/Time/2014/09+=/06",
                "/Time/2014/09/06+=/16",
                "<= /Time/2014/09/06/16+=/33"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = String.Format("<= /Time/2014/09/06/16/33 <= {{ StringValue1 : 'Test1', StringValue2 : 'Test2' }}");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult);
            Assert.IsNotNull(selectResult);
            Assert.AreEqual("Test1", selectResult.StringValue1);
            Assert.AreEqual("Test2", selectResult.StringValue2);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Object_To_Path_String_And_Int()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time+=/2014",
                "/Time/2014+=/09",
                "/Time/2014/09+=/06",
                "/Time/2014/09/06+=/16",
                "<= /Time/2014/09/06/16+=/33"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = String.Format("<= /Time/2014/09/06/16/33 <= {{ StringValue : 'Test1', IntValue : 12 }}");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult);
            Assert.IsNotNull(selectResult);
            Assert.AreEqual("Test1", selectResult.StringValue);
            Assert.AreEqual(12, selectResult.IntValue);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Object_To_Path_String_And_Bool_True()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time+=/2014",
                "/Time/2014+=/09",
                "/Time/2014/09+=/06",
                "/Time/2014/09/06+=/16",
                "<= /Time/2014/09/06/16+=/33"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = String.Format("<= /Time/2014/09/06/16/33 <= {{ StringValue : 'Test1', BoolValue : true }}");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult);
            Assert.IsNotNull(selectResult);
            Assert.AreEqual("Test1", selectResult.StringValue);
            Assert.AreEqual(true, selectResult.BoolValue);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Object_To_Path_String_And_Bool_False()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time+=/2014",
                "/Time/2014+=/09",
                "/Time/2014/09+=/06",
                "/Time/2014/09/06+=/16",
                "<= /Time/2014/09/06/16+=/33"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = String.Format("<= /Time/2014/09/06/16/33 <= {{ StringValue : 'Test1', BoolValue : false }}");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult);
            Assert.IsNotNull(selectResult);
            Assert.AreEqual("Test1", selectResult.StringValue);
            Assert.AreEqual(false, selectResult.BoolValue);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Object_To_Path_String_And_Bool_False_Capitals()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time+=/2014",
                "/Time/2014+=/09",
                "/Time/2014/09+=/06",
                "/Time/2014/09/06+=/16",
                "<= /Time/2014/09/06/16+=/33"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = String.Format("<= /Time/2014/09/06/16/33 <= {{ StringValue : 'Test1', BoolValue : FALSE }}");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult);
            Assert.IsNotNull(selectResult);
            Assert.AreEqual("Test1", selectResult.StringValue);
            Assert.AreEqual(false, selectResult.BoolValue);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Object_To_Path_String_And_Bool_False_Camel()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time+=/2014",
                "/Time/2014+=/09",
                "/Time/2014/09+=/06",
                "/Time/2014/09/06+=/16",
                "<= /Time/2014/09/06/16+=/33"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = String.Format("<= /Time/2014/09/06/16/33 <= {{ StringValue : 'Test1', BoolValue : False }}");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult);
            Assert.IsNotNull(selectResult);
            Assert.AreEqual("Test1", selectResult.StringValue);
            Assert.AreEqual(false, selectResult.BoolValue);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Object_To_Path_Empty()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time+=/2014",
                "/Time/2014+=/09",
                "/Time/2014/09+=/06",
                "/Time/2014/09/06+=/16",
                "<= /Time/2014/09/06/16+=/33"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = String.Format("<= /Time/2014/09/06/16/33 <= {{ }}");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult);
            Assert.IsNotNull(selectResult);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Object_To_Path_Spaced()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time += /2014",
                "/Time/2014 += /09",
                "/Time/2014/09 += /06",
                "/Time/2014/09/06 += /16",
                "<= /Time/2014/09/06/16 += /33"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = String.Format("<= /Time/2014/09/06/16/33 <= {{ StringValue : 'Test1', IntValue : 12 }}");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult);
            Assert.IsNotNull(selectResult);
            Assert.AreEqual("Test1", selectResult.StringValue);
            Assert.AreEqual(12, selectResult.IntValue);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Object_To_Path_And_Update_01()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time += /2014",
                "/Time/2014 += /09",
                "/Time/2014/09 += /06",
                "/Time/2014/09/06 += /16",
                "<= /Time/2014/09/06/16 += /33"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery1 = String.Format("<= /Time/2014/09/06/16/33 <= {{ StringValue : 'Test1', IntValue : '12' }}");
            var updateQuery2 = String.Format("<= /Time/2014/09/06/16/33 <= {{ StringValue : 'Test2', IntValue : 13 }}");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript1 = _parser.Parse(updateQuery1).Script;
            var updateScript2 = _parser.Parse(updateQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript1);
            dynamic updateResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript2);
            dynamic updateResult2 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult1);
            Assert.IsNotNull(updateResult2);
            Assert.IsNotNull(selectResult1);
            Assert.AreEqual("Test1", selectResult1.StringValue);
            Assert.AreEqual("12", selectResult1.IntValue);
            Assert.IsNotNull(selectResult2);
            Assert.AreEqual("Test2", selectResult2.StringValue);
            Assert.AreEqual(13, selectResult2.IntValue);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Object_To_Path_And_Update_02()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time += /2014",
                "/Time/2014 += /09",
                "/Time/2014/09 += /06",
                "/Time/2014/09/06 += /16",
                "<= /Time/2014/09/06/16 += /33"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery1 = String.Format("<= /Time/2014/09/06/16/33 <= {{ StringValue : 'Test1', IntValue : '12' }}");
            var updateQuery2 = String.Format("<= /Time/2014/09/06/16/33 <= {{ StringValue : 'Test2', IntValue : '13', BoolValue : true  }}");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript1 = _parser.Parse(updateQuery1).Script;
            var updateScript2 = _parser.Parse(updateQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript1);
            dynamic updateResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript2);
            dynamic updateResult2 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult1);
            Assert.IsNotNull(updateResult2);
            Assert.IsNotNull(selectResult1);
            Assert.AreEqual("Test1", selectResult1.StringValue);
            Assert.AreEqual("12", selectResult1.IntValue);
            Assert.IsFalse(((IInternalNode)selectResult1).GetProperties().ContainsKey("BoolValue"));
            Assert.IsNotNull(selectResult2);
            Assert.AreEqual("Test2", selectResult2.StringValue);
            Assert.AreEqual("13", selectResult2.IntValue);
            Assert.AreEqual(true, selectResult2.BoolValue);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Object_To_Path_And_Update_03()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time += /2014",
                "/Time/2014 += /09",
                "/Time/2014/09 += /06",
                "/Time/2014/09/06 += /16",
                "<= /Time/2014/09/06/16 += /33"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery1 = String.Format("<= /Time/2014/09/06/16/33 <= {{ StringValue : 'Test1', IntValue : '12' }}");
            var updateQuery2 = String.Format("<= /Time/2014/09/06/16/33 <= {{ StringValue : 'Test2', IntValue : , BoolValue : true  }}");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript1 = _parser.Parse(updateQuery1).Script;
            var updateScript2 = _parser.Parse(updateQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript1);
            dynamic updateResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript2);
            dynamic updateResult2 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult1);
            Assert.IsNotNull(updateResult2);
            Assert.IsNotNull(selectResult1);
            Assert.AreEqual("Test1", selectResult1.StringValue);
            Assert.AreEqual("12", selectResult1.IntValue);
            Assert.IsFalse(((IInternalNode)selectResult1).GetProperties().ContainsKey("BoolValue"));
            Assert.IsNotNull(selectResult2);
            Assert.AreEqual("Test2", selectResult2.StringValue);
            Assert.IsFalse(((IInternalNode)selectResult2).GetProperties().ContainsKey("IntValue"));
            Assert.AreEqual(true, selectResult2.BoolValue);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Object_To_Path_And_Clear()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time += /2014",
                "/Time/2014 += /09",
                "/Time/2014/09 += /06",
                "/Time/2014/09/06 += /16",
                "<= /Time/2014/09/06/16 += /33"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery1 = String.Format("<= /Time/2014/09/06/16/33 <= {{ StringValue : 'Test1', IntValue : '12' }}");
            var updateQuery2 = String.Format("<= /Time/2014/09/06/16/33 <= {{ StringValue : , IntValue : }}");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript1 = _parser.Parse(updateQuery1).Script;
            var updateScript2 = _parser.Parse(updateQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript1);
            dynamic updateResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript2);
            dynamic updateResult2 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult1);
            Assert.IsNotNull(updateResult2);
            Assert.IsNotNull(selectResult1);
            Assert.AreEqual("Test1", selectResult1.StringValue);
            Assert.AreEqual("12", selectResult1.IntValue);
            Assert.IsFalse(((IInternalNode)selectResult1).GetProperties().ContainsKey("BoolValue"));
            Assert.IsNotNull(selectResult2);
            Assert.AreEqual(0, ((IInternalNode)selectResult2).GetProperties().Count);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Object_To_Path_And_Clear_Incorrect()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Time += /2014",
                "/Time/2014 += /09",
                "/Time/2014/09 += /06",
                "/Time/2014/09/06 += /16",
                "<= /Time/2014/09/06/16 += /33"
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery1 = String.Format("<= /Time/2014/09/06/16/33 <= {{ StringValue : 'Test1', IntValue : '12' }}");
            var updateQuery2 = String.Format("<= /Time/2014/09/06/16/33 <= {{ }}");
            var selectQuery = "/Time/2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript1 = _parser.Parse(updateQuery1).Script;
            var updateScript2 = _parser.Parse(updateQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript1);
            dynamic updateResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript2);
            dynamic updateResult2 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(updateResult1);
            Assert.IsNotNull(updateResult2);
            Assert.IsNotNull(selectResult1);
            Assert.AreEqual("Test1", selectResult1.StringValue);
            Assert.AreEqual("12", selectResult1.IntValue);
            Assert.IsFalse(((IInternalNode)selectResult1).GetProperties().ContainsKey("BoolValue"));
            Assert.IsNotNull(selectResult2);
            Assert.AreEqual(2, ((IInternalNode)selectResult2).GetProperties().Count);
            Assert.AreEqual("Test1", selectResult2.StringValue);
            Assert.AreEqual("12", selectResult2.IntValue);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Should_Update_01()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
            };
            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John";
            var assignQuery1 = "/Contacts/Doe/John <= { ObjectType: 'Family' }";
            var assignQuery2 = "/Contacts/Doe/John <= { ObjectType: 'Person' }";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual("Family", result1.ObjectType);
            Assert.AreEqual("Person", result2.ObjectType);
            Assert.AreNotEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Should_Not_Update_01()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
            };
            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John";
            var assignQuery1 = "/Contacts/Doe/John <= { ObjectType: 'Family' }";
            var assignQuery2 = "/Contacts/Doe/John <= { ObjectType: 'Family' }";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual("Family", result1.ObjectType);
            Assert.AreEqual("Family", result2.ObjectType);
            Assert.AreEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Should_Update_02()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
            };
            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John";
            var assignQuery1 = "/Contacts/Doe/John <= { ObjectType: 'Family', Code: 'ABC' }";
            var assignQuery2 = "/Contacts/Doe/John <= { ObjectType: 'Person', Code: 'ABC' }";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual("Family", result1.ObjectType);
            Assert.AreEqual("Person", result2.ObjectType);
            Assert.AreEqual("ABC", result1.Code);
            Assert.AreEqual("ABC", result2.Code);
            Assert.AreNotEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Should_Update_03()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
            };
            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John";
            var assignQuery1 = "/Contacts/Doe/John <= { ObjectType: 'Family' }";
            var assignQuery2 = "/Contacts/Doe/John <= { ObjectType: 'Person', Code: 'ABC' }";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual("Family", result1.ObjectType);
            Assert.AreEqual("Person", result2.ObjectType);
            Assert.AreEqual("ABC", result2.Code);
            Assert.AreNotEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Should_Not_Update_02()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
            };
            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John";
            var assignQuery1 = "/Contacts/Doe/John <= { ObjectType: 'Family', Code: 'ABC' }";
            var assignQuery2 = "/Contacts/Doe/John <= { ObjectType: 'Family', Code: 'ABC' }";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual("Family", result1.ObjectType);
            Assert.AreEqual("Family", result2.ObjectType);
            Assert.AreEqual("ABC", result1.Code);
            Assert.AreEqual("ABC", result2.Code);
            Assert.AreEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Should_Not_Update_03()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "/Contacts+=Doe/John",
            };
            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Contacts/Doe/John";
            var assignQuery1 = "/Contacts/Doe/John <= { ObjectType: 'Family', Code: 'ABC' }";
            var assignQuery2 = "/Contacts/Doe/John <= { ObjectType: 'Family' }";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual("Family", result1.ObjectType);
            Assert.AreEqual("Family", result2.ObjectType);
            Assert.AreEqual("ABC", result1.Code);
            Assert.AreEqual("ABC", result2.Code);
            Assert.AreEqual(((IInternalNode)result1).Id, ((IInternalNode)result2).Id);
        }

    }
}