namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptProcessorAssignDynamicLocationsIntegrationTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime
    {
        private readonly LogicalUnitTestContext _testContext;
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;

        public ScriptProcessorAssignDynamicLocationsIntegrationTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            _diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(_diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
            _logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
        }

        public Task DisposeAsync()
        {
            _parser = null;
            _logicalContext.Dispose();
            _logicalContext = null;
            return Task.CompletedTask;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Assign_Dynamic_To_Path()
        {
            // Arrange.
            var addQueries = new[]
            {
                "/Location+=Europe",
                "/Location/Europe+=NL",
                "/Location/Europe/NL+=Overijssel",
                "/Location/Europe/NL/Overijssel+=Enschede",
                "<= /Location/Europe/NL/Overijssel/Enschede+=Helmerhoek"
            };

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = "<= /Location/Europe/NL/Overijssel/Enschede/Helmerhoek <= $data";
            var selectQuery = "/Location/Europe/NL/Overijssel/Enschede/Helmerhoek";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables["data"] = new ScopeVariable(data, "data");
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

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
                "/Location+=Europe",
                "/Location/Europe+=NL",
                "/Location/Europe/NL+=Overijssel",
                "/Location/Europe/NL/Overijssel+=Enschede",
                "<= /Location/Europe/NL/Overijssel/Enschede+=Helmerhoek"
            };

            dynamic data = new
            {
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = "<= /Location/Europe/NL/Overijssel/Enschede/Helmerhoek <= $data";
            var selectQuery = "/Location/Europe/NL/Overijssel/Enschede/Helmerhoek";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables["data"] = new ScopeVariable(data, "data");
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

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
                "/Location += Europe",
                "/Location/Europe += NL",
                "/Location/Europe/NL += Overijssel",
                "/Location/Europe/NL/Overijssel += Enschede",
                "<= /Location/Europe/NL/Overijssel/Enschede += Helmerhoek"
            };

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = "<= /Location/Europe/NL/Overijssel/Enschede/Helmerhoek <= $data";
            var selectQuery = "/Location/Europe/NL/Overijssel/Enschede/Helmerhoek";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables["data"] = new ScopeVariable(data, "data");
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

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
                "/Location += Europe",
                "/Location/Europe += NL",
                "/Location/Europe/NL += Overijssel",
                "/Location/Europe/NL/Overijssel += Enschede",
                "<= /Location/Europe/NL/Overijssel/Enschede += Helmerhoek"
            };

            var dateTime = new DateTime(2016, 04, 10, 21, 21, 04, DateTimeKind.Local);

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
                DateTime = dateTime,
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = "<= /Location/Europe/NL/Overijssel/Enschede/Helmerhoek <= $data";
            var selectQuery = "/Location/Europe/NL/Overijssel/Enschede/Helmerhoek";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables["data"] = new ScopeVariable(data, "data");
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

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
                "/Location += Europe",
                "/Location/Europe += NL",
                "/Location/Europe/NL += Overijssel",
                "/Location/Europe/NL/Overijssel += Enschede",
                "<= /Location/Europe/NL/Overijssel/Enschede += Helmerhoek"
            };

            var dateTime = new DateTime(2016, 04, 10, 21, 21, 04, DateTimeKind.Utc);

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
                DateTime = dateTime,
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = "<= /Location/Europe/NL/Overijssel/Enschede/Helmerhoek <= $data";
            var selectQuery = "/Location/Europe/NL/Overijssel/Enschede/Helmerhoek";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables["data"] = new ScopeVariable(data, "data");
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

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
                "/Location += Europe",
                "/Location/Europe += NL",
                "/Location/Europe/NL += Overijssel",
                "/Location/Europe/NL/Overijssel += Enschede",
                "<= /Location/Europe/NL/Overijssel/Enschede += Helmerhoek"
            };

            var dateTime = new DateTime(2016, 04, 10, 21, 21, 04, DateTimeKind.Unspecified);

            dynamic data = new
            {
                StringValue = "Test1",
                IntValue = 12,
                DateTime = dateTime,
            };

            var addQuery = String.Join("\r\n", addQueries);
            var updateQuery = "<= /Location/Europe/NL/Overijssel/Enschede/Helmerhoek <= $data";
            var selectQuery = "/Location/Europe/NL/Overijssel/Enschede/Helmerhoek";

            var addScript = _parser.Parse(addQuery).Script;
            var updateScript = _parser.Parse(updateQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            scope.Variables["data"] = new ScopeVariable(data, "data");
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(_logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

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