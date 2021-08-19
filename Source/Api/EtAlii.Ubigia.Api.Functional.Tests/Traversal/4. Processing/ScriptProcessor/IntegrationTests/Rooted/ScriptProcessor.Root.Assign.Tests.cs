// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using Xunit;

    public class ScriptProcessorRootAssignTests : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly FunctionalUnitTestContext _testContext;

        public ScriptProcessorRootAssignTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = testContext.CreateScriptParser();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Time_Root()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContextWithConnection(true).ConfigureAwait(false);

            const string query = "root:time <= EtAlii.Ubigia.Roots.Time";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();
            var root = await logicalContext.Roots
                .GetAll()
                .SingleOrDefaultAsync(r => r.Name == "time")
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.NotNull(root);
            Assert.Equal("time", root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Time_Root_And_Using_Short_RootType()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContextWithConnection(true).ConfigureAwait(false);

            const string query = "root:time <= Time";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();
            var root = await logicalContext.Roots.GetAll().SingleOrDefaultAsync(r => r.Name == "time").ConfigureAwait(false);

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.NotNull(root);
            Assert.Equal("time", root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Time_Root_Under_Other_Name()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContextWithConnection(true).ConfigureAwait(false);

            const string query = "root:specialtime <= EtAlii.Ubigia.Roots.Time";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();
            var root = await logicalContext.Roots.GetAll().SingleOrDefaultAsync(r => r.Name == "specialtime").ConfigureAwait(false);

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.NotNull(root);
            Assert.Equal("specialtime", root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Time_Root_Under_Other_Name_And_Using_Short_RootType()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContextWithConnection(true).ConfigureAwait(false);

            const string query = "root:specialtime <= Time";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();
            var root = await logicalContext.Roots.GetAll().SingleOrDefaultAsync(r => r.Name == "specialtime").ConfigureAwait(false);

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.NotNull(root);
            Assert.Equal("specialtime", root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Object_Root_Under_Other_Name()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContextWithConnection(true).ConfigureAwait(false);

            const string query = "root:projects <= EtAlii.Ubigia.Roots.Object";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();
            var root = await logicalContext.Roots.GetAll().SingleOrDefaultAsync(r => r.Name == "projects").ConfigureAwait(false);

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.NotNull(root);
            Assert.Equal("projects", root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Object_Root_Under_Other_Name_And_Using_Short_RootType()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContextWithConnection(true).ConfigureAwait(false);

            const string query = "root:projects <= Object";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();
            var root = await logicalContext.Roots.GetAll().SingleOrDefaultAsync(r => r.Name == "projects").ConfigureAwait(false);

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.NotNull(root);
            Assert.Equal("projects", root.Name);
        }
    }
}
