// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Tests;
    using Xunit;
    using Xunit.Abstractions;

    public class SchemaProcessorQueriesSimpleTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private ITraversalContext _traversalContext;
        private IGraphContext _context;
        private readonly QueryingUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private FunctionalContextConfiguration _configuration;

        public SchemaProcessorQueriesSimpleTests(QueryingUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _configuration = new FunctionalContextConfiguration()
                .UseTestTraversalParser()
                .UseTestContextParser()
                .UseFunctionalGraphContextDiagnostics(TestConfiguration.Root);
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
        public Task SchemaProcessor_Create()
        {
            // Arrange.
            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(TestConfiguration.Root)
                .Use(scope)
                .Use(_traversalContext);

            // Act.
            var processor = new TestSchemaProcessorFactory().Create(configuration);

            // Assert.
            Assert.NotNull(processor);

            return Task.CompletedTask;
        }



        [Fact]
        public async Task SchemaProcessor_Query_Time_Now_By_Structure()
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
                .UseFunctionalDiagnostics(TestConfiguration.Root)
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
        public async Task SchemaProcessor_Query_Person_By_Structure()
        {
            // Arrange.
            var selectSchemaText = @"Person = @nodes(Person:Stark/Tony)
                                   {
                                        FirstName = @node()
                                        LastName = @node(\#FamilyName)
                                   }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(TestConfiguration.Root)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new TestSchemaProcessorFactory().Create(configuration);

            // Act.
            var results = await processor
                .Process(selectSchema)
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            var structure = results.SingleOrDefault();
            Assert.NotNull(structure);

            AssertValue("Tony", structure, "FirstName");
            AssertValue("Stark", structure, "LastName");
        }

        [Fact]
        public async Task SchemaProcessor_Query_Persons_By_Structure_02()
        {
            // Arrange.
            var selectSchemaText = @"Person = @nodes(Person:*/*)
                                   {
                                        FirstName = @node()
                                        LastName = @node(\#FamilyName)
                                   }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(TestConfiguration.Root)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new TestSchemaProcessorFactory().Create(configuration);

            // Act.
            var results = await processor
                .Process(selectSchema)
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(results);


            AssertValue("John", results[0], "FirstName");
            AssertValue("Doe", results[0], "LastName");

            AssertValue("Jane", results[1], "FirstName");
            AssertValue("Doe", results[1], "LastName");

            AssertValue("Tony", results[2], "FirstName");
            AssertValue("Stark", results[2], "LastName");

            AssertValue("Peter", results[3], "FirstName");
            AssertValue("Banner", results[3], "LastName");

            AssertValue("Tanja", results[4], "FirstName");
            AssertValue("Banner", results[4], "LastName");

            AssertValue("Arjan", results[5], "FirstName");
            AssertValue("Banner", results[5], "LastName");

            AssertValue("Ida", results[6], "FirstName");
            AssertValue("Banner", results[6], "LastName");

        }

        [Fact]
        public async Task SchemaProcessor_Query_Person_By_Values()
        {
            // Arrange.
            var selectSchemaText = @"Data
                                   {
                                        FirstName = @node(Person:Stark/Tony)
                                        LastName = @node(Person:Stark/Tony\#FamilyName)
                                   }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(TestConfiguration.Root)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new TestSchemaProcessorFactory().Create(configuration);

            // Act.
            var results = await processor
                .Process(selectSchema)
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            var structure = results.SingleOrDefault();
            Assert.NotNull(structure);

            AssertValue("Tony", structure, "FirstName");
            AssertValue("Stark", structure, "LastName");
        }


        [Fact]
        public async Task SchemaProcessor_Query_Person_Nested_By_Structure()
        {
            // Arrange.
            var selectSchemaText = @"Person = @nodes(Person:Stark/Tony)
                                    {
                                        Data
                                        {
                                            FirstName = @node()
                                            LastName = @node(\#FamilyName)
                                        }
                                    }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(TestConfiguration.Root)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new TestSchemaProcessorFactory().Create(configuration);

            // Act.
            var results = await processor
                .Process(selectSchema)
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            var structure = results.SingleOrDefault();
            Assert.NotNull(structure);
            structure = structure.Children.SingleOrDefault();
            Assert.NotNull(structure);

            AssertValue("Tony", structure, "FirstName");
            AssertValue("Stark", structure, "LastName");
        }

        [Fact]
        public async Task SchemaProcessor_Query_Person_Nested_Double_By_Structure()
        {
            // Arrange.
            var selectSchemaText = @"Person = @nodes(Person:Stark/Tony)
                                   {
                                        Data1
                                        {
                                            Data2
                                            {
                                                FirstName = @node()
                                                LastName = @node(\#FamilyName)
                                            }
                                        }
                                   }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(TestConfiguration.Root)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new TestSchemaProcessorFactory().Create(configuration);

            // Act.
            var results = await processor
                .Process(selectSchema)
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            var structure = results.SingleOrDefault();
            Assert.NotNull(structure);
            structure = structure.Children.SingleOrDefault();
            Assert.NotNull(structure);
            structure = structure.Children.SingleOrDefault();
            Assert.NotNull(structure);

            AssertValue("Tony", structure, "FirstName");
            AssertValue("Stark", structure, "LastName");
        }

        [Fact]
        public async Task SchemaProcessor_Query_Persons_By_Structure_01()
        {
            // Arrange.
            var selectSchemaText = @"Person = @nodes(Person:Doe/*)
                               {
                                    FirstName = @node()
                                    LastName = @node(\#FamilyName)
                                    NickName
                                    Birthdate
                               }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(TestConfiguration.Root)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new TestSchemaProcessorFactory().Create(configuration);

            // Act.
            var results = await processor
                .Process(selectSchema)
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.Equal(2, results.Length);

            var firstPerson = results[0];
            Assert.NotNull(firstPerson);
            AssertValue("John", firstPerson, "FirstName");
            AssertValue("Doe", firstPerson, "LastName");
            AssertValue(DateTime.Parse("1977-06-27"), firstPerson, "Birthdate");
            AssertValue("Johnny", firstPerson, "NickName");

            var secondPerson = results[1];
            Assert.NotNull(secondPerson);
            AssertValue("Jane", secondPerson, "FirstName");
            AssertValue("Doe", secondPerson, "LastName");
            AssertValue(DateTime.Parse("1970-02-03"), secondPerson, "Birthdate");
            AssertValue("Janey", secondPerson, "NickName");

        }

        [Fact]
        public async Task SchemaProcessor_Query_Person_Friends()
        {
            // Arrange.
            var selectSchemaText = @"Person = @nodes(Person:Doe/John)
                               {
                                    FirstName = @node()
                                    LastName = @node(\#FamilyName)
                                    NickName
                                    Birthdate
                                    Friends = @nodes(/Friends/)
                                    {
                                        FirstName = @node()
                                        LastName = @node(\#FamilyName)
                                    }
                               }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(TestConfiguration.Root)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new TestSchemaProcessorFactory().Create(configuration);

            // Act.
            var results = await processor
                .Process(selectSchema)
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.Single(results);

            var person = results[0];
            Assert.NotNull(person);
            AssertValue("John", person, "FirstName");
            AssertValue("Doe", person, "LastName");
            AssertValue(DateTime.Parse("1977-06-27"), person, "Birthdate");
            AssertValue("Johnny", person, "NickName");

            Assert.Equal(2, person.Children.Count);
            AssertValue("Tony", person.Children[0], "FirstName");
            AssertValue("Stark", person.Children[0], "LastName");
            AssertValue("Jane", person.Children[1], "FirstName");
            AssertValue("Doe", person.Children[1], "LastName");
        }

        private void AssertValue(object expected, Structure structure, string valueName)
        {
            var value = structure.Values.SingleOrDefault(v => v.Name == valueName);
            Assert.NotNull(value);
            Assert.Equal(expected, value.Object);

        }
    }
}
