// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    public sealed class ScriptProcessorLogicalAddTests : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly FunctionalUnitTestContext _testContext;
        private readonly IScriptParser _parser;

        public ScriptProcessorLogicalAddTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = testContext.CreateScriptParser();
        }

        [Fact]
        public async Task ScriptProcessor_Logical_Location_Add()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var locationPath = await _testContext.Logical
                .AddContinentCountryRegionCityLocation(logicalOptions)
                .ConfigureAwait(false);
            var selectQuery = $"<= {locationPath}";
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(selectScript, scope);
            dynamic locationEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(locationEntry);
            Assert.NotEqual(Identifier.Empty, ((Node)locationEntry).Id);
            Assert.Equal(selectQuery.Split(new[] { '/' }).Last(), locationEntry.ToString());
        }

        [Fact]
        public async Task ScriptProcessor_Logical_Get_Time()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var locationPath = await _testContext.Logical
                .AddContinentCountryRegionCityLocation(logicalOptions)
                .ConfigureAwait(false);
            var selectQuery = $"<= {locationPath}";
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(selectScript, scope);
            dynamic locationEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(locationEntry);
            Assert.NotEqual(Identifier.Empty, ((Node)locationEntry).Id);
            Assert.Equal(selectQuery.Split(new[] { '/' }).Last(), locationEntry.ToString());
        }


        [Fact]
        public async Task ScriptProcessor_Logical_Time_Add_With_Variable()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var locationPath = await _testContext.Logical
                .AddContinentCountryRegionCityLocation(logicalOptions)
                .ConfigureAwait(false);
            var selectQuery = $"<= {locationPath}";
            var selectQueryParts = selectQuery.Split(new[] {'/'});
            var variable = selectQueryParts[3];
            selectQueryParts[3] = "$variable";
            selectQuery = string.Join("/", selectQueryParts);
            selectQuery = $"$variable <= \"{variable}\"\r\n{selectQuery}";
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(selectScript, scope);
            dynamic locationEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(locationEntry);
            Assert.NotEqual(Identifier.Empty, ((Node)locationEntry).Id);
            Assert.Equal(selectQuery.Split(new[] { '/' }).Last(), locationEntry.ToString());
        }



        [Fact]
        public async Task ScriptProcessor_Logical_Add_1()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            var root = await _testContext
                .GetRoot(logicalOptions, "Person")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalOptions, root.Identifier, scope)
                .ConfigureAwait(false);
            await _testContext.Logical
                .CreateHierarchy(logicalOptions, (IEditableEntry)entry, "LastName")
                .ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            var lastSequence = await processor.Process(selectScript, scope);
            dynamic beforeResult = await lastSequence.Output.FirstOrDefaultAsync();

            // Act.
            var parseResult = _parser.Parse("/Person/LastName += SurName", scope);
            var addScript = parseResult.Script;
            lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic afterResult = await lastSequence.Output.FirstOrDefaultAsync();

            // Assert.
            Assert.False(parseResult.Errors.Any(), parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Null(beforeResult);
            Assert.NotNull(afterResult);
            Assert.NotEqual(Identifier.Empty, ((Node)afterResult).Id);
        }


        [Fact]
        public async Task ScriptProcessor_Logical_Add_2()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            var root = await _testContext
                .GetRoot(logicalOptions, "Person")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalOptions, root.Identifier, scope)
                .ConfigureAwait(false);
            await _testContext.Logical
                .CreateHierarchy(logicalOptions, (IEditableEntry)entry, "LastName")
                .ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            var lastSequence = await processor.Process(selectScript, scope);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Act.
            var addScript = _parser.Parse("/Person/LastName += \"SurName\"", scope).Script;
            lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);

            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.Null(beforeResult);
            Assert.NotNull(afterResult);
            Assert.NotEqual(Identifier.Empty, ((Node)afterResult).Id);
        }

        [Fact]
        public async Task ScriptProcessor_Logical_Add_Empty_Item_2()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            var root = await _testContext
                .GetRoot(logicalOptions, "Person")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalOptions, root.Identifier, scope)
                .ConfigureAwait(false);
            await _testContext.Logical
                .CreateHierarchy(logicalOptions, (IEditableEntry)entry, "LastName")
                .ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            var lastSequence = await processor.Process(selectScript, scope);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();
            var addScript = _parser.Parse("/Person/LastName += \"\"", scope).Script;

            // Act.
            var act = processor.Process(addScript, scope);

            // Assert.
            Assert.Null(beforeResult);
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task ScriptProcessor_Logical_Add_3()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            var root = await _testContext
                .GetRoot(logicalOptions, "Person")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalOptions, root.Identifier, scope)
                .ConfigureAwait(false);
            await _testContext.Logical
                .CreateHierarchy(logicalOptions, (IEditableEntry)entry, "LastName")
                .ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            var lastSequence = await processor.Process(selectScript, scope);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Act.
            var addScript = _parser.Parse("/Person/LastName += SurName", scope).Script;
            lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);

            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.Null(beforeResult);
            Assert.NotNull(afterResult);
            Assert.NotEqual(Identifier.Empty, ((Node)afterResult).Id);
        }


        [Fact]
        public async Task ScriptProcessor_Logical_Add_4()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            var root = await _testContext
                .GetRoot(logicalOptions, "Person")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalOptions, root.Identifier, scope)
                .ConfigureAwait(false);
            await _testContext.Logical
                .CreateHierarchy(logicalOptions, (IEditableEntry)entry, "LastName")
                .ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            var lastSequence = await processor.Process(selectScript, scope);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Act.
            var addScript = _parser.Parse("/Person/LastName += \"SurName\"", scope).Script;
            lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);

            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.Null(beforeResult);
            Assert.NotNull(afterResult);
            Assert.NotEqual(Identifier.Empty, ((Node)afterResult).Id);
        }

        [Fact]
        public async Task ScriptProcessor_Logical_Add_With_Variable_1()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            var root = await _testContext
                .GetRoot(logicalOptions, "Person")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalOptions, root.Identifier, scope)
                .ConfigureAwait(false);
            await _testContext.Logical
                .CreateHierarchy(logicalOptions, (IEditableEntry)entry, "LastName")
                .ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            scope = new ExecutionScope();
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            var lastSequence = await processor.Process(selectScript, scope);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Act.
            var parseResult = _parser.Parse("/Person/LastName += $var", scope);
            var addScript = parseResult.Script;
            scope.Variables["var"] = new ScopeVariable("SurName", "Variable");
            lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);

            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.False(parseResult.Errors.Any(), parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Null(beforeResult);
            Assert.NotNull(afterResult);
            Assert.NotEqual(Identifier.Empty, ((Node)afterResult).Id);
        }

        [Fact]
        public async Task ScriptProcessor_Logical_Add_With_Variable_2()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            var root = await _testContext
                .GetRoot(logicalOptions, "Person")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalOptions, root.Identifier, scope)
                .ConfigureAwait(false);
            await _testContext.Logical
                .CreateHierarchy(logicalOptions, (IEditableEntry)entry, "LastName")
                .ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            scope = new ExecutionScope();
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            var lastSequence = await processor.Process(selectScript, scope);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();
            var parseResult = _parser.Parse("$var <= \"SurName\"\r\n/Person/LastName += $var", scope);
            var addScript = parseResult.Script;
            scope.Variables["var"] = new ScopeVariable("SurName", "Variable");

            // Act.
            lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.False(parseResult.Errors.Any(), parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Null(beforeResult);
            Assert.NotNull(afterResult);
            Assert.NotEqual(Identifier.Empty, ((Node)afterResult).Id);
        }

        [Fact]
        public async Task ScriptProcessor_Logical_Add_With_Variable_3()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);

            var root = await _testContext
                .GetRoot(logicalOptions, "Person")
                .ConfigureAwait(false);
            var entry = await _testContext
                .GetEntry(logicalOptions, root.Identifier, scope)
                .ConfigureAwait(false);
            await _testContext.Logical
                .CreateHierarchy(logicalOptions, (IEditableEntry)entry, "LastNameOriginal","SurName")
                .ConfigureAwait(false);
            await _testContext.Logical
                .CreateHierarchy(logicalOptions, (IEditableEntry)entry, "LastName")
                .ConfigureAwait(false);
            var selectQuery = "<= /Person/LastName/";
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            scope = new ExecutionScope();
            var processor = _testContext.CreateScriptProcessor(logicalOptions);
            var lastSequence = await processor.Process(selectScript, scope);
            dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();
            var parseResult = _parser.Parse("$var <= /Person/LastNameOriginal/SurName\r\n/Person/LastName += $var", scope);
            var addScript = parseResult.Script;
            scope.Variables["var"] = new ScopeVariable("SurName", "Variable");

            // Act.
            lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.False(parseResult.Errors.Any(), parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Null(beforeResult);
            Assert.NotNull(afterResult);
            Assert.NotEqual(Identifier.Empty, ((Node)afterResult).Id);
        }
    }
}
