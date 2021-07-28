// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ScriptProcessorRootedPathAssignTagTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly TraversalUnitTestContext _testContext;

        public ScriptProcessorRootedPathAssignTagTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = new TestScriptParserFactory().Create(testContext.ClientConfiguration);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Tags()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
                "Person:Doe# <= FamilyName",
                "Person:Doe/John# <= FirstName",
                "Person:Doe/Jane# <= FirstName",
                "Person:Doe/Johnny# <= FirstName",
            };

            var selectQuery = "Person:Doe/";

            var addScript = _parser.Parse(addQueries).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(selectScript);
            var people = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(lastSequence);
            Assert.Equal(3, people.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_Tags()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
                "Person:Doe# <= FamilyName",
                "Person:Doe/John# <= FirstName",
                "Person:Doe/Jane# <= FirstName",
                "Person:Doe/Johnny# <= FirstName",
            };

            var selectQuery = "Person:Doe/";
            var selectTagQuery1 = "Person:Doe#";
            var selectTagQuery2 = "Person:Doe/Jane#";

            var addScript = _parser.Parse(addQueries).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var selectTagScript1 = _parser.Parse(selectTagQuery1).Script;
            var selectTagScript2 = _parser.Parse(selectTagQuery2).Script;

            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectTagScript1);
            var tag1Result = await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectTagScript2);
            var tag2Result = await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(selectScript);
            var people = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(lastSequence);
            Assert.Single(tag1Result);
            Assert.Equal("FamilyName", tag1Result[0]);
            Assert.Single(tag2Result);
            Assert.Equal("FirstName", tag2Result[0]);
            Assert.Equal(3, people.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Filter_Tags()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
                "Person:Doe# <= FamilyName",
                "Person:Doe/John# <= FirstName",
                "Person:Doe/Jane# <= FirstName",
                "Person:Doe/Johnny# <= NickName",
            };

            var selectQuery = "Person:Doe/";
            var filterByTagQuery = "Person:Doe/#FirstName";

            var addScript = _parser.Parse(addQueries).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var filterByTagScript = _parser.Parse(filterByTagQuery).Script;

            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(filterByTagScript);
            var filterByTagResult = await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(selectScript);
            var people = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(lastSequence);
            Assert.Equal(2, filterByTagResult.Length);
            Assert.Equal(3, people.Length);
        }

    }
}
