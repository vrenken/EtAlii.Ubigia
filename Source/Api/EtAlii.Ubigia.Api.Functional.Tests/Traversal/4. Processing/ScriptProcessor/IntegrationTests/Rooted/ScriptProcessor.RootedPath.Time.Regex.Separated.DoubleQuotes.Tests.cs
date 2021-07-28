// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;

    public class ScriptProcessorRootedPathTimeRegexSeparatedDoubleQuotesTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly TraversalUnitTestContext _testContext;

        public ScriptProcessorRootedPathTimeRegexSeparatedDoubleQuotesTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = new TestScriptParserFactory().Create();
        }

        [Fact]
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYY_Regex_Separated_DoubleQuotes()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "time:\"2016\"",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/01/01/00/00/00/000";
            var selectQuery2 = "time:\"2016\"";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript1 = _parser.Parse(selectQuery1).Script;
            var selectScript2 = _parser.Parse(selectQuery2).Script;

            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            var addResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1);
            var firstResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2);
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
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMM_Regex_Separated_DoubleQuotes()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);

            var addQueries = new[]
            {
                "time:\"2016-09\"",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/00/00/00/000";
            var selectQuery2 = "time:\"2016-09\"";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript1 = _parser.Parse(selectQuery1).Script;
            var selectScript2 = _parser.Parse(selectQuery2).Script;

            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            var addResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1);
            var firstResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2);
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
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMMDD_Regex_Separated_DoubleQuotes()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);

            var addQueries = new[]
            {
                "time:\"2016-09-01\"",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/00/00/00/000";
            var selectQuery2 = "time:\"2016-09-01\"";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript1 = _parser.Parse(selectQuery1).Script;
            var selectScript2 = _parser.Parse(selectQuery2).Script;

            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            var addResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1);
            var firstResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2);
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
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMMDDHH_Regex_Separated_DoubleQuotes()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical
                .CreateLogicalContext(true)
                .ConfigureAwait(false);

            var addQueries = new[]
            {
                "time:\"2016-09-01 22\"",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/22/00/00/000";
            var selectQuery2 = "time:\"2016-09-01 22\"";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript1 = _parser.Parse(selectQuery1).Script;
            var selectScript2 = _parser.Parse(selectQuery2).Script;

            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            var addResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1);
            var firstResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2);
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
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMMDDHHMM_Regex_Separated_DoubleQuotes()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);

            var addQueries = new[]
            {
                "time:\"2019-09-01 22:05\"",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2019/09/01/22/05/00/000";
            var selectQuery2 = "time:\"2019-09-01 22:05\"";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript1 = _parser.Parse(selectQuery1).Script;
            var selectScript2 = _parser.Parse(selectQuery2).Script;

            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            var addResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1);
            var firstResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2);
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
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMMDDHHMMSS_Regex_Separated_DoubleQuotes()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);

            var addQueries = new[]
            {
                "time:\"2016-09-01 22:05:23\"",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/22/05/23/000";
            var selectQuery2 = "time:\"2016-09-01 22:05:23\"";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript1 = _parser.Parse(selectQuery1).Script;
            var selectScript2 = _parser.Parse(selectQuery2).Script;

            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            var addResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1);
            var firstResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2);
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
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYYMMDDHHMMSSMMM_Regex_Separated_DoubleQuotes()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);

            var addQueries = new[]
            {
                "time:\"2016-09-01 22:05:23.123\"",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/22/05/23/123";
            var selectQuery2 = "time:\"2016-09-01 22:05:23.123\"";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript1 = _parser.Parse(selectQuery1).Script;
            var selectScript2 = _parser.Parse(selectQuery2).Script;

            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            var addResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1);
            var firstResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2);
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
