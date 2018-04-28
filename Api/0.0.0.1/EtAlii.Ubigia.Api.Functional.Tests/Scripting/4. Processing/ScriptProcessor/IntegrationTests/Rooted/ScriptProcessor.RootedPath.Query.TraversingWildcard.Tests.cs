namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;
    

    
    public class ScriptProcessorRootedPathQueryTraversingWildcardIntegrationTests : IClassFixture<LogicalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;

        public ScriptProcessorRootedPathQueryTraversingWildcardIntegrationTests(LogicalUnitTestContext testContext)
        {
            var task = Task.Run(async () =>
            {
                _diagnostics = TestDiagnostics.Create();
                var scriptParserConfiguration = new ScriptParserConfiguration()
                    .Use(_diagnostics);
                _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
                _logicalContext = await testContext.LogicalTestContext.CreateLogicalContext(true);
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(() =>
            {
                _parser = null;
                _logicalContext.Dispose();
                _logicalContext = null;
            });
            task.Wait();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_TraversingWildcard_01()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Joe",
                "Person:+=Doe/Johnny",
                "Person:+=Dane/John",
                "Person:+=Dane/Jane",
                "Person:+=Dane/Joe",
                "Person:+=Dane/Johnny",
                "Person:+=Dee/The/John",
                "Person:+=Dee/The/Jane",
                "Person:+=Dee/The/Joe",
                "Person:+=Dee/The/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "Person:*3*/John";

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
            Assert.NotNull(result);
            Assert.Equal(3, result.Length);
            dynamic person1 = result.Skip(0).First();
            dynamic person2 = result.Skip(1).First();
            dynamic person3 = result.Skip(2).First();
            Assert.Equal("John", person1.ToString());
            Assert.Equal("John", person2.ToString());
            Assert.Equal("John", person3.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_TraversingWildcard_02()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Joe",
                "Person:+=Doe/Johnny",
                "Person:+=Dane/John",
                "Person:+=Dane/Jane",
                "Person:+=Dane/Joe",
                "Person:+=Dane/Johnny",
                "Person:+=Dee/The/John",
                "Person:+=Dee/The/Jane",
                "Person:+=Dee/The/Joe",
                "Person:+=Dee/The/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "Person:*2*/Jo*";

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
            Assert.NotNull(result);
            Assert.Equal(9, result.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_TraversingWildcard_03()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Joe",
                "Person:+=Doe/Johnny",
                "Person:+=Dane/John",
                "Person:+=Dane/Jane",
                "Person:+=Dane/Joe",
                "Person:+=Dane/Johnny",
                "Person:+=Dee/The/John",
                "Person:+=Dee/The/Jane",
                "Person:+=Dee/The/Joe",
                "Person:+=Dee/The/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "Person:*2*";

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
            Assert.NotNull(result);
            Assert.Equal(12, result.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_TraversingWildcard_04()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Joe",
                "Person:+=Doe/Johnny",
                "Person:+=Dane/John",
                "Person:+=Dane/Jane",
                "Person:+=Dane/Joe",
                "Person:+=Dane/Johnny",
                "Person:+=Dee/The/John",
                "Person:+=Dee/The/Jane",
                "Person:+=Dee/The/Joe",
                "Person:+=Dee/The/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "Person:*3*";

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
            Assert.NotNull(result);
            Assert.Equal(16, result.Length);
        }
    }
}