// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ScriptProcessorAssignDynamicTimeIntegrationTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly TraversalUnitTestContext _testContext;
        private readonly IScriptParser _parser;

        public ScriptProcessorAssignDynamicTimeIntegrationTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = new TestScriptParserFactory().Create(testContext.ClientConfiguration);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_To_Path()
        {
            // Arrange.
            var addQueries = new[]
            {
                "<= Time:2014/09/06/16/33"
            };

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery = "<= Time:2014/09/06/16/33 <= $data";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new FunctionalScope { Variables = { ["data"] = new ScopeVariable(data, "data") } };
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext, scope);

            // Act.
            var lastSequence = await processor.Process(addScript);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence  = await processor.Process(updateScript);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(updateResult);
            Assert.NotNull(selectResult);
            Assert.Equal("Test1", selectResult.StringValue);
            Assert.Equal(12, selectResult.IntValue);
            //Assert.NotEqual(addResult.Id, updateResult.Id)
            //Assert.NotEqual(updateResult.Id, selectResult.Id)
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_To_Path_Empty()
        {
            // Arrange.
            var addQueries = new[]
            {
                "<= Time:2014/09/06/16/33"
            };

            dynamic data = new
            {
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery = "<= Time:2014/09/06/16/33 <= $data";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new FunctionalScope { Variables = { ["data"] = new ScopeVariable(data, "data") } };
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext, scope);

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
        public async Task ScriptProcessor_Assign_Dynamic_To_Path_Spaced()
        {
            // Arrange.
            var addQueries = new[]
            {
                "<= Time:2014/09/06/16/33"
            };

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery = "<= Time:2014/09/06/16/33 <= $data";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new FunctionalScope { Variables = { ["data"] = new ScopeVariable(data, "data") } };
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext, scope);

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
        public async Task ScriptProcessor_Assign_Dynamic_To_Path_Spaced_With_DateTime_Local()
        {
            // Arrange.
            var addQueries = new[]
            {
                "<= Time:2014/09/06/16/33"
            };

            var dateTime = new DateTime(2016, 04, 10, 21, 21, 04, DateTimeKind.Local);

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
                DateTime = dateTime,
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery = "<= Time:2014/09/06/16/33 <= $data";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new FunctionalScope { Variables = { ["data"] = new ScopeVariable(data, "data") } };
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext, scope);

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
            Assert.Equal(dateTime, selectResult.DateTime);
            Assert.Equal(dateTime.Kind, selectResult.DateTime.Kind);// "Kind"
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_To_Path_Spaced_With_DateTime_Utc()
        {
            // Arrange.
            var addQueries = new[]
            {
                "<= Time:2014/09/06/16/33"
            };

            var dateTime = new DateTime(2016, 04, 10, 21, 21, 04, DateTimeKind.Utc);

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
                DateTime = dateTime,
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery = "<= Time:2014/09/06/16/33 <= $data";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new FunctionalScope { Variables = { ["data"] = new ScopeVariable(data, "data") } };
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext, scope);

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
            Assert.Equal(dateTime, selectResult.DateTime);
            Assert.Equal(dateTime.Kind, selectResult.DateTime.Kind); // "Kind"
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_To_Path_Spaced_With_DateTime_Unspecified()
        {
            // Arrange.
            var addQueries = new[]
            {
                "<= Time:2014/09/06/16/33"
            };

            var dateTime = new DateTime(2016, 04, 10, 21, 21, 04, DateTimeKind.Unspecified);

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
                DateTime = dateTime,
            };

            var addQuery = string.Join("\r\n", addQueries);
            var updateQuery = "<= Time:2014/09/06/16/33 <= $data";
            var selectQuery = "Time:2014/09/06/16/33";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new FunctionalScope { Variables = { ["data"] = new ScopeVariable(data, "data") } };
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalContext, scope);

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
            Assert.Equal(dateTime, selectResult.DateTime);
            Assert.Equal(dateTime.Kind, selectResult.DateTime.Kind); // "Kind"
        }
    }
}
