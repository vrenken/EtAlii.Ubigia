// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public sealed class ScriptProcessorRootedPathTimeRegexSeparatedSingleQuotesTests : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly FunctionalUnitTestContext _testContext;

        public ScriptProcessorRootedPathTimeRegexSeparatedSingleQuotesTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = testContext.CreateScriptParser();
        }

        [Fact]
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYY_Regex_Separated_SingleQuotes()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "time:'2016'",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/01/01/00/00/00/000";
            var selectQuery2 = "time:'2016'";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript1 = _parser.Parse(selectQuery1, scope).Script;
            var selectScript2 = _parser.Parse(selectQuery2, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            var addResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1, scope);
            var firstResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2, scope);
            var secondResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(addResult.Id, firstResult.Id);
            Assert.Equal(addResult.Id, secondResult.Id);
            Assert.Equal("000", secondResult.Type);
        }

        [Fact]
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMM_Regex_Separated_SingleQuotes()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            var addQueries = new[]
            {
                "time:'2016-09'",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/00/00/00/000";
            var selectQuery2 = "time:'2016-09'";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript1 = _parser.Parse(selectQuery1, scope).Script;
            var selectScript2 = _parser.Parse(selectQuery2, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            var addResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1, scope);
            var firstResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2, scope);
            var secondResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(addResult.Id, firstResult.Id);
            Assert.Equal(addResult.Id, secondResult.Id);
            Assert.Equal("000", secondResult.Type);
        }

        [Fact]
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMMDD_Regex_Separated_SingleQuotes()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            var addQueries = new[]
            {
                "time:'2016-09-01'",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/00/00/00/000";
            var selectQuery2 = "time:'2016-09-01'";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript1 = _parser.Parse(selectQuery1, scope).Script;
            var selectScript2 = _parser.Parse(selectQuery2, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            var addResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1, scope);
            var firstResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2, scope);
            var secondResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(addResult.Id, firstResult.Id);
            Assert.Equal(addResult.Id, secondResult.Id);
            Assert.Equal("000", secondResult.Type);
        }

        [Fact]
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMMDDHH_Regex_Separated_SingleQuotes()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            var addQueries = new[]
            {
                "time:'2016-09-01 22'",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/22/00/00/000";
            var selectQuery2 = "time:'2016-09-01 22'";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript1 = _parser.Parse(selectQuery1, scope).Script;
            var selectScript2 = _parser.Parse(selectQuery2, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            var addResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1, scope);
            var firstResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2, scope);
            var secondResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(addResult.Id, firstResult.Id);
            Assert.Equal(addResult.Id, secondResult.Id);
            Assert.Equal("000", secondResult.Type);
        }

        [Fact]
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMMDDHHMM_Regex_Separated_SingleQuotes()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            var addQueries = new[]
            {
                "time:'2020-09-01 22:05'",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2020/09/01/22/05/00/000";
            var selectQuery2 = "time:'2020-09-01 22:05'";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript1 = _parser.Parse(selectQuery1, scope).Script;
            var selectScript2 = _parser.Parse(selectQuery2, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            var addResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1, scope);
            var firstResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2, scope);
            var secondResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(addResult.Id, firstResult.Id);
            Assert.Equal(addResult.Id, secondResult.Id);
            Assert.Equal("000", secondResult.Type);
        }

        [Fact]
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMMDDHHMMSS_Regex_Separated_SingleQuotes()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            var addQueries = new[]
            {
                "time:'2016-09-01 22:05:23'",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/22/05/23/000";
            var selectQuery2 = "time:'2016-09-01 22:05:23'";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript1 = _parser.Parse(selectQuery1, scope).Script;
            var selectScript2 = _parser.Parse(selectQuery2, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            var addResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1, scope);
            var firstResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2, scope);
            var secondResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(addResult.Id, firstResult.Id);
            Assert.Equal(addResult.Id, secondResult.Id);
            Assert.Equal("000", secondResult.Type);
        }

        [Fact]
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMMDDHHMMSSMMM_Regex_Separated_SingleQuotes()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            var addQueries = new[]
            {
                "time:'2016-09-01 22:05:23.123'",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/22/05/23/123";
            var selectQuery2 = "time:'2016-09-01 22:05:23.123'";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript1 = _parser.Parse(selectQuery1, scope).Script;
            var selectScript2 = _parser.Parse(selectQuery2, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            var addResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1, scope);
            var firstResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2, scope);
            var secondResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(addResult.Id, firstResult.Id);
            Assert.Equal(addResult.Id, secondResult.Id);
            Assert.Equal("123", secondResult.Type);
        }

    }
}
