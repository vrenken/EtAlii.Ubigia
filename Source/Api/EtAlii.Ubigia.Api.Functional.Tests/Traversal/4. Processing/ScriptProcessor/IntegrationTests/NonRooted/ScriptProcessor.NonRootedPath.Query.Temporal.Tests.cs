// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.CSharp.RuntimeBinder;
    using Xunit;

    public class ScriptProcessorNonRootedPathQueryTemporalIntegrationTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly IDiagnosticsConfiguration _diagnostics;
        private readonly TraversalUnitTestContext _testContext;

        public ScriptProcessorNonRootedPathQueryTemporalIntegrationTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _diagnostics = DiagnosticsConfiguration.Default;
            _parser = new TestScriptParserFactory().Create();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Temporal_Downdate_01()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person/Doe/John <= { NickName: 'Joe' }",
                "/Person/Doe/John <= { NickName: 'John' }",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John{";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic person = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(person);
            Assert.Equal("Joe", person.NickName);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Temporal_Downdate_02()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person/Doe/John <= { NickName: 'Joe' }",
                "/Person/Doe/John <= { NickName: 'Johnny' }",
                "/Person/Doe/John <= { NickName: 'John' }",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John{{";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var history = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(history);
            Assert.Equal(4, history.Length);
            dynamic personHistory = history[0];
            Assert.Equal("John", personHistory.NickName);
            personHistory = history[1];
            Assert.Equal("Johnny", personHistory.NickName);
            personHistory = history[2];
            Assert.Equal("Joe", personHistory.NickName);
            personHistory = history[3];
            Assert.Throws<RuntimeBinderException>(() => personHistory.NickName); // The first entry does not have a NickName assigned.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Temporal_Downdate_03()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person/Doe/John <= { NickName: 'Joe' }",
                "/Person/Doe/John <= { NickName: 'Johnny' }",
                "/Person/Doe/John <= { NickName: 'John' }",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John{";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var processor = new TestScriptProcessorFactory().Create(logicalContext, _diagnostics);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic person = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(person);
            Assert.Equal("Johnny", person.NickName);
        }
    }
}
