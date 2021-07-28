// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;

    public class ScriptProcessorRootedPathAssignObjectIntegrationTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly IScriptParser _parser;
        private readonly TraversalUnitTestContext _testContext;

        public ScriptProcessorRootedPathAssignObjectIntegrationTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = new TestScriptParserFactory().Create();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Object_To_Path_String_And_String()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Time:+=2014",
                "Time:2014+=09",
                "Time:2014/09+=06",
                "Time:2014/09/06+=16",
                "<= Time:2014/09/06/16+=33"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery = "<= Time:2014/09/06/16/33 <= { StringValue1 : \'Test1\', StringValue2 : \'Test2\' }";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(updateResult);
            Assert.NotNull(selectResult);
            Assert.Equal("Test1", selectResult.StringValue1);
            Assert.Equal("Test2", selectResult.StringValue2);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Object_To_Path_String_And_Int()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Time:+=2014",
                "Time:2014+=09",
                "Time:2014/09+=06",
                "Time:2014/09/06+=16",
                "<= Time:2014/09/06/16+=33"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery = "<= Time:2014/09/06/16/33 <= { StringValue : \'Test1\', IntValue : 12 }";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(updateResult);
            Assert.NotNull(selectResult);
            Assert.Equal("Test1", selectResult.StringValue);
            Assert.Equal(12, selectResult.IntValue);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Object_To_Path_String_And_Bool_True()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Time:+=2014",
                "Time:2014+=09",
                "Time:2014/09+=06",
                "Time:2014/09/06+=16",
                "<= Time:2014/09/06/16+=33"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery = "<= Time:2014/09/06/16/33 <= { StringValue : \'Test1\', BoolValue : true }";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(updateResult);
            Assert.NotNull(selectResult);
            Assert.Equal("Test1", selectResult.StringValue);
            Assert.Equal(true, selectResult.BoolValue);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Object_To_Path_String_And_Bool_False()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Time:+=2014",
                "Time:2014+=09",
                "Time:2014/09+=06",
                "Time:2014/09/06+=16",
                "<= Time:2014/09/06/16+=33"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery = "<= Time:2014/09/06/16/33 <= { StringValue : \'Test1\', BoolValue : false }";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(updateResult);
            Assert.NotNull(selectResult);
            Assert.Equal("Test1", selectResult.StringValue);
            Assert.Equal(false, selectResult.BoolValue);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Object_To_Path_String_And_Bool_False_Capitals()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Time:+=2014",
                "Time:2014+=09",
                "Time:2014/09+=06",
                "Time:2014/09/06+=16",
                "<= Time:2014/09/06/16+=33"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery = "<= Time:2014/09/06/16/33 <= { StringValue : \'Test1\', BoolValue : FALSE }";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(updateResult);
            Assert.NotNull(selectResult);
            Assert.Equal("Test1", selectResult.StringValue);
            Assert.Equal(false, selectResult.BoolValue);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Object_To_Path_String_And_Bool_False_Camel()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Time:+=2014",
                "Time:2014+=09",
                "Time:2014/09+=06",
                "Time:2014/09/06+=16",
                "<= Time:2014/09/06/16+=33"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery = "<= Time:2014/09/06/16/33 <= { StringValue : \'Test1\', BoolValue : False }";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(updateResult);
            Assert.NotNull(selectResult);
            Assert.Equal("Test1", selectResult.StringValue);
            Assert.Equal(false, selectResult.BoolValue);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Object_To_Path_Empty()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Time:+=2014",
                "Time:2014+=09",
                "Time:2014/09+=06",
                "Time:2014/09/06+=16",
                "<= Time:2014/09/06/16+=33"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery = "<= Time:2014/09/06/16/33 <= { }";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(updateResult);
            Assert.NotNull(selectResult);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Object_To_Path_Spaced()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Time: += 2014",
                "Time:2014 += 09",
                "Time:2014/09 += 06",
                "Time:2014/09/06 += 16",
                "<= Time:2014/09/06/16 += 33"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery = "<= Time:2014/09/06/16/33 <= { StringValue : \'Test1\', IntValue : 12 }";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(updateResult);
            Assert.NotNull(selectResult);
            Assert.Equal("Test1", selectResult.StringValue);
            Assert.Equal(12, selectResult.IntValue);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Object_To_Path_And_Update_01()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Time: += 2014",
                "Time:2014 += 09",
                "Time:2014/09 += 06",
                "Time:2014/09/06 += 16",
                "<= Time:2014/09/06/16 += 33"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery1 = "<= Time:2014/09/06/16/33 <= { StringValue : \'Test1\', IntValue : \'12\' }";
            var updateQuery2 = "<= Time:2014/09/06/16/33 <= { StringValue : \'Test2\', IntValue : 13 }";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript1 = _parser.Parse(updateQuery1).Script;
            var updateScript2 = _parser.Parse(updateQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript1);
            dynamic updateResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript2);
            dynamic updateResult2 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(updateResult1);
            Assert.NotNull(updateResult2);
            Assert.NotNull(selectResult1);
            Assert.Equal("Test1", selectResult1.StringValue);
            Assert.Equal("12", selectResult1.IntValue);
            Assert.NotNull(selectResult2);
            Assert.Equal("Test2", selectResult2.StringValue);
            Assert.Equal(13, selectResult2.IntValue);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Object_To_Path_And_Update_02()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Time: += 2014",
                "Time:2014 += 09",
                "Time:2014/09 += 06",
                "Time:2014/09/06 += 16",
                "<= Time:2014/09/06/16 += 33"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery1 = "<= Time:2014/09/06/16/33 <= { StringValue : \'Test1\', IntValue : \'12\' }";
            var updateQuery2 = "<= Time:2014/09/06/16/33 <= { StringValue : \'Test2\', IntValue : \'13\', BoolValue : true  }";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript1 = _parser.Parse(updateQuery1).Script;
            var updateScript2 = _parser.Parse(updateQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript1);
            dynamic updateResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript2);
            dynamic updateResult2 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(updateResult1);
            Assert.NotNull(updateResult2);
            Assert.NotNull(selectResult1);
            Assert.Equal("Test1", selectResult1.StringValue);
            Assert.Equal("12", selectResult1.IntValue);
            Assert.False(((Node)selectResult1).Properties.ContainsKey("BoolValue"));
            Assert.NotNull(selectResult2);
            Assert.Equal("Test2", selectResult2.StringValue);
            Assert.Equal("13", selectResult2.IntValue);
            Assert.Equal(true, selectResult2.BoolValue);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Object_To_Path_And_Update_03()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Time: += 2014",
                "Time:2014 += 09",
                "Time:2014/09 += 06",
                "Time:2014/09/06 += 16",
                "<= Time:2014/09/06/16 += 33"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery1 = "<= Time:2014/09/06/16/33 <= { StringValue : \'Test1\', IntValue : \'12\' }";
            var updateQuery2 = "<= Time:2014/09/06/16/33 <= { StringValue : \'Test2\', IntValue : , BoolValue : true  }";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript1 = _parser.Parse(updateQuery1).Script;
            var updateScript2 = _parser.Parse(updateQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript1);
            dynamic updateResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript2);
            dynamic updateResult2 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(updateResult1);
            Assert.NotNull(updateResult2);
            Assert.NotNull(selectResult1);
            Assert.Equal("Test1", selectResult1.StringValue);
            Assert.Equal("12", selectResult1.IntValue);
            Assert.False(((Node)selectResult1).Properties.ContainsKey("BoolValue"));
            Assert.NotNull(selectResult2);
            Assert.Equal("Test2", selectResult2.StringValue);
            Assert.False(((Node)selectResult2).Properties.ContainsKey("IntValue"));
            Assert.Equal(true, selectResult2.BoolValue);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Object_To_Path_And_Clear()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Time: += 2014",
                "Time:2014 += 09",
                "Time:2014/09 += 06",
                "Time:2014/09/06 += 16",
                "<= Time:2014/09/06/16 += 33"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery1 = "<= Time:2014/09/06/16/33 <= { StringValue : \'Test1\', IntValue : \'12\' }";
            var updateQuery2 = "<= Time:2014/09/06/16/33 <= { StringValue : , IntValue : }";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript1 = _parser.Parse(updateQuery1).Script;
            var updateScript2 = _parser.Parse(updateQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript1);
            dynamic updateResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript2);
            dynamic updateResult2 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(updateResult1);
            Assert.NotNull(updateResult2);
            Assert.NotNull(selectResult1);
            Assert.Equal("Test1", selectResult1.StringValue);
            Assert.Equal("12", selectResult1.IntValue);
            Assert.False(((Node)selectResult1).Properties.ContainsKey("BoolValue"));
            Assert.NotNull(selectResult2);
            Assert.Empty(((Node)selectResult2).Properties);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Object_To_Path_And_Clear_Incorrect()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Time: += 2014",
                "Time:2014 += 09",
                "Time:2014/09 += 06",
                "Time:2014/09/06 += 16",
                "<= Time:2014/09/06/16 += 33"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery1 = "<= Time:2014/09/06/16/33 <= { StringValue : \'Test1\', IntValue : \'12\' }";
            var updateQuery2 = "<= Time:2014/09/06/16/33 <= { }";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript1 = _parser.Parse(updateQuery1).Script;
            var updateScript2 = _parser.Parse(updateQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript1);
            dynamic updateResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult1 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript2);
            dynamic updateResult2 = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(updateResult1);
            Assert.NotNull(updateResult2);
            Assert.NotNull(selectResult1);
            Assert.Equal("Test1", selectResult1.StringValue);
            Assert.Equal("12", selectResult1.IntValue);
            Assert.False(((Node)selectResult1).Properties.ContainsKey("BoolValue"));
            Assert.NotNull(selectResult2);
            Assert.Equal(2, ((Node)selectResult2).Properties.Count);
            Assert.Equal("Test1", selectResult2.StringValue);
            Assert.Equal("12", selectResult2.IntValue);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Should_Update_01()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Person:+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "Person:Doe/John";
            var assignQuery1 = "Person:Doe/John <= { ObjectType: 'Family' }";
            var assignQuery2 = "Person:Doe/John <= { ObjectType: 'Person' }";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("Family", result1.ObjectType);
            Assert.Equal("Person", result2.ObjectType);
            Assert.NotEqual(((Node)result1).Id, ((Node)result2).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Should_Not_Update_01()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Person:+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "Person:Doe/John";
            var assignQuery1 = "Person:Doe/John <= { ObjectType: 'Family' }";
            var assignQuery2 = "Person:Doe/John <= { ObjectType: 'Family' }";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result2 = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("Family", result1.ObjectType);
            Assert.Equal("Family", result2.ObjectType);
            Assert.Equal(((Node)result1).Id, ((Node)result2).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Should_Update_02()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Person:+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "Person:Doe/John";
            var assignQuery1 = "Person:Doe/John <= { ObjectType: 'Family', Code: 'ABC' }";
            var assignQuery2 = "Person:Doe/John <= { ObjectType: 'Person', Code: 'ABC' }";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
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
        public async Task ScriptProcessor_RootedPath_Assign_Should_Update_03()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Person:+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "Person:Doe/John";
            var assignQuery1 = "Person:Doe/John <= { ObjectType: 'Family' }";
            var assignQuery2 = "Person:Doe/John <= { ObjectType: 'Person', Code: 'ABC' }";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
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
        public async Task ScriptProcessor_RootedPath_Assign_Should_Not_Update_02()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Person:+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "Person:Doe/John";
            var assignQuery1 = "Person:Doe/John <= { ObjectType: 'Family', Code: 'ABC' }";
            var assignQuery2 = "Person:Doe/John <= { ObjectType: 'Family', Code: 'ABC' }";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
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
        public async Task ScriptProcessor_RootedPath_Assign_Should_Not_Update_03()
        {
            // Arrange.
            var addQueries = new[]
            {
                "Person:+=Doe/John",
            };
            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "Person:Doe/John";
            var assignQuery1 = "Person:Doe/John <= { ObjectType: 'Family', Code: 'ABC' }";
            var assignQuery2 = "Person:Doe/John <= { ObjectType: 'Family' }";

            var addScript = _parser.Parse(addQuery).Script;
            var assignScript1 = _parser.Parse(assignQuery1).Script;
            var assignScript2 = _parser.Parse(assignQuery2).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            // Act.
            lastSequence = await processor.Process(assignScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic result1 = await lastSequence.Output.SingleOrDefaultAsync();

            lastSequence = await processor.Process(assignScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
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

    }
}
