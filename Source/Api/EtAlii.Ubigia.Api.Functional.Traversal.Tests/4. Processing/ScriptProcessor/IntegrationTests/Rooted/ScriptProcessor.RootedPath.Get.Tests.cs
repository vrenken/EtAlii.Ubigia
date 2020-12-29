namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorRootedPathGetTests : IClassFixture<ScriptingUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;
        private readonly IDiagnosticsConfiguration _diagnostics;
        private readonly ScriptingUnitTestContext _testContext;

        public ScriptProcessorRootedPathGetTests(ScriptingUnitTestContext testContext)
        {
            _testContext = testContext;
            _diagnostics = DiagnosticsConfiguration.Default;
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(_diagnostics);
            _parser = new TestScriptParserFactory().Create(scriptParserConfiguration);
        }
        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItem()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);

            const string query = "Time:";
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(script);
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByVariable_1()
        {
            // Arrange.
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                "$var1 <= Time:",
                "$var1"
            };

            var script = _parser.Parse(queries).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsType<DynamicNode>(result.Single());
            Assert.Equal("Time", result.Cast<INode>().Single().Type);
        }

        //[Ignore, TestMethod, TestCategory(TestAssembly.Category)]
        //public async Task ScriptProcessor_RootedPath_Get_GetItemByVariable_2()
        //[
        //    // Arrange.
        //    using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true)
        //    var queries = new[]
        //    [
        //        "$var1 <= \"Time\":",
        //        "$var1"
        //    ]
        //    var script = _parser.Parse(queries).Script
        //    var scope = new ScriptScope()
        //    var configuration = new ScriptProcessorConfiguration()
        //        .Use(_diagnostics)
        //        .Use(scope)
        //        .Use(logicalContext)
        //    var processor = new ScriptProcessorFactory().Create(configuration)

        //    // Act.
        //    var lastSequence = await processor.Process(script)
        //    var result = await lastSequence.Output.ToArray()

        //    // Assert.
        //    Assert.NotNull(result)
        //    Assert.IsType<DynamicNode>(result.Single())
        //    Assert.Equal("Time", result.Cast<INode>().Single().Type)
        //]
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByVariables_1_Absolute_1()
        {
            // Arrange.
            var continent = "Europe";

            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                $"Location:+={continent}",
                "$var1 <= Location",
                $"$var2 <= {continent}",
                "/$var1/$var2"
            };

            var query = string.Join("\r\n", queries);
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<INode>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByVariables_1_Absolute_2()
        {
            // Arrange.
            var continent = "Europe";

            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                $"Location:+={continent}",
                "$var1 <= /Location",
                $"$var2 <= {continent}",
                "$var1/$var2"
            };

            var query = string.Join("\r\n", queries);
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<INode>().Single().Type);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByVariables_1_Rooted()
        {
            // Arrange.
            var continent = "Europe";

            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                $"Location:+={continent}",
                "$var1 <= Location:",
                $"$var2 <= {continent}",
                "$var1/$var2"
            };

            var query = string.Join("\r\n", queries);
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(lastSequence);
            Assert.NotNull(lastSequence.Sequence);
            Assert.NotNull(lastSequence.ExecutionPlan);
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<INode>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByVariables_2_Absolute_1()
        {
            // Arrange.
            var continent = "Europe";

            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                $"Location:+={continent}",
                "$var1 <= \"Location\"",
                $"$var2 <= \"{continent}\"",
                "/$var1/$var2"
            };

            var script = _parser.Parse(queries).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<INode>().Single().Type);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByVariables_2_Absolute_2()
        {
            // Arrange.
            var continent = "Europe";

            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                $"Location:+={continent}",
                "$var1 <= /\"Location\"",
                $"$var2 <= \"{continent}\"",
                "$var1/$var2"
            };

            var script = _parser.Parse(queries).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<INode>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByVariables_2()
        {
            // Arrange.
            var continent = "Europe";

            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                $"Location:+={continent}",
                "$var1 <= \"Location\"",
                $"$var2 <= \"{continent}\"",
                "/$var1/$var2"
            };

            var script = _parser.Parse(queries).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<INode>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByVariables_Spaced()
        {
            // Arrange.
            var continent = "Europe";
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                $"Location: += {continent}",
                "$var1 <= \"Location\"",
                $"$var2 <= \"{continent}\"",
                "/$var1/$var2"
            };

            var script = _parser.Parse(queries).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal(continent, result.Cast<INode>().Single().Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByCompositeVariable_01()
        {
            // Arrange.
            var now = DateTime.Now;
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                "Time:+={0:yyyy}",
                "$var1 <= Time:{0:yyyy}",
                "$var1"
            };

            var query = string.Format(string.Join("\r\n", queries), now);
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal("000", result.Cast<INode>().Single().Type); // A time root query will return 000 milliseconds.
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByCompositeVariable_02()
        {
            // Arrange.
            var now = DateTime.Now;
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                "$var1 <= Time:{0:yyyy}{0:MM}{0:dd}{0:HH}{0:mm}{0:ss}{0:fff}",
                "$var1"
            };

            var query = string.Format(string.Join("\r\n", queries), now);
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal($"{now:fff}", result.Cast<INode>().Single().Type); // A time root query will return milliseconds.
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByCompositeVariable_03()
        {
            // Arrange.
            var past = DateTime.Now.Subtract(TimeSpan.FromSeconds(5));
            var now = DateTime.Now;
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                "Time:{1:yyyy}{1:MM}{1:dd}{1:HH}{1:mm}{1:ss}{1:fff}", // This should not have any impact.
                "$var1 <= Time:{0:yyyy}{0:MM}{0:dd}{0:HH}{0:mm}{0:ss}{0:fff}",
                "$var1"
            };

            var query = string.Format(string.Join("\r\n", queries), now, past);
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal($"{now:fff}", result.Cast<INode>().Single().Type); // A time root query will return milliseconds.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByCompositeVariable_Spaced_01()
        {
            // Arrange.
            var now = DateTime.Now;
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                "Time: += {0:yyyy}",
                "$var1 <= Time:{0:yyyy}",
                "$var1"
            };

            var query = string.Format(string.Join("\r\n", queries), now);
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal("000", result.Cast<INode>().Single().Type); // A time root query will return milliseconds.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Get_GetItemByCompositeVariable_Spaced_02()
        {
            // Arrange.
            var now = DateTime.Now;
            using var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            var queries = new[]
            {
                "Time: += {0:yyyy}",
                "$var1 <= Time:{0:yyyy}{0:MM}{0:dd}{0:HH}{0:mm}{0:ss}{0:fff}",
                "$var1"
            };

            var query = string.Format(string.Join("\r\n", queries), now);
            var script = _parser.Parse(query).Script;
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<object>>(result);
            Assert.Equal($"{now:fff}", result.Cast<INode>().Single().Type); // A time root query will return milliseconds.
        }
    }
}
