namespace EtAlii.Servus.Api.Functional.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ScriptProcessor_Assign_String_UnitTests
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;

        [TestInitialize]
        public void Initialize()
        {
            _diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(_diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parser = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_String_To_Variable()
        {
            // Arrange.
            var script = _parser.Parse("$var1 <= \"Time\"").Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            await lastSequence.Output.ToArray();
            // Assert.
            Assert.AreEqual("Time", await scope.Variables["var1"].Value.SingleAsync());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_String_To_Variable_Via_Variable()
        {
            // Arrange.
            var script = _parser.Parse("$var1 <= \"Time\"\r\n$var2 <= $var1").Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            await lastSequence.Output.ToArray();

            // Assert.
            Assert.AreEqual("Time", await scope.Variables["var2"].Value.SingleAsync());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_String_To_Variable_Via_Variable_With_Replace()
        {
            // Arrange.
            var script = _parser.Parse("$var1 <= \"Time\"\r\n$var2 <= $var1\r\n$var1 <= \"Location\"").Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            await lastSequence.Output.ToArray();

            // Assert.
            Assert.AreEqual("Time", await scope.Variables["var2"].Value.SingleAsync());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_String_To_Variable_Via_Variable_With_Clear()
        {
            // Arrange.
            var script = _parser.Parse("$var1 <= \"Time\"\r\n$var2 <= $var1\r\n$var1 <=").Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            await lastSequence.Output.LastOrDefaultAsync();

            // Assert.
            Assert.AreEqual("Time", await scope.Variables["var2"].Value.SingleAsync());
            Assert.IsFalse(scope.Variables.ContainsKey("var1"));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_String_To_Variable_Via_Variable_With_Empty_String()
        {
            // Arrange.
            var script = _parser.Parse("$var1 <= \"Time\"\r\n$var2 <= $var1\r\n$var1 <= \"\"").Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            await lastSequence.Output.ToArray();

            // Assert.
            Assert.AreEqual("Time", await scope.Variables["var2"].Value.SingleAsync());
            Assert.AreEqual("", await scope.Variables["var1"].Value.SingleAsync());
        }

    }
}