// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using Xunit;

    public class ScriptProcessorNonRootedPathFunctionInclude : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly FunctionalUnitTestContext _testContext;

        public ScriptProcessorNonRootedPathFunctionInclude(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = testContext.CreateScriptParser();
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Function_Include_Path_01()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            const string query = "<= include(\\02) <= /time/\"2017-02-20 20:06:02.123\"";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(script, scope);
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


        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Function_Include_Path_02()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var query = new[]
            {
                "/time/\"2017-02-20 20:06:01.122\"",
                "<= include(\\02\\06/*) <= /time/\"2017-02-20 20:06:02.123\""
            };
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(script, scope);
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

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Function_Include_Path_03()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var query = new[]
            {
                "/time/\"2017-02-20 20:06:02.122\"",
                "<= include(\\02/*) <= /time/\"2017-02-20 20:06:02.123\""
            };
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(script, scope);
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

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Function_Include_Path_04()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var query = new[]
            {
                "/time/\"2017-02-20 20:06:01.122\"",
                "<= include(\\02\\*/*) <= /time/\"2017-02-20 20:06:02.123\""
            };
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(script, scope);
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

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Function_Include_Path_05()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var query = new[]
            {
                "/time/\"2017-02-20 20:06:01.122\"",
                "<= include(\\*\\*/*) <= /time/\"2017-02-20 20:06:02.123\""
            };
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(script, scope);
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
