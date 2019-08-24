﻿namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorNonRootedPathAssignTagTests : IAsyncLifetime
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalTestContext _testContext;

        public async Task InitializeAsync()
        {
            _testContext = new LogicalTestContextFactory().Create();
            await _testContext.Start();

            _diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(_diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        public async Task DisposeAsync()
        {
            _parser = null;

            await _testContext.Stop();
            _testContext = null;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_NonRootedPath_Assign_Tags()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
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

            var addScript = _parser.Parse(addQueries).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

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
        public async Task ScriptProcessor_NonRootedPath_Get_Tags()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
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

            var addScript = _parser.Parse(addQueries).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var selectTagScript1 = _parser.Parse(selectTagQuery1).Script;
            var selectTagScript2 = _parser.Parse(selectTagQuery2).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

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
        public async Task ScriptProcessor_NonRootedPath_Filter_Tags()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
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

            var addScript = _parser.Parse(addQueries).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var filterByTagScript = _parser.Parse(filterByTagQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

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