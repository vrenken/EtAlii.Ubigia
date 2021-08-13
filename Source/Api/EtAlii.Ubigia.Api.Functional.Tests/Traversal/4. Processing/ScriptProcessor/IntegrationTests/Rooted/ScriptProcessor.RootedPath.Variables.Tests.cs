// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ScriptProcessorRootedPathVariablesTests : IAsyncLifetime
    {
        private IScriptParser _parser;
        private TraversalUnitTestContext _testContext;

        public async Task InitializeAsync()
        {
            _testContext = new TraversalUnitTestContext();
            await _testContext
                .InitializeAsync()
                .ConfigureAwait(false);

            _parser = new TestScriptParserFactory().Create(_testContext.ClientConfiguration);
        }

        public async Task DisposeAsync()
        {
            await _testContext
                .DisposeAsync()
                .ConfigureAwait(false);
            _testContext = null;
            _parser = null;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Variables_QueryBy_FirstName_LastName()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical
                .CreateLogicalContext(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "Person: += Doe",
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
                "Person:Doe# <= FamilyName",
            };
            var selectQuery = "Person:$lastName/$firstName";

            var addScript = _parser.Parse(addQueries).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var scope = new FunctionalScope();
            scope.Variables.Add("lastName", new ScopeVariable("Doe", null));
            scope.Variables.Add("firstName", new ScopeVariable("John", null));

            var processor = _testContext.CreateScriptProcessor(logicalContext, scope);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var person = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(person);
            Assert.Equal("John", person.ToString());
        }
    }
}
