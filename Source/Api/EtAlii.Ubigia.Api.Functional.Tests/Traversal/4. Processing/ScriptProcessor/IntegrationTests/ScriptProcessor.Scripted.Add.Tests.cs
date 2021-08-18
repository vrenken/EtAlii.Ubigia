// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    public class ScriptProcessorScriptedAddTests : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly FunctionalUnitTestContext _testContext;
        private readonly IScriptParser _parser;

        public ScriptProcessorScriptedAddTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = testContext.CreateScriptParser();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_Simple()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var continent = "Europe";
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQueries = new[]
            {
                $"/Location+={continent}",
                $"<= /Location/{continent}"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = $"<= /Location/{continent}";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic firstContinentEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic secondContinentEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstContinentEntry);
            Assert.NotNull(secondContinentEntry);
            Assert.Equal(((Node)firstContinentEntry).Id, ((Node)secondContinentEntry).Id);
            Assert.Equal(continent, firstContinentEntry.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_1()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var continent = "Europe";
            var country = "NL";
            var region = "Overijssel";
            var city = "Enschede";
            var location = "Helmerhoek";

            var addQueries = new[]
            {
                $"/Location+={continent}",
                $"/Location/{continent}+={country}",
                $"/Location/{continent}/{country}+={region}",
                $"/Location/{continent}/{country}/{region}+={city}",
                $"<= /Location/{continent}/{country}/{region}/{city}+={location}"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = $"<= /Location/{continent}/{country}/{region}/{city}/{location}";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic firstLocationEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic secondLocationEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstLocationEntry);
            Assert.NotNull(secondLocationEntry);
            Assert.Equal(((Node)firstLocationEntry).Id, ((Node)secondLocationEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_2()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "/Time+={0:yyyy}",
                "/Time/{0:yyyy}+={0:MM}",
                "/Time/{0:yyyy}/{0:MM}+={0:dd}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}+={0:HH}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}+={0:mm}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
            };

            var addQuery = string.Format(string.Join("\r\n", addQueries), now);
            var selectQuery = string.Format("<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic firstMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic secondMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstMinuteEntry);
            Assert.NotNull(secondMinuteEntry);
            Assert.Equal(((Node)firstMinuteEntry).Id, ((Node)secondMinuteEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_3()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "/Time+={0:yyyy}",
                "/Time/{0:yyyy}+={0:MM}",
                "/Time/{0:yyyy}/{0:MM}+={0:dd}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}+={0:HH}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}+={0:mm}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}"
            };

            var addQuery = string.Format(string.Join("\r\n", addQueries), now);
            var selectQuery = string.Format("/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic firstMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic secondMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstMinuteEntry);
            Assert.NotNull(secondMinuteEntry);
            Assert.Equal(((Node)firstMinuteEntry).Id, ((Node)secondMinuteEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_4()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "/Time+={0:yyyy}",
                "/Time/{0:yyyy}+={0:MM}",
                "/Time/{0:yyyy}/{0:MM}+={0:dd}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}+={0:HH}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}+={0:mm}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
            };

            var addQuery = string.Format(string.Join("\r\n", addQueries), now);
            var selectQuery = string.Format("<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic firstMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic secondMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstMinuteEntry);
            Assert.NotNull(secondMinuteEntry);
            Assert.Equal(((Node)firstMinuteEntry).Id, ((Node)secondMinuteEntry).Id);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_At_Once_1()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var continent = "Europe";
            var country = "NL";
            var region = "Overijssel";
            var city = "Enschede";
            var location = "Helmerhoek";

            var addQueries = new[]
            {
                $"<= /Location+={continent}/{country}/{region}/{city}/{location}"
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = $"<= /Location/{continent}/{country}/{region}/{city}/{location}";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic firstLocationEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic secondLocationEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstLocationEntry);
            Assert.NotNull(secondLocationEntry);
            Assert.Equal(((Node)firstLocationEntry).Id, ((Node)secondLocationEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_At_Once_2()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "/Time+={0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
            };

            var addQuery = string.Format(string.Join("\r\n", addQueries), now);
            var selectQuery = string.Format("<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic firstMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic secondMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstMinuteEntry);
            Assert.NotNull(secondMinuteEntry);
            Assert.Equal(((Node)firstMinuteEntry).Id, ((Node)secondMinuteEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_At_Once_3()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "/Time+={0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
                "/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
            };

            var addQuery = string.Format(string.Join("\r\n", addQueries), now);
            var selectQuery = string.Format("<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic firstMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic secondMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstMinuteEntry);
            Assert.NotNull(secondMinuteEntry);
            Assert.Equal(((Node)firstMinuteEntry).Id, ((Node)secondMinuteEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_At_Once_4()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var now = DateTime.Now;
            var addQueries = new[]
            {
                "/Time+={0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
                "<= /Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}",
            };

            var addQuery = string.Format(string.Join("\r\n", addQueries), now);
            var selectQuery = string.Format("/Time/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}", now);

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic firstMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic secondMinuteEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstMinuteEntry);
            Assert.NotNull(secondMinuteEntry);
            Assert.Equal(((Node)firstMinuteEntry).Id, ((Node)secondMinuteEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_Spaced_1()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var continent = "Europe";
            var country = "NL";
            var region = "Overijssel";
            var city = "Enschede";
            var location = "Helmerhoek";

            var addQueries = new[]
            {
                $"/Location +={continent}",
                $"/Location/{continent} +={country}",
                $"/Location/{continent}/{country} +={region}",
                $"/Location/{continent}/{country}/{region} +={city}",
                $"<= /Location/{continent}/{country}/{region}/{city} +={location}",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = $"/Location/{continent}/{country}/{region}/{city}/{location}";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic firstLocationEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic secondLocationEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstLocationEntry);
            Assert.NotNull(secondLocationEntry);
            Assert.Equal(((Node)firstLocationEntry).Id, ((Node)secondLocationEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Add_Spaced_2()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var continent = "Europe";
            var country = "NL";
            var region = "Overijssel";
            var city = "Enschede";
            var location = "Helmerhoek";

            var addQueries = new[]
            {
                $"/Location+= {continent}",
                $"/Location/{continent}+= {country}",
                $"/Location/{continent}/{country}+= {region}",
                $"/Location/{continent}/{country}/{region}+= {city}",
                $"<= /Location/{continent}/{country}/{region}/{city}+= {location}",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = $"/Location/{continent}/{country}/{region}/{city}/{location}";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic firstLocationEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic secondLocationEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstLocationEntry);
            Assert.NotNull(secondLocationEntry);
            Assert.Equal(((Node)firstLocationEntry).Id, ((Node)secondLocationEntry).Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Advanced_Add_Tree_At_Once()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQuery = "<= /Person+=Doe/John";
            var selectQuery = "/Person/Doe/John";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic firstJohnEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic secondJohnEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstJohnEntry);
            Assert.NotNull(secondJohnEntry);
            Assert.Equal(((Node)firstJohnEntry).Id, ((Node)secondJohnEntry).Id);

        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Advanced_Add_Tree_At_Once_From_Root_NonSpaced_01()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQuery = "+=Person/Doe/John";
            var selectQuery = "/Person/Doe/John";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic firstJohnEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic secondJohnEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.Null(firstJohnEntry);//, "First entry is not null")
            Assert.NotNull(secondJohnEntry);//, "Second entry is null")
            Assert.Equal("John", secondJohnEntry.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Advanced_Add_Tree_At_Once_From_Root_Spaced_02()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQuery = "+= Person/Doe/John";
            var selectQuery = "/Person/Doe/John";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic firstJohnEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic secondJohnEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.Null(firstJohnEntry);//, "First entry is not null")
            Assert.NotNull(secondJohnEntry);//, "Second entry is null")
            Assert.Equal("John", secondJohnEntry.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Advanced_Add_Tree_At_Once_From_Root_Wrong_Root()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQuery = "+=Person_Bad/Doe/John";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var act = processor.Process(addScript, scope);

            // Assert.
            await ObservableExceptionAssert.Throws<ScriptProcessingException, SequenceProcessingResult>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_Scripted_Advanced_Add_Tree_At_Once_Spaced()
        {
            // Arrange.
            var scope = new ExecutionScope();
            using var logicalContext = await _testContext.Logical.CreateLogicalContext(true).ConfigureAwait(false);
            var addQuery = "<= /Person += Doe/John";
            var selectQuery = "/Person/Doe/John";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalContext);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            dynamic firstJohnEntry = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic secondJohnEntry = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstJohnEntry);
            Assert.NotNull(secondJohnEntry);
            Assert.Equal(((Node)firstJohnEntry).Id, ((Node)secondJohnEntry).Id);
        }
    }
}
