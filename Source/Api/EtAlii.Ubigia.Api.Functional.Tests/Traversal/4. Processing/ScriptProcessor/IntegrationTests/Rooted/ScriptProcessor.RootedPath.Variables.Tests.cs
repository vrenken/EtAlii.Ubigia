// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using Xunit;
    using UnitTestSettings = EtAlii.Ubigia.Api.Functional.Tests.UnitTestSettings;

    public class ScriptProcessorRootedPathVariablesTests : IAsyncLifetime
    {
        private IScriptParser _parser;
        private ILogicalTestContext _testContext;

        public async Task InitializeAsync()
        {
            _testContext = new LogicalTestContextFactory().Create();
            await _testContext.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);

            _parser = new TestScriptParserFactory().Create();
        }

        public async Task DisposeAsync()
        {
            _parser = null;

            await _testContext.Stop().ConfigureAwait(false);
            _testContext = null;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Variables_QueryBy_FirstName_LastName()
        {
            // Arrange.
            using var logicalContext = await _testContext.CreateLogicalContext(true).ConfigureAwait(false);
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
            var scriptScope = new ScriptScope();
            scriptScope.Variables.Add("lastName", new ScopeVariable("Doe", null));
            scriptScope.Variables.Add("firstName", new ScopeVariable("John", null));

            var processor = new TestScriptProcessorFactory().Create(logicalContext, scriptScope);

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
