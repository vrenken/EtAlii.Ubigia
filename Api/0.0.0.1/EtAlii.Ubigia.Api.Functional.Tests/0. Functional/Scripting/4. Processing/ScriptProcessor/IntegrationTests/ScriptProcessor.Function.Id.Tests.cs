
namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;
    using TestAssembly = EtAlii.Ubigia.Api.Tests.TestAssembly;

    
    public class ScriptProcessor_Function_Id_IntegrationTests : IDisposable
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private static ILogicalTestContext _testContext;

        public ScriptProcessor_Function_Id_IntegrationTests()
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

        public void Dispose()
        {
            var task = Task.Run(async () =>
            {
                _parser = null;

                await _testContext.Stop();
                _testContext = null;
            });
            task.Wait();
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Assign_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id('First') <= /Time";
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);


            // Assert.
            await ExceptionAssert.ThrowsObservable<ScriptProcessingException, SequenceProcessingResult>(act);
            //Assert.Equal(1, parseResult.Errors.Length);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Assign_Invalid_Faulty_Arguments()
        {
            // Arrange.
            const string text = "id('First', 'Second') <= /Time";
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ExceptionAssert.ThrowsObservable<ScriptProcessingException, SequenceProcessingResult>(act);
            //Assert.Equal(1, parseResult.Errors.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Variable_Invalid_Faulty_Arguments()
        {
            // Arrange.
            const string text = "id($path, 'First', 'Second')";
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ExceptionAssert.ThrowsObservable<ScriptProcessingException, SequenceProcessingResult>(act);
            //Assert.Equal(1, parseResult.Errors.Length);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Variable_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id($path, 'First')";
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ExceptionAssert.ThrowsObservable<ScriptProcessingException, SequenceProcessingResult>(act);
            //Assert.Equal(1, parseResult.Errors.Length);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Constant_DoubleQuoted_Invalid_Faulty_Arguments()
        {
            // Arrange.
            const string text = "id(\"/Hierarchy\", 'First', 'Second')";
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ExceptionAssert.ThrowsObservable<ScriptProcessingException, SequenceProcessingResult>(act);
            //Assert.Equal(1, parseResult.Errors.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Constant_DoubleQuoted_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id(\"/Hierarchy\", 'First')";
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ExceptionAssert.ThrowsObservable<ScriptProcessingException, SequenceProcessingResult>(act);
            //Assert.Equal(1, parseResult.Errors.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Constant_SingleQuoted_Invalid_Faulty_Arguments()
        {
            // Arrange.
            const string text = "id('/Hierarchy', 'First', 'Second')";
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ExceptionAssert.ThrowsObservable<ScriptProcessingException, SequenceProcessingResult>(act);
            //Assert.Equal(1, parseResult.Errors.Length);
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Function_Id_Constant_SingleQuoted_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id('/Hierarchy', 'First')";
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);
            var parseResult = _parser.Parse(text);

            // Act.
            var act = processor.Process(parseResult.Script);

            // Assert.
            await ExceptionAssert.ThrowsObservable<ScriptProcessingException, SequenceProcessingResult>(act);
            //Assert.Equal(1, parseResult.Errors.Length);
        }

    }
}