// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ScriptProcessorNonRootedPathQueryHierarchicalIntegrationTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly TraversalUnitTestContext _testContext;

        public ScriptProcessorNonRootedPathQueryHierarchicalIntegrationTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = new TestScriptParserFactory().Create(testContext.ClientConfiguration);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Query_Hierarchical_Parent_Child_Parent()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does/John <= { IsMale:true, Birthdate:1980-02-25, Weight:76.23 }"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/John\\";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            dynamic first = result.First();

            Assert.Equal("Does", first.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Query_Hierarchical_Parent_Child_Parent_With_Condition_Bool()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does/John <= { IsMale: true, Birthdate:1980-02-25, Weight:76.23 }"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/.IsMale=true\\";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();
            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            dynamic first = result.First();

            Assert.Equal("Does", first.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Query_Hierarchical_Parent_Child_Parent_With_Condition_DateTime()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does/John <= { IsMale: true, Birthdate: 1980-02-25, Weight: 76.23 }"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/.Birthdate=1980-02-25\\";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            dynamic first = result.First();

            Assert.Equal("Does", first.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Query_Hierarchical_Parent_Child_Parent_With_Condition_Float()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Does",
                "/Person/Does+=John",
                "/Person/Does/John <= { IsMale: true, Birthdate: 1980-02-25, Weight: 76.23 }"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Does/.Weight=76.23\\";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            dynamic first = result.First();

            Assert.Equal("Does", first.ToString());
        }
    }
}
