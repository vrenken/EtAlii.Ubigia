// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    public class ScriptProcessorFunctionIdIntegrationTests : IAsyncLifetime
    {
        private IScriptParser _parser;
        private FunctionalUnitTestContext _testContext;

        public async Task InitializeAsync()
        {
            _testContext = new FunctionalUnitTestContext();
            await _testContext
                .InitializeAsync()
                .ConfigureAwait(false);

            _parser = _testContext.CreateScriptParser();
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
            var scope = new ExecutionScope();
            const string text = "id('First') <= /Time";
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            var parseResult = _parser.Parse(text, scope);

            // Act.
            var act = processor.Process(parseResult.Script, scope);


            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Assign_Invalid_Faulty_Arguments()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "id('First', 'Second') <= /Time";
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            var parseResult = _parser.Parse(text, scope);

            // Act.
            var act = processor.Process(parseResult.Script, scope);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Variable_Invalid_Faulty_Arguments()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "id($path, 'First', 'Second')";
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            var parseResult = _parser.Parse(text, scope);

            // Act.
            var act = processor.Process(parseResult.Script, scope);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Variable_Invalid_Faulty_Argument()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "id($path, 'First')";
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            var parseResult = _parser.Parse(text, scope);

            // Act.
            var act = processor.Process(parseResult.Script, scope);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Constant_DoubleQuoted_Invalid_Faulty_Arguments()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "id(\"/Hierarchy\", 'First', 'Second')";
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            var parseResult = _parser.Parse(text, scope);

            // Act.
            var act = processor.Process(parseResult.Script, scope);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Constant_DoubleQuoted_Invalid_Faulty_Argument()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "id(\"/Hierarchy\", 'First')";
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            var parseResult = _parser.Parse(text, scope);

            // Act.
            var act = processor.Process(parseResult.Script, scope);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Constant_SingleQuoted_Invalid_Faulty_Arguments()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "id('/Hierarchy', 'First', 'Second')";
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            var parseResult = _parser.Parse(text, scope);

            // Act.
            var act = processor.Process(parseResult.Script, scope);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Constant_SingleQuoted_Invalid_Faulty_Argument()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "id('/Hierarchy', 'First')";
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            var parseResult = _parser.Parse(text, scope);

            // Act.
            var act = processor.Process(parseResult.Script, scope);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
            //Assert.Equal(1, parseResult.Errors.Length)
        }

    }
}
