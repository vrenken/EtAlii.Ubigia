// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Tests;
    using Microsoft.CSharp.RuntimeBinder;
    using Xunit;

    [CorrelateUnitTests]
    public sealed class ScriptProcessorNonRootedPathQueryTemporalIntegrationTests : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly FunctionalUnitTestContext _testContext;

        public ScriptProcessorNonRootedPathQueryTemporalIntegrationTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = testContext.CreateScriptParser();
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Temporal_Downdate_01()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person/Doe/John <= { NickName: 'Joe' }",
                "/Person/Doe/John <= { NickName: 'John' }",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John{";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic person = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(person);
            Assert.Equal("Joe", person.NickName);
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Temporal_Downdate_02()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person/Doe/John <= { NickName: 'Joe' }",
                "/Person/Doe/John <= { NickName: 'Johnny' }",
                "/Person/Doe/John <= { NickName: 'John' }",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John{{";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
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

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Temporal_Downdate_03()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person/Doe/John <= { NickName: 'Joe' }",
                "/Person/Doe/John <= { NickName: 'Johnny' }",
                "/Person/Doe/John <= { NickName: 'John' }",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John{";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic person = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(person);
            Assert.Equal("Johnny", person.NickName);
        }
    }
}
