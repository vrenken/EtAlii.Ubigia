﻿namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Diagnostics;
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
            await _testContext.Start(UnitTestSettings.NetworkPortRange);

            _diagnostics = UbigiaDiagnostics.DefaultConfiguration;
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(_diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        public async Task DisposeAsync()
        {
            _parser = null;

            await _testContext.Stop();
            _testContext = null;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Assign_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id('First') <= /Time";
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);


            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act);
            //Assert.Equal(1, parseResult.Errors.Length)
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Assign_Invalid_Faulty_Arguments()
        {
            // Arrange.
            const string text = "id('First', 'Second') <= /Time";
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act);
            //Assert.Equal(1, parseResult.Errors.Length)
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Variable_Invalid_Faulty_Arguments()
        {
            // Arrange.
            const string text = "id($path, 'First', 'Second')";
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act);
            //Assert.Equal(1, parseResult.Errors.Length)
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Variable_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id($path, 'First')";
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act);
            //Assert.Equal(1, parseResult.Errors.Length)
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Constant_DoubleQuoted_Invalid_Faulty_Arguments()
        {
            // Arrange.
            const string text = "id(\"/Hierarchy\", 'First', 'Second')";
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act);
            //Assert.Equal(1, parseResult.Errors.Length)
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Constant_DoubleQuoted_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id(\"/Hierarchy\", 'First')";
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act);
            //Assert.Equal(1, parseResult.Errors.Length)
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Constant_SingleQuoted_Invalid_Faulty_Arguments()
        {
            // Arrange.
            const string text = "id('/Hierarchy', 'First', 'Second')";
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act);
            //Assert.Equal(1, parseResult.Errors.Length)
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Constant_SingleQuoted_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id('/Hierarchy', 'First')";
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act);
            //Assert.Equal(1, parseResult.Errors.Length)
        }

    }
}