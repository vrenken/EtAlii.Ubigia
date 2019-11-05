﻿namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorRootedPathFunctionRenameTests : IClassFixture<LogicalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;
        private readonly IDiagnosticsConfiguration _diagnostics;
        private readonly LogicalUnitTestContext _testContext;

        public ScriptProcessorRootedPathFunctionRenameTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
            _diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(_diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }
        public void Dispose()
        {
            _parser = null;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Rename_Path_01()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            const string query = "Person: += Doe/John\r\n$path <= Person:Doe/John\r\nrename($path, 'Jane')";
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
        public async Task ScriptProcessor_RootedPath_Function_Rename_Path_02()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            const string query = "Person: += Doe/John\r\n$path <= Person:Doe/John\r\nrename(Person:Doe/John, 'Jane')";
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
        public async Task ScriptProcessor_RootedPath_Function_Rename_Path_03()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            const string query = "Person: += Doe/John\r\n$jane <= 'Jane'\r\n$path <= Person:Doe/John\r\nrename(Person:Doe/John, $jane)";
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
        public async Task ScriptProcessor_RootedPath_Function_Rename_Path_04()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            const string query = "Person: += Doe/John\r\n$jane <= 'Jane'\r\n$path <= Person:Doe/John\r\nrename($path, $jane)";
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