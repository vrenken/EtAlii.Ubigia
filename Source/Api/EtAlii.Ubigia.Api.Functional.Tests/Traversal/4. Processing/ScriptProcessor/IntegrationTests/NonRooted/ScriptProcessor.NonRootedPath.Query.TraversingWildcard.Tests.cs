// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public sealed class ScriptProcessorNonRootedPathQueryTraversingWildcardIntegrationTests : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly FunctionalUnitTestContext _testContext;

        public ScriptProcessorNonRootedPathQueryTraversingWildcardIntegrationTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = testContext.CreateScriptParser();
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_TraversingWildcard_01()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person+=Doe/Jane",
                "/Person+=Doe/Joe",
                "/Person+=Doe/Johnny",
                "/Person+=Dane/John",
                "/Person+=Dane/Jane",
                "/Person+=Dane/Joe",
                "/Person+=Dane/Johnny",
                "/Person+=Dee/The/John",
                "/Person+=Dee/The/Jane",
                "/Person+=Dee/The/Joe",
                "/Person+=Dee/The/Johnny",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/*3*/John";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(3, result.Length);
            dynamic person1 = result.Skip(0).First();
            dynamic person2 = result.Skip(1).First();
            dynamic person3 = result.Skip(2).First();
            Assert.Equal("John", person1.ToString());
            Assert.Equal("John", person2.ToString());
            Assert.Equal("John", person3.ToString());
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_TraversingWildcard_02()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person+=Doe/Jane",
                "/Person+=Doe/Joe",
                "/Person+=Doe/Johnny",
                "/Person+=Dane/John",
                "/Person+=Dane/Jane",
                "/Person+=Dane/Joe",
                "/Person+=Dane/Johnny",
                "/Person+=Dee/The/John",
                "/Person+=Dee/The/Jane",
                "/Person+=Dee/The/Joe",
                "/Person+=Dee/The/Johnny",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/*2*/Jo*";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(9, result.Length);
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_TraversingWildcard_03()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person+=Doe/Jane",
                "/Person+=Doe/Joe",
                "/Person+=Doe/Johnny",
                "/Person+=Dane/John",
                "/Person+=Dane/Jane",
                "/Person+=Dane/Joe",
                "/Person+=Dane/Johnny",
                "/Person+=Dee/The/John",
                "/Person+=Dee/The/Jane",
                "/Person+=Dee/The/Joe",
                "/Person+=Dee/The/Johnny",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/*2*";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(12, result.Length);
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_TraversingWildcard_04()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person+=Doe/Jane",
                "/Person+=Doe/Joe",
                "/Person+=Doe/Johnny",
                "/Person+=Dane/John",
                "/Person+=Dane/Jane",
                "/Person+=Dane/Joe",
                "/Person+=Dane/Johnny",
                "/Person+=Dee/The/John",
                "/Person+=Dee/The/Jane",
                "/Person+=Dee/The/Joe",
                "/Person+=Dee/The/Johnny",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/*3*";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(selectScript, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(16, result.Length);
        }
    }
}
