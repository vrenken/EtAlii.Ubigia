// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public sealed class ScriptProcessorNonRootedPathAssignTagTests : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly FunctionalUnitTestContext _testContext;

        public ScriptProcessorNonRootedPathAssignTagTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = testContext.CreateScriptParser();
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Assign_Tags()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person/+=Doe/John",
                "/Person/+=Doe/Jane",
                "/Person/+=Doe/Johnny",
                "/Person/Doe# <= FamilyName",
                "/Person/Doe/John# <= FirstName",
                "/Person/Doe/Jane# <= FirstName",
                "/Person/Doe/Johnny# <= FirstName",
            };

            var selectQuery = "/Person/Doe/";

            var addScript = _parser.Parse(addQueries, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(selectScript, scope);
            var people = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(lastSequence);
            Assert.Equal(3, people.Length);
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Get_Tags()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person/+=Doe/John",
                "/Person/+=Doe/Jane",
                "/Person/+=Doe/Johnny",
                "/Person/Doe# <= FamilyName",
                "/Person/Doe/John# <= FirstName",
                "/Person/Doe/Jane# <= FirstName",
                "/Person/Doe/Johnny# <= FirstName",
            };

            var selectQuery = "/Person/Doe/";
            var selectTagQuery1 = "/Person/Doe#";
            var selectTagQuery2 = "/Person/Doe/Jane#";

            var addScript = _parser.Parse(addQueries, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var selectTagScript1 = _parser.Parse(selectTagQuery1, scope).Script;
            var selectTagScript2 = _parser.Parse(selectTagQuery2, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectTagScript1, scope);
            var tag1Result = await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectTagScript2, scope);
            var tag2Result = await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(selectScript, scope);
            var people = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(lastSequence);
            Assert.Single(tag1Result);
            Assert.Equal("FamilyName", tag1Result[0]);
            Assert.Single(tag2Result);
            Assert.Equal("FirstName", tag2Result[0]);
            Assert.Equal(3, people.Length);
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Filter_Tags()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person/+=Doe/John",
                "/Person/+=Doe/Jane",
                "/Person/+=Doe/Johnny",
                "/Person/Doe# <= FamilyName",
                "/Person/Doe/John# <= FirstName",
                "/Person/Doe/Jane# <= FirstName",
                "/Person/Doe/Johnny# <= NickName",
            };

            var selectQuery = "/Person/Doe/";
            var filterByTagQuery = "/Person/Doe/#FirstName";

            var addScript = _parser.Parse(addQueries, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var filterByTagScript = _parser.Parse(filterByTagQuery, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(filterByTagScript, scope);
            var filterByTagResult = await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(selectScript, scope);
            var people = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(lastSequence);
            Assert.Equal(2, filterByTagResult.Length);
            Assert.Equal(3, people.Length);
        }
    }
}
