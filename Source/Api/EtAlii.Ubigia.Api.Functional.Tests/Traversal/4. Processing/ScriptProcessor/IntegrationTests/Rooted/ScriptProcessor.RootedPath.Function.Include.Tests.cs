// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorRootedPathFunctionIncludeTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly IDiagnosticsConfiguration _diagnostics;
        private readonly TraversalUnitTestContext _testContext;

        public ScriptProcessorRootedPathFunctionIncludeTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _diagnostics = DiagnosticsConfiguration.Default;
            _parser = new TestScriptParserFactory().Create();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Include_Path_01()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            const string query = "<= include(\\02) <= time:\"2017-02-20 20:06:02.123\"";
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

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
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var query = new[]
            {
                "time:\"2017-02-20 20:06:01.122\"",
                "<= include(\\02\\06/*) <= time:\"2017-02-20 20:06:02.123\""
            };
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

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
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var query = new[]
            {
                "time:\"2017-02-20 20:06:02.122\"",
                "<= include(\\02/*) <= time:\"2017-02-20 20:06:02.123\""
            };
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

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
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var query = new[]
            {
                "time:\"2017-02-20 20:06:01.122\"",
                "<= include(\\02\\*/*) <= time:\"2017-02-20 20:06:02.123\""
            };
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

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
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var query = new[]
            {
                "time:\"2017-02-20 20:06:01.122\"",
                "<= include(\\*\\*/*) <= time:\"2017-02-20 20:06:02.123\""
            };
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

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
