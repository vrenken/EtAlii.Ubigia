namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics.Tests;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting.Parsing;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting.Processing;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;
    using TestAssembly = EtAlii.Ubigia.Api.Tests.TestAssembly;

    
    public class ScriptProcessor_Root_Assign_Tests : IClassFixture<LogicalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private readonly LogicalUnitTestContext _testContext;

        public ScriptProcessor_Root_Assign_Tests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
            var task = Task.Run(() =>
            {
                _diagnostics = TestDiagnostics.Create();
                var scriptParserConfiguration = new ScriptParserConfiguration()
                    .Use(_diagnostics);
                _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(() =>
            {
                _parser = null;
            });
            task.Wait();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Time_Root()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);

            const string query = "root:time <= EtAlii.Ubigia.Roots.Time";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();
            var root = (await logicalContext.Roots.GetAll()).SingleOrDefault(r => r.Name == "time");

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Equal(0, result.Length);
            Assert.NotNull(root);
            Assert.Equal("time", root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Time_Root_And_Using_Short_RootType()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);

            const string query = "root:time <= Time";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();
            var root = (await logicalContext.Roots.GetAll()).SingleOrDefault(r => r.Name == "time");

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Equal(0, result.Length);
            Assert.NotNull(root);
            Assert.Equal("time", root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Time_Root_Under_Other_Name()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);

            const string query = "root:specialtime <= EtAlii.Ubigia.Roots.Time";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();
            var root = (await logicalContext.Roots.GetAll()).SingleOrDefault(r => r.Name == "specialtime");

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Equal(0, result.Length);
            Assert.NotNull(root);
            Assert.Equal("specialtime", root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Time_Root_Under_Other_Name_And_Using_Short_RootType()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);

            const string query = "root:specialtime <= Time";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();
            var root = (await logicalContext.Roots.GetAll()).SingleOrDefault(r => r.Name == "specialtime");

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Equal(0, result.Length);
            Assert.NotNull(root);
            Assert.Equal("specialtime", root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Object_Root_Under_Other_Name()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);

            const string query = "root:projects <= EtAlii.Ubigia.Roots.Object";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();
            var root = (await logicalContext.Roots.GetAll()).SingleOrDefault(r => r.Name == "projects");

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Equal(0, result.Length);
            Assert.NotNull(root);
            Assert.Equal("projects", root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Root_Assign_Object_Root_Under_Other_Name_And_Using_Short_RootType()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);

            const string query = "root:projects <= Object";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();
            var root = (await logicalContext.Roots.GetAll()).SingleOrDefault(r => r.Name == "projects");

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Equal(0, result.Length);
            Assert.NotNull(root);
            Assert.Equal("projects", root.Name);
        }
    }
}