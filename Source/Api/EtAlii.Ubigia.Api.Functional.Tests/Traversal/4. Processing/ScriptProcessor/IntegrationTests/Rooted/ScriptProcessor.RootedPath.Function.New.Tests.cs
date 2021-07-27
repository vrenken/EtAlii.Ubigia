// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ScriptProcessorRootedPathFunctionNewTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly TraversalUnitTestContext _testContext;

        public ScriptProcessorRootedPathFunctionNewTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = new TestScriptParserFactory().Create();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_New_Path_01()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            const string query = "Person: += Doe/John/Visits\r\n<= Person:Doe/John/Visits += new()";
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.NotNull(result[0]);
            Assert.StartsWith("Undefined_", result[0].ToString()!);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_New_Path_02()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            const string query = "Person: += Doe/John/Visits\r\nPerson:Doe/John/Visits += new('Vacation')";
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.NotNull(result[0]);
            Assert.Equal("Vacation", result[0].ToString());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_New_Path_03()
        {
            // Arrange.
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            const string query = "Person: += Doe/John/Visits\r\nPerson:Doe/John/Visits += new(\"Vacation\")";
            var script = _parser.Parse(query).Script;
            var processor = new TestScriptProcessorFactory().Create(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Single(result);
            Assert.NotNull(result[0]);
            Assert.Equal("Vacation", result[0].ToString());
        }
    }
}
