// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public sealed class ScriptProcessorAssignDynamicTimeIntegrationTests : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly FunctionalUnitTestContext _testContext;
        private readonly IScriptParser _parser;

        public ScriptProcessorAssignDynamicTimeIntegrationTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = testContext.CreateScriptParser();
        }

        [Fact]
        public async Task ScriptProcessor_Assign_Dynamic_To_Path()
        {
            // Arrange.
            var scope = new ExecutionScope();
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

            var addScript = _parser.Parse(addQuery, scope).Script;
            var updateScript = _parser.Parse(updateQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            scope = new ExecutionScope { Variables = { ["data"] = new ScopeVariable(data, "data") } };
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence  = await processor.Process(updateScript, scope);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
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

        [Fact]
        public async Task ScriptProcessor_Assign_Dynamic_To_Path_Empty()
        {
            // Arrange.
            var scope = new ExecutionScope();
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

            var addScript = _parser.Parse(addQuery, scope).Script;
            var updateScript = _parser.Parse(updateQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            scope = new ExecutionScope { Variables = { ["data"] = new ScopeVariable(data, "data") } };
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript, scope);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(updateResult);
            Assert.NotNull(selectResult);
        }

        [Fact]
        public async Task ScriptProcessor_Assign_Dynamic_To_Path_Spaced()
        {
            // Arrange.
            var scope = new ExecutionScope();
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

            var addScript = _parser.Parse(addQuery, scope).Script;
            var updateScript = _parser.Parse(updateQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            scope = new ExecutionScope { Variables = { ["data"] = new ScopeVariable(data, "data") } };
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript, scope);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic selectResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(updateResult);
            Assert.NotNull(selectResult);
            Assert.Equal("Test1", selectResult.StringValue);
            Assert.Equal(12, selectResult.IntValue);
        }


        [Fact]
        public async Task ScriptProcessor_Assign_Dynamic_To_Path_Spaced_With_DateTime_Local()
        {
            // Arrange.
            var scope = new ExecutionScope();
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

            var addScript = _parser.Parse(addQuery, scope).Script;
            var updateScript = _parser.Parse(updateQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            scope = new ExecutionScope { Variables = { ["data"] = new ScopeVariable(data, "data") } };
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript, scope);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
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

        [Fact]
        public async Task ScriptProcessor_Assign_Dynamic_To_Path_Spaced_With_DateTime_Utc()
        {
            // Arrange.
            var scope = new ExecutionScope();
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

            var addScript = _parser.Parse(addQuery, scope).Script;
            var updateScript = _parser.Parse(updateQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            scope = new ExecutionScope { Variables = { ["data"] = new ScopeVariable(data, "data") } };
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript, scope);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
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

        [Fact]
        public async Task ScriptProcessor_Assign_Dynamic_To_Path_Spaced_With_DateTime_Unspecified()
        {
            // Arrange.
            var scope = new ExecutionScope();
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

            var addScript = _parser.Parse(addQuery, scope).Script;
            var updateScript = _parser.Parse(updateQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;

            scope = new ExecutionScope { Variables = { ["data"] = new ScopeVariable(data, "data") } };
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic addResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(updateScript, scope);
            dynamic updateResult = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
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
