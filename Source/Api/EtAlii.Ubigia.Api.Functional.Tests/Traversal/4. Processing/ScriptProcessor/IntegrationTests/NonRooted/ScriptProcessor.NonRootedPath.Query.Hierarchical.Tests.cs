// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using Xunit;

    public sealed class ScriptProcessorNonRootedPathQueryHierarchicalIntegrationTests : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly FunctionalUnitTestContext _testContext;

        public ScriptProcessorNonRootedPathQueryHierarchicalIntegrationTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = testContext.CreateScriptParser();
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Query_Hierarchical_Parent_Child_Parent()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does/John <= { IsMale:true, Birthdate:1980-02-25, Weight:76.23 }"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/John\\";

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
            Assert.Single(result);
            dynamic first = result.First();

            Assert.Equal("Does", first.ToString());
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Query_Hierarchical_Parent_Child_Parent_With_Condition_Bool()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does/John <= { IsMale: true, Birthdate:1980-02-25, Weight:76.23 }"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/.IsMale=true\\";

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
            Assert.Single(result);
            dynamic first = result.First();

            Assert.Equal("Does", first.ToString());
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Query_Hierarchical_Parent_Child_Parent_With_Condition_DateTime()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does/John <= { IsMale: true, Birthdate: 1980-02-25, Weight: 76.23 }"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/.Birthdate=1980-02-25\\";

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
            Assert.Single(result);
            dynamic first = result.First();

            Assert.Equal("Does", first.ToString());
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Query_Hierarchical_Parent_Child_Parent_With_Condition_Float()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does/John <= { IsMale: true, Birthdate: 1980-02-25, Weight: 76.23 }"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/.Weight=76.23\\";

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
            Assert.Single(result);
            dynamic first = result.First();

            Assert.Equal("Does", first.ToString());
        }
    }
}
