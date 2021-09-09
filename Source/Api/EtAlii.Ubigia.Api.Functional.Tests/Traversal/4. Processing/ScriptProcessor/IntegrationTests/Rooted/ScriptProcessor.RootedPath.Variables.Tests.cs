// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using Xunit;

    public class ScriptProcessorRootedPathVariablesTests : IAsyncLifetime
    {
        private IScriptParser _parser;
        private FunctionalUnitTestContext _testContext;

        public async Task InitializeAsync()
        {
            _testContext = new FunctionalUnitTestContext();
            await _testContext
                .InitializeAsync()
                .ConfigureAwait(false);

            _parser = _testContext.CreateScriptParser();
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
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
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

            var addScript = _parser.Parse(addQueries, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            scope.Variables.Add("lastName", new ScopeVariable("Doe", null));
            scope.Variables.Add("firstName", new ScopeVariable("John", null));

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            var person = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(person);
            Assert.Equal("John", person.ToString());
        }
    }
}
