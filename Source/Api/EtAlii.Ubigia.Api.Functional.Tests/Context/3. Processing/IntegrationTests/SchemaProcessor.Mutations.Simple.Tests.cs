// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;
    using Xunit.Abstractions;

    public class SchemaProcessorMutationsSimpleTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private ITraversalContext _traversalContext;
        private IGraphContext _context;
        private readonly QueryingUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private IDiagnosticsConfiguration _diagnostics;
        private FunctionalContextConfiguration _configuration;

        public SchemaProcessorMutationsSimpleTests(QueryingUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _diagnostics = _testContext.FunctionalTestContext.Diagnostics;
            _configuration = new FunctionalContextConfiguration()
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
        public async Task SchemaProcessor_Mutate_Person_01()
        {
            // Arrange.
            var mutationText = @"Person = @node(Person:Doe/John)
                               {
                                    Weight = 160.1,
                                    NickName = ""HeavyJohnny""
                               }";
            var mutationSchema = _context.Parse(mutationText).Schema;

            var queryText = @"Person = @node(Person:Doe/John)
                              {
                                    Weight,
                                    NickName
                              }";
            var querySchema = _context.Parse(queryText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new LapaSchemaProcessorFactory().Create(configuration);

            // Act.
            var mutationResults = await processor
                .Process(mutationSchema)
                .ToArrayAsync()
                .ConfigureAwait(false);
            var queryResults = await processor
                .Process(querySchema)
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            var mutationStructure = mutationResults.Single();
            Assert.NotNull(mutationStructure);
            var queryStructure = queryResults.Single();
            Assert.NotNull(queryStructure);

            AssertValue(160.1f, mutationStructure, "Weight");
            AssertValue(160.1f, queryStructure, "Weight");

            AssertValue("HeavyJohnny", mutationStructure, "NickName");
            AssertValue("HeavyJohnny", queryStructure, "NickName");
        }

        [Fact]
        public async Task SchemaProcessor_Mutate_Person_02()
        {
            // Arrange.
            var mutationText = @"Person = @node-add(Person:Doe, Mary)
                               {
                                    Weight = 160.1,
                                    NickName = ""MinteyMary""
                               }";
            var mutationSchema = _context.Parse(mutationText).Schema;

            var queryText = @"Person = @node(Person:Doe/Mary)
                              {
                                    Weight,
                                    NickName
                              }";
            var querySchema = _context.Parse(queryText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new LapaSchemaProcessorFactory().Create(configuration);

            // Act.
            var mutationResults = await processor
                .Process(mutationSchema)
                .ToArrayAsync()
                .ConfigureAwait(false);
            var queryResults = await processor
                .Process(querySchema)
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            var mutationStructure = mutationResults.Single();
            Assert.NotNull(mutationStructure);
            var queryStructure = queryResults.Single();
            Assert.NotNull(queryStructure);

            AssertValue(160.1f, mutationStructure, "Weight");
            AssertValue(160.1f, queryStructure, "Weight");

            AssertValue("MinteyMary", mutationStructure, "NickName");
            AssertValue("MinteyMary", queryStructure, "NickName");
        }

        [Fact(Skip = "Skipped - Should be made working though.")]
        public async Task SchemaProcessor_Mutate_Person_03()
        {
            // Arrange.
            var mutationText = @"Person = @node(Person:Doe/John)
                                 {
                                     FirstName = @node(),
                                     LastName = @node(\#FamilyName),
                                     NickName,
                                     Friends[] = @nodes-link(/Friends, Person:Banner/Peter, /Friends)
                                     {
                                        FirstName = @node(),
                                        LastName = @node(\#FamilyName),
                                        NickName
                                     }
                                 }";
            var mutationSchema = _context.Parse(mutationText).Schema;

            var queryText = @"Person = @node(Person:Doe/John)
                              {
                                    FirstName = @node(),
                                    LastName = @node(\#FamilyName),
                                    NickName
                                    Friends[] = @nodes(/Friends/)
                                    {
                                        FirstName = @node(),
                                        LastName = @node(\#FamilyName),
                                        NickName
                                    }
                              }";
            var querySchema = _context.Parse(queryText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new LapaSchemaProcessorFactory().Create(configuration);

            // Act.
            var mutationResults = await processor
                .Process(mutationSchema)
                .ToArrayAsync()
                .ConfigureAwait(false);
            var queryResults = await processor
                .Process(querySchema)
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            var mutationStructure = mutationResults.Single();
            Assert.NotNull(mutationStructure);
            var queryStructure = queryResults.Single();
            Assert.NotNull(queryStructure);

            AssertValue("John", mutationStructure, "FirstName");
            AssertValue("John", queryStructure, "FirstName");

            AssertValue("Doe", mutationStructure, "LastName");
            AssertValue("Doe", queryStructure, "LastName");

            AssertValue("Johnny", mutationStructure, "NickName");
            AssertValue("Johnny", queryStructure, "NickName");

            void AssertFriends(Structure[] friends)
            {
                Assert.NotNull(friends);
                Assert.Equal(3, friends.Length);
                AssertValue("Tony", friends[0], "FirstName");
                AssertValue("Stark", friends[0], "LastName");
                AssertValue("Iron Man", friends[0], "NickName");
                AssertValue("Jane", friends[1], "FirstName");
                AssertValue("Doe", friends[1], "LastName");
                AssertValue("Janey", friends[1], "NickName");
                AssertValue("Peter", friends[2], "FirstName");
                AssertValue("Banner", friends[2], "LastName");
                AssertValue("Pete", friends[2], "NickName");
            }
            var mutationFriends = mutationStructure.Children.Where(c => c.Type == "Friends").ToArray();
            AssertFriends(mutationFriends);
            var queryFriends = queryStructure.Children.Where(c => c.Type == "Friends").ToArray();
            AssertFriends(queryFriends);
        }

        [Fact]
        public async Task SchemaProcessor_Mutate_Persons_01()
        {
            // Arrange.
            var mutationText = @"Person = @nodes-add(Person:Doe, Mary)
                               {
                                    Weight = 160.1,
                                    NickName = ""MinteyMary""
                               }";
            var mutationSchema = _context.Parse(mutationText).Schema;

            var queryText = @"Person[] = @nodes(Person:Doe/)
                              {
                                    Weight,
                                    NickName
                              }";
            var querySchema = _context.Parse(queryText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new LapaSchemaProcessorFactory().Create(configuration);

            // Act.
            var mutationResults = await processor
                .Process(mutationSchema)
                .ToArrayAsync()
                .ConfigureAwait(false);
            var queryResults = await processor
                .Process(querySchema)
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            var mutationStructures = mutationResults;
            Assert.NotNull(mutationStructures);
            var queryStructures = queryResults;
            Assert.NotNull(queryStructures);
            Assert.Single(mutationStructures);
            Assert.Equal(3, queryStructures.Length);

            var mutationStructure = mutationStructures.Single(s => s.Name == "Mary");
            var queryStructure = queryStructures.Single(s => s.Name == "Mary");

            AssertValue(160.1f, mutationStructure, "Weight");
            AssertValue(160.1f, queryStructure, "Weight");

            AssertValue("MinteyMary", mutationStructure, "NickName");
            AssertValue("MinteyMary", queryStructure, "NickName");
        }


        [Fact(Skip = "Skipped - Should be made working though.")]
        public async Task SchemaProcessor_Mutate_Person_Friends()
        {
            // Arrange.
            var mutationText = @"Person = @nodes(Person:Doe/John)
                               {
                                    FirstName = @node()
                                    LastName = @node(\#FamilyName)
                                    NickName
                                    Birthdate
                                    Friends[] = @nodes-link(/Friends, Person:Banner/Peter, /Friends)
                                    {
                                        FirstName = @node()
                                        LastName = @node(\#FamilyName)
                                    }
                               }";

            var mutationSchema = _context.Parse(mutationText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new LapaSchemaProcessorFactory().Create(configuration);

            // Act.
            var results = await processor
                .Process(mutationSchema)
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

            Assert.Equal(3, person.Children.Count);
            AssertValue("Tony", person.Children[0], "FirstName");
            AssertValue("Stark", person.Children[0], "LastName");
            AssertValue("Jane", person.Children[1], "FirstName");
            AssertValue("Doe", person.Children[1], "LastName");
            AssertValue("Peter", person.Children[2], "FirstName");
            AssertValue("Banner", person.Children[2], "LastName");
        }


        [Fact(Skip = "Skipped - Should be made working though.")]
        public async Task SchemaProcessor_Mutate_Person_Friends_Bidirectional_01()
        {
            // Arrange.
            var mutationText = @"Data
                                 {
                                     Person = @node(Person:Doe/John)
                                     {
                                         FirstName = @node(),
                                         LastName = @node(\#FamilyName),
                                         NickName,
                                         Friends[] = @nodes-link(/Friends, Person:Banner/Peter, /Friends)
                                         {
                                            FirstName = @node(),
                                            LastName = @node(\#FamilyName),
                                            NickName
                                         }
                                     }
                                 }";

            var mutationSchema = _context.Parse(mutationText).Schema;

            var queryText = @"Data
                              {
                                    Person1 = @node(Person:Doe/John)
                                    {
                                        FirstName = @node(),
                                        LastName = @node(\#FamilyName),
                                        NickName
                                        Friends[] = @nodes(/Friends/)
                                        {
                                            FirstName = @node(),
                                            LastName = @node(\#FamilyName),
                                            NickName
                                        }
                                    },
                                    Person2 = @node(Person:Banner/Peter)
                                    {
                                        FirstName = @node(),
                                        LastName = @node(\#FamilyName),
                                        NickName
                                        Friends[] = @nodes(/Friends/)
                                        {
                                            FirstName = @node(),
                                            LastName = @node(\#FamilyName),
                                            NickName
                                        }
                                    }
                              }";
            var querySchema = _context.Parse(queryText).Schema;


            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_traversalContext);
            var processor = new LapaSchemaProcessorFactory().Create(configuration);

            // Act.
            var mutationResults = await processor
                .Process(mutationSchema)
                .ToArrayAsync()
                .ConfigureAwait(false);

            var queryResults = await processor
                .Process(querySchema)
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.Single(mutationResults);
            var mutationPeople = mutationResults[0].Children;
            Assert.Equal(2, mutationPeople.Count);
            var mutationPerson1 = mutationPeople[0];
            Assert.NotNull(mutationPerson1);
            var mutationPerson2 = mutationPeople[1];
            Assert.NotNull(mutationPerson2);

            Assert.Single(queryResults);
            var queryPeople = queryResults[0].Children;
            Assert.Equal(2, queryPeople.Count);
            var queryPerson1 = queryPeople[0];
            Assert.NotNull(queryPerson1);
            var queryPerson2 = queryPeople[1];
            Assert.NotNull(queryPerson2);


            void AssertJohnDoeFriends(Structure[] friends)
            {
                Assert.NotNull(friends);
                Assert.Equal(3, friends.Length);
                AssertValue("Tony", friends[0], "FirstName");
                AssertValue("Stark", friends[0], "LastName");
                AssertValue("Iron Man", friends[0], "NickName");
                AssertValue("Jane", friends[1], "FirstName");
                AssertValue("Doe", friends[1], "LastName");
                AssertValue("Janey", friends[1], "NickName");
                AssertValue("Peter", friends[2], "FirstName");
                AssertValue("Banner", friends[2], "LastName");
                AssertValue("Pete", friends[2], "NickName");
            }

            void AssertPeterBannerFriends(Structure[] friends)
            {
                Assert.NotNull(friends);
                Assert.Equal(5, friends.Length);
                AssertValue("Tanja", friends[0], "FirstName");
                AssertValue("Banner", friends[0], "LastName");
                AssertValue("LadyL", friends[0], "NickName");

                AssertValue("Arjan", friends[1], "FirstName");
                AssertValue("Banner", friends[1], "LastName");
                AssertValue("Bengel", friends[1], "NickName");

                AssertValue("Ida", friends[2], "FirstName");
                AssertValue("Banner", friends[2], "LastName");
                AssertValue("Scheetje", friends[2], "NickName");

                AssertValue("Tony", friends[3], "FirstName");
                AssertValue("Stark", friends[3], "LastName");
                AssertValue("Iron Man", friends[3], "NickName");

                AssertValue("John", friends[4], "FirstName");
                AssertValue("Doe", friends[4], "LastName");
                AssertValue("Johnny", friends[4], "NickName");
            }

            AssertValue("John", mutationPerson1, "FirstName");
            AssertValue("Doe", mutationPerson1, "LastName");
            AssertValue("Johnny", mutationPerson1, "NickName");
            var mutationFriends1 = mutationPerson1.Children.Where(c => c.Type == "Friends").ToArray();
            AssertJohnDoeFriends(mutationFriends1);

            AssertValue("John", queryPerson1, "FirstName");
            AssertValue("Doe", queryPerson1, "LastName");
            AssertValue("Johnny", queryPerson1, "NickName");
            var queryFriends1 = queryPerson1.Children.Where(c => c.Type == "Friends").ToArray();
            AssertJohnDoeFriends(queryFriends1);

            AssertValue("Peter", mutationPerson2, "FirstName");
            AssertValue("Banner", mutationPerson2, "LastName");
            AssertValue("Pete", mutationPerson2, "NickName");
            var mutationFriends2 = mutationPerson2.Children.Where(c => c.Type == "Friends").ToArray();
            AssertPeterBannerFriends(mutationFriends2);

            AssertValue("Peter", queryPerson2, "FirstName");
            AssertValue("Banner", queryPerson2, "LastName");
            AssertValue("Pete", queryPerson2, "NickName");
            var queryFriends2 = queryPerson2.Children.Where(c => c.Type == "Friends").ToArray();
            AssertPeterBannerFriends(queryFriends2);
        }

        private void AssertValue(object expected, Structure structure, string valueName)
        {
            var value = structure.Values.SingleOrDefault(v => v.Name == valueName);
            Assert.NotNull(value);
            Assert.Equal(expected, value.Object);

        }
    }
}
