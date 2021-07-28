// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    public class ScriptProcessorFunctionIdIntegrationTests : IAsyncLifetime
    {
        private IScriptParser _parser;
        private TraversalUnitTestContext _testContext;

        public async Task InitializeAsync()
        {
            _testContext = new TraversalUnitTestContext();
            await _testContext
                .InitializeAsync()
                .ConfigureAwait(false);

            _parser = new TestScriptParserFactory().Create(_testContext.ClientConfiguration);
        }

        public async Task DisposeAsync()
        {
            await _testContext
                .DisposeAsync()
                .ConfigureAwait(false);
            _testContext = null;
            _parser = null;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Assign_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id('First') <= /Time";
            using var logicalContext = await _testContext.Logical
                .CreateLogicalContext(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);
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
            using var logicalContext = await _testContext.Logical
                .CreateLogicalContext(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);
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
            using var logicalContext = await _testContext.Logical
                .CreateLogicalContext(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);
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
            using var logicalContext = await _testContext.Logical
                .CreateLogicalContext(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);
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
            using var logicalContext = await _testContext.Logical
                .CreateLogicalContext(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);
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
            using var logicalContext = await _testContext.Logical
                .CreateLogicalContext(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);
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
            using var logicalContext = await _testContext.Logical
                .CreateLogicalContext(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);
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
            using var logicalContext = await _testContext.Logical
                .CreateLogicalContext(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }

    }
}
