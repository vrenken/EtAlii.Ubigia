namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorFunctionIdIntegrationTests : IAsyncLifetime
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalTestContext _testContext;

        public async Task InitializeAsync()
        {
            _testContext = new LogicalTestContextFactory().Create();
            await _testContext.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);

            _diagnostics = DiagnosticsConfiguration.Default;
            _parser = new TestScriptParserFactory().Create();
        }

        public async Task DisposeAsync()
        {
            _parser = null;

            await _testContext.Stop().ConfigureAwait(false);
            _testContext = null;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Assign_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id('First') <= /Time";
            var logicalContext = await _testContext.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);


            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Assign_Invalid_Faulty_Arguments()
        {
            // Arrange.
            const string text = "id('First', 'Second') <= /Time";
            var logicalContext = await _testContext.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Variable_Invalid_Faulty_Arguments()
        {
            // Arrange.
            const string text = "id($path, 'First', 'Second')";
            var logicalContext = await _testContext.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Variable_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id($path, 'First')";
            var logicalContext = await _testContext.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Constant_DoubleQuoted_Invalid_Faulty_Arguments()
        {
            // Arrange.
            const string text = "id(\"/Hierarchy\", 'First', 'Second')";
            var logicalContext = await _testContext.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Constant_DoubleQuoted_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id(\"/Hierarchy\", 'First')";
            var logicalContext = await _testContext.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Constant_SingleQuoted_Invalid_Faulty_Arguments()
        {
            // Arrange.
            const string text = "id('/Hierarchy', 'First', 'Second')";
            var logicalContext = await _testContext.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Constant_SingleQuoted_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id('/Hierarchy', 'First')";
            var logicalContext = await _testContext.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }

    }
}
