﻿namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorRootedPathFunctionIncludeTests : IClassFixture<LogicalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;
        private readonly IDiagnosticsConfiguration _diagnostics;
        private readonly LogicalUnitTestContext _testContext;

        public ScriptProcessorRootedPathFunctionIncludeTests(LogicalUnitTestContext testContext)
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
        public async Task ScriptProcessor_RootedPath_Function_Include_Path_01()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            const string query = "<= include(\\02) <= time:\"2017-02-20 20:06:02.123\"";
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
            Assert.Equal(2, result.Length);
            Assert.IsType<object[]>(result);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[0]);
            Assert.Equal("123", result.Cast<IReadOnlyEntry>().Skip(0).First().Type);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[1]);
            Assert.Equal("02", result.Cast<IReadOnlyEntry>().Skip(1).First().Type);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Include_Path_02()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var query = new[]
            {
                "time:\"2017-02-20 20:06:01.122\"",
                "<= include(\\02\\06/*) <= time:\"2017-02-20 20:06:02.123\""
            };
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
            Assert.Equal(3, result.Length);
            Assert.IsType<object[]>(result);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[0]);
            Assert.Equal("123", result.Cast<IReadOnlyEntry>().Skip(0).First().Type);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[1]);
            Assert.Equal("01", result.Cast<IReadOnlyEntry>().Skip(1).First().Type);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[2]);
            Assert.Equal("02", result.Cast<IReadOnlyEntry>().Skip(2).First().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Include_Path_03()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var query = new[]
            {
                "time:\"2017-02-20 20:06:02.122\"",
                "<= include(\\02/*) <= time:\"2017-02-20 20:06:02.123\""
            };
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
            Assert.Equal(3, result.Length);
            Assert.IsType<object[]>(result);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[0]);
            Assert.Equal("123", result.Cast<IReadOnlyEntry>().Skip(0).First().Type);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[1]);
            Assert.Equal("122", result.Cast<IReadOnlyEntry>().Skip(1).First().Type);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[2]);
            Assert.Equal("123", result.Cast<IReadOnlyEntry>().Skip(2).First().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Include_Path_04()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var query = new[]
            {
                "time:\"2017-02-20 20:06:01.122\"",
                "<= include(\\02\\*/*) <= time:\"2017-02-20 20:06:02.123\""
            };
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
            Assert.Equal(3, result.Length);
            Assert.IsType<object[]>(result);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[0]);
            Assert.Equal("123", result.Cast<IReadOnlyEntry>().Skip(0).First().Type);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[1]);
            Assert.Equal("01", result.Cast<IReadOnlyEntry>().Skip(1).First().Type);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[2]);
            Assert.Equal("02", result.Cast<IReadOnlyEntry>().Skip(2).First().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Include_Path_05()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var query = new[]
            {
                "time:\"2017-02-20 20:06:01.122\"",
                "<= include(\\*\\*/*) <= time:\"2017-02-20 20:06:02.123\""
            };
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
            Assert.Equal(3, result.Length);
            Assert.IsType<object[]>(result);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[0]);
            Assert.Equal("123", result.Cast<IReadOnlyEntry>().Skip(0).First().Type);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[1]);
            Assert.Equal("01", result.Cast<IReadOnlyEntry>().Skip(1).First().Type);
            Assert.IsAssignableFrom<IReadOnlyEntry>(result[2]);
            Assert.Equal("02", result.Cast<IReadOnlyEntry>().Skip(2).First().Type);
        }
    }
}