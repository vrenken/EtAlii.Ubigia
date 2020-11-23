﻿namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorNonRootedPathFunctionRename : IClassFixture<ScriptingUnitTestContext>, IAsyncLifetime
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private readonly ScriptingUnitTestContext _testContext;

        public ScriptProcessorNonRootedPathFunctionRename(ScriptingUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public Task InitializeAsync()
        {
            _diagnostics = DiagnosticsConfiguration.Default;
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(_diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            _parser = null;
            return Task.CompletedTask;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Function_Rename_Path_01()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            const string query = "/Person += Doe/John\r\n$path <= /Person/Doe/John\r\nrename($path, 'Jane')";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.IsType<object[]>(result);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[0]);
            Assert.Equal("Jane", result.Cast<IReadOnlyEntry>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Function_Rename_Path_02()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            const string query = "/Person += Doe/John\r\n$path <= /Person/Doe/John\r\nrename(/Person/Doe/John, 'Jane')";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.IsType<object[]>(result);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[0]);
            Assert.Equal("Jane", result.Cast<IReadOnlyEntry>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Function_Rename_Path_03()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            const string query = "/Person += Doe/John\r\n$jane <= 'Jane'\r\n$path <= /Person/Doe/John\r\nrename(/Person/Doe/John, $jane)";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.IsType<object[]>(result);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[0]);
            Assert.Equal("Jane", result.Cast<IReadOnlyEntry>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Function_Rename_Path_04()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            const string query = "/Person += Doe/John\r\n$jane <= 'Jane'\r\n$path <= /Person/Doe/John\r\nrename($path, $jane)";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.IsType<object[]>(result);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[0]);
            Assert.Equal("Jane", result.Cast<IReadOnlyEntry>().Single().Type);
        }

    }
}