// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;

    public class ScriptProcessorRootedPathFunctionIdTests : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly FunctionalUnitTestContext _testContext;

        public ScriptProcessorRootedPathFunctionIdTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = testContext.CreateScriptParser();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Id_Assign()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            const string query = "<= id() <= Person:";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsType<Identifier>(result.Single());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Id_Variable_Path_01()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            const string query = "Person: += Doe/John\r\n$path <= Person:Doe/John\r\nid($path)";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsType<Identifier>(result.Single());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Id_Variable_Path_02()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            const string query = "$path <= Person:\r\nid($path)";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsType<Identifier>(result.Single());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Id_Variable_Path_Variable()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            const string query = "$path <= Person:\r\n$id <= id($path)\r\n$id";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsType<Identifier>(result.Single());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Id_Path_01()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            const string query = "id(Person:)";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.IsType<Identifier>(result.Single());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Id_Path_02()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            const string query = "$var1 <= Person: += Doe/Jane\r\n$var2 <= id(Person:Doe/Jane)";
            var script = _parser.Parse(query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(script, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(result);
            Assert.Empty(result);  // << Question: should the $var2 assignment create output or not? Nope it should not. only assign (<=), function or path results should.
            Assert.IsAssignableFrom<Node>(await scope.Variables["var1"].Value.SingleAsync());
            Assert.IsType<Identifier>(await scope.Variables["var2"].Value.SingleAsync());
            Assert.Equal((await scope.Variables["var1"].Value.Cast<Node>().SingleAsync()).Id, await scope.Variables["var2"].Value.Cast<Identifier>().SingleAsync());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Id_Path_03()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "<= Id(Person:Doe/*)";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            var personsAfter = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(personsAfter);
            Assert.Equal(3, personsAfter.Length);
        }
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Function_Id_Path_04()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "$var1 <= Person:Doe/*\r\n<= Id($var1)";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            var personsAfter = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(personsAfter);
            Assert.Equal(3, personsAfter.Length);
        }

    }
}
