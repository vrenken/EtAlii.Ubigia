namespace EtAlii.Ubigia.Api.Functional.Tests 
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Querying;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

//using EtAlii.Ubigia.Api.Functional.Diagnostics.Querying;

    public class QueryProcessorMutationsSimpleTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private IGraphSLScriptContext _scriptContext;
        private IGraphTLQueryContext _queryContext;
        private readonly QueryingUnitTestContext _testContext;
        private IDiagnosticsConfiguration _diagnostics;

        public QueryProcessorMutationsSimpleTests(QueryingUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _diagnostics = _testContext.FunctionalTestContext.Diagnostics;
            var configuration = new GraphTLQueryContextConfiguration()
                .UseFunctionalGraphTLDiagnostics(_testContext.FunctionalTestContext.Diagnostics)
                .UseFunctionalGraphSLDiagnostics(_testContext.FunctionalTestContext.Diagnostics);
            await _testContext.FunctionalTestContext.ConfigureLogicalContextConfiguration(configuration,true);
            
            _scriptContext = new GraphSLScriptContextFactory().Create(configuration);
            _queryContext = new GraphTLQueryContextFactory().Create(configuration);
        
            await _testContext.FunctionalTestContext.AddPeople(_scriptContext);
            await _testContext.FunctionalTestContext.AddAddresses(_scriptContext); 

            Console.WriteLine("{1}.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphTLQueryContext));
        }

        public Task DisposeAsync()
        {
            var start = Environment.TickCount;

            _scriptContext = null;
            _queryContext = null;

            Console.WriteLine("{1}.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphTLQueryContext));
            return Task.CompletedTask;
        }

        [Fact]
        public async Task QueryProcessor_Mutate_Person_01()
        {
            // Arrange.
            var mutationText = @"Person @node(Person:Doe/John)
                               {
                                    Weight <= 160.1,
                                    NickName <= ""HeavyJohnny""
                               }";
            var mutation = _queryContext.Parse(mutationText).Query;

            var queryText = @"Person @node(Person:Doe/John)
                              {
                                    Weight,
                                    NickName
                              }";
            var query = _queryContext.Parse(queryText).Query;

            var scope = new QueryScope();
            var configuration = new QueryProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new QueryProcessorFactory().Create(configuration);

            // Act.
            var mutationResult = await processor.Process(mutation);
            await mutationResult.Output;
            var queryResult = await processor.Process(query);
            await queryResult.Output;

            // Assert.
            var mutationStructure = mutationResult.Structure.Single();
            Assert.NotNull(mutationStructure);
            var queryStructure = queryResult.Structure.Single();
            Assert.NotNull(queryStructure);
            
            AssertValue(160.1f, mutationStructure, "Weight");
            AssertValue(160.1f, queryStructure, "Weight");

            AssertValue("HeavyJohnny", mutationStructure, "NickName");
            AssertValue("HeavyJohnny", queryStructure, "NickName");
        }

        [Fact]
        public async Task QueryProcessor_Mutate_Person_02()
        {
            // Arrange.
            var mutationText = @"Person @node(Person:Doe += Mary)
                               {
                                    Weight <= 160.1,
                                    NickName <= ""MinteyMary""
                               }";
            var mutation = _queryContext.Parse(mutationText).Query;

            var queryText = @"Person @node(Person:Doe/Mary)
                              {
                                    Weight,
                                    NickName
                              }";
            var query = _queryContext.Parse(queryText).Query;

            var scope = new QueryScope();
            var configuration = new QueryProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new QueryProcessorFactory().Create(configuration);

            // Act.
            var mutationResult = await processor.Process(mutation);
            await mutationResult.Output;
            var queryResult = await processor.Process(query);
            await queryResult.Output;

            // Assert.
            var mutationStructure = mutationResult.Structure.Single();
            Assert.NotNull(mutationStructure);
            var queryStructure = queryResult.Structure.Single();
            Assert.NotNull(queryStructure);
            
            AssertValue(160.1f, mutationStructure, "Weight");
            AssertValue(160.1f, queryStructure, "Weight");

            AssertValue("MinteyMary", mutationStructure, "NickName");
            AssertValue("MinteyMary", queryStructure, "NickName");
        }

        
        [Fact]
        public async Task QueryProcessor_Mutate_Person_03()
        {
            // Arrange.
            var mutationText = @"Person @node(Person:Doe/John)
                                 {
                                     FirstName @value(),
                                     LastName @value(\#FamilyName),
                                     NickName,
                                     Friends @nodes(/Friends += Person:Stark/Tony)
                                     {
                                        FirstName @value(),
                                        LastName @value(\#FamilyName),
                                        NickName
                                     }  
                                 }";
            var mutation = _queryContext.Parse(mutationText).Query;

            var queryText = @"Person @node(Person:Doe/John)
                              {    
                                    FirstName @value(),
                                    LastName @value(\#FamilyName),
                                    NickName
                                    Friend @nodes(/Friends/)
                                    {
                                        FirstName @value(),
                                        LastName @value(\#FamilyName),
                                        NickName
                                    }
                              }";
            var query = _queryContext.Parse(queryText).Query;

            var scope = new QueryScope();
            var configuration = new QueryProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new QueryProcessorFactory().Create(configuration);

            // Act.
            var mutationResult = await processor.Process(mutation);
            await mutationResult.Output;
            var queryResult = await processor.Process(query);
            await queryResult.Output;

            // Assert.
            var mutationStructure = mutationResult.Structure.Single();
            Assert.NotNull(mutationStructure);
            var queryStructure = queryResult.Structure.Single();
            Assert.NotNull(queryStructure);
            
            AssertValue("John", mutationStructure, "FirstName");
            AssertValue("John", queryStructure, "FirstName");

            AssertValue("Doe", mutationStructure, "LastName");
            AssertValue("Doe", queryStructure, "LastName");

            AssertValue("Johnny", mutationStructure, "NickName");
            AssertValue("Johnny", queryStructure, "NickName");

            var mutationFriends = mutationStructure.Children.Where(c => c.Name == "Friend")?.ToArray();
            var queryFriends = queryStructure.Children.Where(c => c.Name == "Friend")?.ToArray();
            
            Assert.NotNull(mutationFriends);
            Assert.NotNull(queryFriends);
            Assert.Equal(mutationFriends.Length, queryFriends.Length);
            
            throw new NotImplementedException("Implement further friends tests");
        }

        [Fact]
        public async Task QueryProcessor_Mutate_Persons_01()
        {
            // Arrange.
            var mutationText = @"Person @nodes(Person:Doe += Mary)
                               {
                                    Weight <= 160.1,
                                    NickName <= ""MinteyMary""
                               }";
            var mutation = _queryContext.Parse(mutationText).Query;

            var queryText = @"Person @nodes(Person:Doe/)
                              {
                                    Weight,
                                    NickName
                              }";
            var query = _queryContext.Parse(queryText).Query;

            var scope = new QueryScope();
            var configuration = new QueryProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new QueryProcessorFactory().Create(configuration);

            // Act.
            var mutationResult = await processor.Process(mutation);
            await mutationResult.Output;
            var queryResult = await processor.Process(query);
            await queryResult.Output;

            // Assert.
            var mutationStructures = mutationResult.Structure.ToArray();
            Assert.NotNull(mutationStructures);
            var queryStructures = queryResult.Structure.ToArray();
            Assert.NotNull(queryStructures);
            Assert.Equal(3, mutationStructures.Length);
            Assert.Equal(3, queryStructures.Length);
            
            var mutationStructure = mutationStructures.Single(s => s.Name == "Mary");
            var queryStructure = queryStructures.Single(s => s.Name == "Mary");
            
            AssertValue(160.1f, mutationStructure, "Weight");
            AssertValue(160.1f, queryStructure, "Weight");

            AssertValue("MinteyMary", mutationStructure, "NickName");
            AssertValue("MinteyMary", queryStructure, "NickName");
        }

                
        [Fact]
        public async Task QueryProcessor_Mutate_Person_Friends()
        {
            // Arrange.
            var selectQueryText = @"Person @nodes(Person:Doe/John)
                               {
                                    FirstName @value()
                                    LastName @value(\#FamilyName)
                                    Nickname
                                    Birthdate
                                    Friends @nodes(/Friends += Person:Vrenken/Peter)
                                    {
                                        FirstName @value()
                                        LastName @value(\#FamilyName)
                                    }
                               }";

            var selectQuery = _queryContext.Parse(selectQueryText).Query;

            var scope = new QueryScope();
            var configuration = new QueryProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new QueryProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(selectQuery);
            var lastResult = await result.Output.LastOrDefaultAsync();

            // Assert.
            Assert.Equal(1, result.Structure.Count);
            
            var person = result.Structure[0];
            Assert.NotNull(person);
            AssertValue("John", person, "FirstName");
            AssertValue("Doe", person, "LastName");
            AssertValue(DateTime.Parse("1978-07-28"), person, "Birthdate");
            AssertValue("Johnny", person, "Nickname");

            Assert.Equal(3, person.Children.Count); 
            AssertValue("Tony", person.Children[0], "FirstName");
            AssertValue("Stark", person.Children[0], "LastName");
            AssertValue("Jane", person.Children[1], "FirstName");
            AssertValue("Doe", person.Children[1], "LastName");
            AssertValue("Peter", person.Children[2], "FirstName");
            AssertValue("Vrenken", person.Children[2], "LastName");
        }


        private void AssertValue(object expected, Structure structure, string valueName)
        {
            var value = structure.Values.SingleOrDefault(v => v.Name == valueName);
            Assert.NotNull(value);
            Assert.Equal(expected, value.Object);
            
        }
    }
}