// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;

    public class ScriptProcessorAssignDynamic2IntegrationTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly TraversalUnitTestContext _testContext;
        private readonly IScriptParser _parser;

        public ScriptProcessorAssignDynamic2IntegrationTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = new TestScriptParserFactory().Create(testContext.ClientConfiguration);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_Should_Update_01()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical
                .CreateLogicalContext(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John";
            var assignQuery1 = "/Person/Doe/John <= $first";
            var assignQuery2 = "/Person/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var assignScript1 = _parser.Parse(assignQuery1, scope).Script;
            var assignScript2 = _parser.Parse(assignQuery2, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            scope = new ExecutionScope();
            dynamic firstVariable = new { ObjectType = "Family" };
            dynamic secondVariable = new { ObjectType = "Person" };
            scope.Variables.Add("first", new ScopeVariable(firstVariable, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(secondVariable, "Variable"));
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("Family", result1.ObjectType);
            Assert.Equal("Person", result2.ObjectType);
            Assert.NotEqual(((Node)result1).Id, ((Node)result2).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_Should_Not_Update_01()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John";
            var assignQuery1 = "/Person/Doe/John <= $first";
            var assignQuery2 = "/Person/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var assignScript1 = _parser.Parse(assignQuery1, scope).Script;
            var assignScript2 = _parser.Parse(assignQuery2, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            scope = new ExecutionScope();
            dynamic firstVariable = new {ObjectType = "Family"};
            dynamic secondVariable = new {ObjectType = "Family"};
            scope.Variables.Add("first", new ScopeVariable(firstVariable, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(secondVariable, "Variable"));
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("Family", result1.ObjectType);
            Assert.Equal("Family", result2.ObjectType);
            Assert.Equal(((Node)result1).Id, ((Node)result2).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_Should_Update_02()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John";
            var assignQuery1 = "/Person/Doe/John <= $first";
            var assignQuery2 = "/Person/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var assignScript1 = _parser.Parse(assignQuery1, scope).Script;
            var assignScript2 = _parser.Parse(assignQuery2, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            scope = new ExecutionScope();
            dynamic firstVariable = new {ObjectType = "Family", Code = "ABC"};
            dynamic secondVariable = new {ObjectType = "Person", Code = "ABC"};
            scope.Variables.Add("first", new ScopeVariable(firstVariable, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(secondVariable, "Variable"));
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("Family", result1.ObjectType);
            Assert.Equal("Person", result2.ObjectType);
            Assert.Equal("ABC", result1.Code);
            Assert.Equal("ABC", result2.Code);
            Assert.NotEqual(((Node)result1).Id, ((Node)result2).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_Should_Update_03()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John";
            var assignQuery1 = "/Person/Doe/John <= $first";
            var assignQuery2 = "/Person/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var assignScript1 = _parser.Parse(assignQuery1, scope).Script;
            var assignScript2 = _parser.Parse(assignQuery2, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            scope = new ExecutionScope();
            dynamic firstVariable = new {ObjectType = "Family"};
            dynamic secondVariable = new {ObjectType = "Person", Code = "ABC"};
            scope.Variables.Add("first", new ScopeVariable(firstVariable, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(secondVariable, "Variable"));
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("Family", result1.ObjectType);
            Assert.Equal("Person", result2.ObjectType);
            Assert.Equal("ABC", result2.Code);
            Assert.NotEqual(((Node)result1).Id, ((Node)result2).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_Should_Not_Update_02()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John";
            var assignQuery1 = "/Person/Doe/John <= $first";
            var assignQuery2 = "/Person/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var assignScript1 = _parser.Parse(assignQuery1, scope).Script;
            var assignScript2 = _parser.Parse(assignQuery2, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            scope = new ExecutionScope();
            dynamic firstVariable = new {ObjectType = "Family", Code = "ABC"};
            dynamic secondVariable = new {ObjectType = "Family", Code = "ABC"};
            scope.Variables.Add("first", new ScopeVariable(firstVariable, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(secondVariable, "Variable"));
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("Family", result1.ObjectType);
            Assert.Equal("Family", result2.ObjectType);
            Assert.Equal("ABC", result1.Code);
            Assert.Equal("ABC", result2.Code);
            Assert.Equal(((Node)result1).Id, ((Node)result2).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_Should_Not_Update_03()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John";
            var assignQuery1 = "/Person/Doe/John <= $first";
            var assignQuery2 = "/Person/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var assignScript1 = _parser.Parse(assignQuery1, scope).Script;
            var assignScript2 = _parser.Parse(assignQuery2, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            scope = new ExecutionScope();
            dynamic firstVariable = new { ObjectType = "Family", Code = "ABC" };
            dynamic secondVariable = new { ObjectType = "Family" };
            scope.Variables.Add("first", new ScopeVariable(firstVariable, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(secondVariable, "Variable"));
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("Family", result1.ObjectType);
            Assert.Equal("Family", result2.ObjectType);
            Assert.Equal("ABC", result1.Code);
            Assert.Equal("ABC", result2.Code);
            Assert.Equal(((Node)result1).Id, ((Node)result2).Id);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_Should_Not_Update_04()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John";
            var assignQuery1 = "/Person/Doe/John <= $first";
            var assignQuery2 = "/Person/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var assignScript1 = _parser.Parse(assignQuery1, scope).Script;
            var assignScript2 = _parser.Parse(assignQuery2, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            scope = new ExecutionScope();
            dynamic firstVariable = new { ObjectType = (string)null, Code = (string)null };
            dynamic secondVariable = new { ObjectType = (string)null, Code = (string)null };
            scope.Variables.Add("first", new ScopeVariable(firstVariable, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(secondVariable, "Variable"));
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            //Assert.Equal(null, result1.ObjectType)
            //Assert.Equal(null, result2.ObjectType)
            //Assert.Equal(null, result1.Code)
            //Assert.Equal(null, result2.Code)
            Assert.Equal(((Node)result1).Id, ((Node)result2).Id);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_Should_Not_Update_05()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/John";
            var assignQuery1 = "/Person/Doe/John <= $first";
            var assignQuery2 = "/Person/Doe/John <= $second";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var assignScript1 = _parser.Parse(assignQuery1, scope).Script;
            var assignScript2 = _parser.Parse(assignQuery2, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            scope = new ExecutionScope();
            dynamic firstVariable = new { ObjectType = "TEST", Code = (string)null };
            dynamic secondVariable = new { ObjectType = "TEST", Code = (string)null };
            scope.Variables.Add("first", new ScopeVariable(firstVariable, "Variable"));
            scope.Variables.Add("second", new ScopeVariable(secondVariable, "Variable"));
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("TEST", result1.ObjectType);
            Assert.Equal("TEST", result2.ObjectType);
            //Assert.Equal(null, result1.Code)
            //Assert.Equal(null, result2.Code)
            Assert.Equal(((Node)result1).Id, ((Node)result2).Id);
        }
    }
}
