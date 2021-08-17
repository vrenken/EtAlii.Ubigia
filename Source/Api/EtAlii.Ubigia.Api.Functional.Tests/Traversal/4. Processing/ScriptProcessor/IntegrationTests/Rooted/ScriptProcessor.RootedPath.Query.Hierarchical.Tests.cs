// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ScriptProcessorRootedPathQueryHierarchicalIntegrationTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly TraversalUnitTestContext _testContext;
        private readonly IScriptParser _parser;

        public ScriptProcessorRootedPathQueryHierarchicalIntegrationTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = new TestScriptParserFactory().Create(testContext.ClientConfiguration);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Query_Hierarchical_Parent_Child_Parent()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var addQueries = new[]
            {
                "Person:+=Does",
                "Person:Does+=John",
                "Person:Does/John <= { IsMale:true, Birthdate:1980-02-25, Weight:76.23 }"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "Person:Does/John\\";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

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

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Query_Hierarchical_Parent_Child_Parent_With_Condition_Bool()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var addQueries = new[]
            {
                "Person:+=Does",
                "Person:Does+=John",
                "Person:Does/John <= { IsMale: true, Birthdate:1980-02-25, Weight:76.23 }"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "Person:Does/.IsMale=true\\";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

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

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Query_Hierarchical_Parent_Child_Parent_With_Condition_DateTime()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var addQueries = new[]
            {
                "Person:+=Does",
                "Person:Does+=John",
                "Person:Does/John <= { IsMale: true, Birthdate: 1980-02-25, Weight: 76.23 }"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "Person:Does/.Birthdate=1980-02-25\\";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

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

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Query_Hierarchical_Parent_Child_Parent_With_Condition_Float()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var addQueries = new[]
            {
                "Person:+=Does",
                "Person:Does+=John",
                "Person:Does/John <= { IsMale: true, Birthdate: 1980-02-25, Weight: 76.23 }"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "Person:Does/.Weight=76.23\\";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

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
