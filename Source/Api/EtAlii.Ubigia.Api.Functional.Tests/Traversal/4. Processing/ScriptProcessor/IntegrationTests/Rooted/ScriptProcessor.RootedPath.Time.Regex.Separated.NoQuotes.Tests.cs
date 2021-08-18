// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;

    public class ScriptProcessorRootedPathTimeRegexSeparatedNoQuotesTests : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly FunctionalUnitTestContext _testContext;

        public ScriptProcessorRootedPathTimeRegexSeparatedNoQuotesTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = testContext.CreateScriptParser();
        }

        [Fact]
        public async Task ScriptProcessor_RootedPath_Time_Select_YYYY_Regex_Separated_NoQuotes()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "time:2016",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/01/01/00/00/00/000";
            var selectQuery2 = "time:2016";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript1 = _parser.Parse(selectQuery1, scope).Script;
            var selectScript2 = _parser.Parse(selectQuery2, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalContext);

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
    }
}
