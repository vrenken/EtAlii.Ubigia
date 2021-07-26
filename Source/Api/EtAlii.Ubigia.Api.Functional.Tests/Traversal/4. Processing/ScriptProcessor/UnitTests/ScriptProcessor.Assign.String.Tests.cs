// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;
    using Xunit;

    public class ScriptProcessorAssignStringUnitTests : IDisposable
    {
        private IScriptParser _parser;

        public ScriptProcessorAssignStringUnitTests()
        {
            _parser = new TestScriptParserFactory().Create();
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_String_To_Variable()
        {
            // Arrange.
            var script = _parser.Parse("$var1 <= \"Time\"").Script;
            var scope = new ScriptScope();
            var configuration = new TraversalProcessorConfiguration()
                .UseFunctionalDiagnostics(TestConfiguration.Root)
                .UseTestProcessor()
                .Use(scope);
            var processor = new TestScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            await lastSequence.Output.ToArray();
            // Assert.
            Assert.Equal("Time", await scope.Variables["var1"].Value.SingleAsync());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_String_To_Variable_Via_Variable()
        {
            // Arrange.
            var script = _parser.Parse("$var1 <= \"Time\"\r\n$var2 <= $var1").Script;
            var scope = new ScriptScope();
            var configuration = new TraversalProcessorConfiguration()
                .UseFunctionalDiagnostics(TestConfiguration.Root)
                .UseTestProcessor()
                .Use(scope);
            var processor = new TestScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            await lastSequence.Output.ToArray();

            // Assert.
            Assert.Equal("Time", await scope.Variables["var2"].Value.SingleAsync());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_String_To_Variable_Via_Variable_With_Replace()
        {
            // Arrange.
            var script = _parser.Parse("$var1 <= \"Time\"\r\n$var2 <= $var1\r\n$var1 <= \"Location\"").Script;
            var scope = new ScriptScope();
            var configuration = new TraversalProcessorConfiguration()
                .UseFunctionalDiagnostics(TestConfiguration.Root)
                .UseTestProcessor()
                .Use(scope);
            var processor = new TestScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            await lastSequence.Output.ToArray();

            // Assert.
            Assert.Equal("Time", await scope.Variables["var2"].Value.SingleAsync());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_String_To_Variable_Via_Variable_With_Clear()
        {
            // Arrange.
            var script = _parser.Parse("$var1 <= \"Time\"\r\n$var2 <= $var1\r\n$var1 <=").Script;
            var scope = new ScriptScope();
            var configuration = new TraversalProcessorConfiguration()
                .UseFunctionalDiagnostics(TestConfiguration.Root)
                .UseTestProcessor()
                .Use(scope);
            var processor = new TestScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            await lastSequence.Output.LastOrDefaultAsync();

            // Assert.
            Assert.Equal("Time", await scope.Variables["var2"].Value.SingleAsync());
            Assert.False(scope.Variables.ContainsKey("var1"));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_String_To_Variable_Via_Variable_With_Empty_String()
        {
            // Arrange.
            var script = _parser.Parse("$var1 <= \"Time\"\r\n$var2 <= $var1\r\n$var1 <= \"\"").Script;
            var scope = new ScriptScope();
            var configuration = new TraversalProcessorConfiguration()
                .UseFunctionalDiagnostics(TestConfiguration.Root)
                .UseTestProcessor()
                .Use(scope);
            var processor = new TestScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(script);
            await lastSequence.Output.ToArray();

            // Assert.
            Assert.Equal("Time", await scope.Variables["var2"].Value.SingleAsync());
            Assert.Equal("", await scope.Variables["var1"].Value.SingleAsync());
        }

    }
}
