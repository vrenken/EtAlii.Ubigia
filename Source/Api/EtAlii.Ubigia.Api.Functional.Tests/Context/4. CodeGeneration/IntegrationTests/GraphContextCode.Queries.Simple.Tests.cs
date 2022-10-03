// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;
    using Xunit.Abstractions;
    using EtAlii.Ubigia.Api.Functional.Context.Tests.Model;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class GraphContextCodeQueriesSimpleTests : IClassFixture<FunctionalUnitTestContext>, IAsyncLifetime
    {
        private IGraphContext _context;
        private readonly FunctionalUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private FunctionalOptions _options;
        private int _test;

        public GraphContextCodeQueriesSimpleTests(FunctionalUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
        }

        public async Task InitializeAsync()
        {
            var initialize = Environment.TickCount;

            _options = await new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseDiagnostics()
                .UseLogicalContext(_testContext, true)
                .ConfigureAwait(false);

            var (graphContext, traversalContext) = _testContext.CreateComponent<IGraphContext, ITraversalContext>(_options);
            _context = graphContext;

            var scope = new ExecutionScope();
            await _testContext.Functional
                .AddPeople(traversalContext, scope)
                .ConfigureAwait(false);
            await _testContext.Functional
                .AddAddresses(traversalContext, scope)
                .ConfigureAwait(false);

            _testOutputHelper.WriteLine($"Initialize: {TimeSpan.FromTicks(Environment.TickCount - initialize).TotalMilliseconds}ms");

            _test = Environment.TickCount;
        }

        public async Task DisposeAsync()
        {
            _testOutputHelper.WriteLine($"Test: {TimeSpan.FromTicks(Environment.TickCount - _test).TotalMilliseconds}ms");

            var dispose = Environment.TickCount;

            await _options.LogicalContext
                .DisposeAsync()
                .ConfigureAwait(false);
            _options = null;
            _context = null;

            _testOutputHelper.WriteLine($"Dispose: {TimeSpan.FromTicks(Environment.TickCount - dispose).TotalMilliseconds}ms");
        }

        [Fact]
        public async Task GraphContextCode_Query_Time_Now_By_Structure()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var queryText = @"Time = @node(time:now)
                               {
                                    Millisecond = @node()
                                    Second = @node(\)
                                    Minute = @node(\\)
                                    Hour = @node(\\\)
                                    Day = @node(\\\\)
                                    Month = @node(\\\\\)
                                    Year = @node(\\\\\\)
                               }";

            var query = _context.Parse(queryText, scope).Schema;

            var functionalOptions = new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseDiagnostics()
                .UseLogicalContext(_options.LogicalContext);
            var processor = _testContext.CreateSchemaProcessor(functionalOptions);

            // Act.
            var results = await processor
                .Process(query, scope)
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            var structure = results.SingleOrDefault();
            Assert.NotNull(structure);

            void AssertTimeValue(string valueName)
            {
                var value = structure.Values.SingleOrDefault(v => v.Name == valueName);
                Assert.NotNull(value);
                Assert.IsType<string>(value.Object);
                Assert.True(((string)value.Object).All(char.IsDigit));
            }

            AssertTimeValue("Millisecond");
            AssertTimeValue("Second");
            AssertTimeValue("Minute");
            AssertTimeValue("Hour");
            AssertTimeValue("Day");
            AssertTimeValue("Month");
            AssertTimeValue("Year");
        }

        [Fact]
        public async Task GraphContextCode_Query_Person_By_Structure()
        {
            // Arrange.
            var processor = _testContext.CreateSchemaProcessor(_options);
            var parser = _testContext.CreateSchemaParser();
            var context = new GraphContext(processor, parser);

            // Act.
            var person = await context
                .ProcessTonyStarkPerson()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(person);

            Assert.Equal("Tony", person.FirstName);
            Assert.Equal("Stark", person.LastName);
        }

        [Fact]
        public async Task GraphContextCode_Query_Persons_By_Structure_02()
        {
            // Arrange.
            var processor = _testContext.CreateSchemaProcessor(_options);
            var parser = _testContext.CreateSchemaParser();
            var context = new GraphContext(processor, parser);

            // Act.
            var persons = await context
                .ProcessAllPersons()
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(persons);


            Assert.Equal("John", persons[0].FirstName);
            Assert.Equal("Doe", persons[0].LastName);

            Assert.Equal("Jane", persons[1].FirstName);
            Assert.Equal("Doe", persons[1].LastName);

            Assert.Equal("Tony", persons[2].FirstName);
            Assert.Equal("Stark", persons[2].LastName);

            Assert.Equal("Peter", persons[3].FirstName);
            Assert.Equal("Banner", persons[3].LastName);

            Assert.Equal("Tanja", persons[4].FirstName);
            Assert.Equal("Banner", persons[4].LastName);

            Assert.Equal("Arjan", persons[5].FirstName);
            Assert.Equal("Banner", persons[5].LastName);

            Assert.Equal("Ida", persons[6].FirstName);
            Assert.Equal("Banner", persons[6].LastName);
        }

        [Fact]
        public async Task GraphContextCode_Query_Person_By_Values()
        {
            // Arrange.
            var processor = _testContext.CreateSchemaProcessor(_options);
            var parser = _testContext.CreateSchemaParser();
            var context = new GraphContext(processor, parser);

            // Act.
            var data = await context
                .ProcessTonyStarkData()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(data);

            Assert.Equal("Tony", data.FirstName);
            Assert.Equal("Stark", data.LastName);
        }


        [Fact]
        public async Task GraphContextCode_Query_Person_Nested_By_Structure()
        {
            // Arrange.
            var processor = _testContext.CreateSchemaProcessor(_options);
            var parser = _testContext.CreateSchemaParser();
            var context = new GraphContext(processor, parser);

            // Act.
            var person = await context
                .ProcessTonyStarkNestedData()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(person);
            var data = person.Data;
            Assert.NotNull(data);

            Assert.Equal("Tony", data.FirstName);
            Assert.Equal("Stark", data.LastName);
        }

        [Fact]
        public async Task GraphContextCode_Query_Person_Nested_Double_By_Structure()
        {
            // Arrange.
            var processor = _testContext.CreateSchemaProcessor(_options);
            var parser = _testContext.CreateSchemaParser();
            var context = new GraphContext(processor, parser);

            // Act.
            var person = await context
                .ProcessTonyStarkNestedTwiceData()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(person);
            Assert.NotNull(person.Data1);
            Assert.NotNull(person.Data1.Data2);
            var data2 = person.Data1.Data2;
            Assert.Equal("Tony", data2.FirstName);
            Assert.Equal("Stark", data2.LastName);
        }


        [Fact]
        public async Task GraphContextCode_Query_Persons_By_Structure_01()
        {
            // Arrange.
            var processor = _testContext.CreateSchemaProcessor(_options);
            var parser = _testContext.CreateSchemaParser();
            var context = new GraphContext(processor, parser);

            // Act.
            var items = await context
                .ProcessAllDoes()
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.Equal(2, items.Length);

            var firstPerson = items[0];
            Assert.NotNull(firstPerson);
            Assert.Equal("John", firstPerson.FirstName);
            Assert.Equal("Doe", firstPerson.LastName);
            Assert.Equal(DateTime.Parse("1977-06-27"), firstPerson.Birthdate);
            Assert.Equal("Johnny", firstPerson.NickName);

            var secondPerson = items[1];
            Assert.NotNull(secondPerson);
            Assert.Equal("Jane", secondPerson.FirstName);
            Assert.Equal("Doe", secondPerson.LastName);
            Assert.Equal(DateTime.Parse("1970-02-03"), secondPerson.Birthdate);
            Assert.Equal("Janey", secondPerson.NickName);

        }

        [Fact]
        public async Task GraphContextCode_Query_Person_Friends()
        {
            // Arrange.
            var processor = _testContext.CreateSchemaProcessor(_options);
            var parser = _testContext.CreateSchemaParser();
            var context = new GraphContext(processor, parser);

            // Act.
            var person = await context
                .ProcessJohnDoeWithFriends()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(person);

            Assert.NotNull(person);
            Assert.Equal("John", person.FirstName);
            Assert.Equal("Doe", person.LastName);
            Assert.Equal(DateTime.Parse("1977-06-27"), person.Birthdate);
            Assert.Equal("Johnny", person.NickName);

            Assert.Equal(2, person.Friends.Length);
            Assert.Equal("Tony", person.Friends[0].FirstName);
            Assert.Equal("Stark", person.Friends[0].LastName);
            Assert.Equal("Jane", person.Friends[1].FirstName);
            Assert.Equal("Doe", person.Friends[1].LastName);
        }
    }
}
