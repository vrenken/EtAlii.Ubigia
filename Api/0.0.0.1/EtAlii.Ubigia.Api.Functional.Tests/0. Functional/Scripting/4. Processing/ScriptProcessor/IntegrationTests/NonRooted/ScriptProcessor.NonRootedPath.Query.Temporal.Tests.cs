namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics.Tests;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;
    using TestAssembly = EtAlii.Ubigia.Api.Tests.TestAssembly;

    
    public class ScriptProcessorNonRootedPathQueryTemporalIntegrationTests : IClassFixture<LogicalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private readonly LogicalUnitTestContext _testContext;

        public ScriptProcessorNonRootedPathQueryTemporalIntegrationTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
            var task = Task.Run(async () =>
            {
                _diagnostics = TestDiagnostics.Create();
                var scriptParserConfiguration = new ScriptParserConfiguration()
                    .Use(_diagnostics);
                _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
                _logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
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
        public async Task ScriptProcessor_NonRootedPath_Temporal_Downdate_01()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person/Doe/John <= { Nickname: 'Joe' }",
                "/Person/Doe/John <= { Nickname: 'John' }",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John{";

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
            dynamic person = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(person);
            Assert.Equal("Joe", person.Nickname);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Temporal_Downdate_02()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person/Doe/John <= { Nickname: 'Joe' }",
                "/Person/Doe/John <= { Nickname: 'Johnny' }",
                "/Person/Doe/John <= { Nickname: 'John' }",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John{{";

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
            dynamic person = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(person);
            Assert.Equal("Joe", person.Nickname);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Temporal_Downdate_03()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person/Doe/John <= { Nickname: 'Joe' }",
                "/Person/Doe/John <= { Nickname: 'Johnny' }",
                "/Person/Doe/John <= { Nickname: 'John' }",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John{";

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
            dynamic person = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(person);
            Assert.Equal("Johnny", person.Nickname);
        }
    }
}