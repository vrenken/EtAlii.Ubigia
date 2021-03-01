﻿namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;
    using Xunit.Abstractions;
    using EtAlii.Ubigia.Api.Functional.Context.Tests.Model;

    public class CodeSchemaProcessorQueriesSimpleTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private ITraversalContext _traversalContext;
        private IGraphContext _context;
        private readonly QueryingUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private IDiagnosticsConfiguration _diagnostics;
        private GraphContextConfiguration _configuration;

        public CodeSchemaProcessorQueriesSimpleTests(QueryingUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _diagnostics = _testContext.FunctionalTestContext.Diagnostics;
            _configuration = new GraphContextConfiguration()
                .UseTestTraversalParser()
                .UseTestContextParser()
                .UseFunctionalGraphContextDiagnostics(_testContext.FunctionalTestContext.Diagnostics);
            await _testContext.FunctionalTestContext.ConfigureLogicalContextConfiguration(_configuration,true).ConfigureAwait(false);

            _traversalContext = new TraversalContextFactory().Create(_configuration);
            _context = new GraphContextFactory().Create(_configuration);

            await _testContext.FunctionalTestContext.AddPeople(_traversalContext).ConfigureAwait(false);
            await _testContext.FunctionalTestContext.AddAddresses(_traversalContext).ConfigureAwait(false);

            _testOutputHelper.WriteLine("{1}.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphContext));
        }

        public async Task DisposeAsync()
        {
            var start = Environment.TickCount;

            await _configuration.Connection.Close().ConfigureAwait(false);
            _configuration = null;
            _traversalContext = null;
            _context = null;

            _testOutputHelper.WriteLine("{1}.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphContext));
        }

        [Fact]
        public async Task SchemaCodeProcessor_Query_Time_Now_By_Structure()
        {
            // Arrange.
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

            var query = _context.Parse(queryText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new TestSchemaProcessorFactory().Create(configuration);

            // Act.
            var results = await processor
                .Process(query)
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
        public async Task SchemaCodeProcessor_Query_Person_By_Structure()
        {
            // Arrange.
            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new TestSchemaProcessorFactory().Create(configuration);

            // Act.
            var person = await processor
                .ProcessTonyStarkPerson()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(person);

            Assert.Equal("Tony", person.FirstName);
            Assert.Equal("Stark", person.LastName);
        }

        [Fact]
        public async Task SchemaCodeProcessor_Query_Persons_By_Structure_02()
        {
            // Arrange.
            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new TestSchemaProcessorFactory().Create(configuration);

            // Act.
            var persons = await processor
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
        public async Task SchemaCodeProcessor_Query_Person_By_Values()
        {
            // Arrange.
            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new TestSchemaProcessorFactory().Create(configuration);

            // Act.
            var data = await processor
                .ProcessTonyStarkData()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(data);

            Assert.Equal("Tony", data.FirstName);
            Assert.Equal("Stark", data.LastName);
        }


        [Fact]
        public async Task SchemaCodeProcessor_Query_Person_Nested_By_Structure()
        {
            // Arrange.
            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new TestSchemaProcessorFactory().Create(configuration);

            // Act.
            var person = await processor
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
        public async Task SchemaCodeProcessor_Query_Person_Nested_Double_By_Structure()
        {
            // Arrange.
            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new TestSchemaProcessorFactory().Create(configuration);

            // Act.
            var person = await processor
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
        public async Task SchemaCodeProcessor_Query_Persons_By_Structure_01()
        {
            // Arrange.
            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new TestSchemaProcessorFactory().Create(configuration);

            // Act.
            var items = await processor
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
        public async Task SchemaCodeProcessor_Query_Person_Friends()
        {
            // Arrange.
            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new TestSchemaProcessorFactory().Create(configuration);

            // Act.
            var person = await processor
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
