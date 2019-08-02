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

    public class SchemaProcessorMutationsSimpleTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private IGraphSLScriptContext _scriptContext;
        private IGraphTLContext _context;
        private readonly QueryingUnitTestContext _testContext;
        private IDiagnosticsConfiguration _diagnostics;

        public SchemaProcessorMutationsSimpleTests(QueryingUnitTestContext testContext)
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
            _context = new GraphTLQueryContextFactory().Create(configuration);
        
            await _testContext.FunctionalTestContext.AddPeople(_scriptContext);
            await _testContext.FunctionalTestContext.AddAddresses(_scriptContext); 

            Console.WriteLine("{1}.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphTLContext));
        }

        public Task DisposeAsync()
        {
            var start = Environment.TickCount;

            _scriptContext = null;
            _context = null;

            Console.WriteLine("{1}.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphTLContext));
            return Task.CompletedTask;
        }

        [Fact]
        public async Task SchemaProcessor_Mutate_Person_01()
        {
            // Arrange.
            var mutationText = @"Person @node(Person:Doe/John)
                               {
                                    Weight <= 160.1,
                                    NickName <= ""HeavyJohnny""
                               }";
            var mutationSchema = _context.Parse(mutationText).Schema;

            var queryText = @"Person @node(Person:Doe/John)
                              {
                                    Weight,
                                    NickName
                              }";
            var querySchema = _context.Parse(queryText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var mutationResult = await processor.Process(mutationSchema);
            await mutationResult.Output;
            var queryResult = await processor.Process(querySchema);
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
        public async Task SchemaProcessor_Mutate_Person_02()
        {
            // Arrange.
            var mutationText = @"Person @node(Person:Doe += Mary)
                               {
                                    Weight <= 160.1,
                                    NickName <= ""MinteyMary""
                               }";
            var mutationSchema = _context.Parse(mutationText).Schema;

            var queryText = @"Person @node(Person:Doe/Mary)
                              {
                                    Weight,
                                    NickName
                              }";
            var querySchema = _context.Parse(queryText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var mutationResult = await processor.Process(mutationSchema);
            await mutationResult.Output;
            var queryResult = await processor.Process(querySchema);
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
        public async Task SchemaProcessor_Mutate_Person_03()
        {
            // Arrange.
            var mutationText = @"Person @node(Person:Doe/John)
                                 {
                                     FirstName @value(),
                                     LastName @value(\#FamilyName),
                                     NickName,
                                     Friend @nodes(/Friends += Person:Vrenken/Peter)
                                     {
                                        FirstName @value(),
                                        LastName @value(\#FamilyName),
                                        NickName
                                     }  
                                 }";
            var mutationSchema = _context.Parse(mutationText).Schema;

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
            var querySchema = _context.Parse(queryText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var mutationResult = await processor.Process(mutationSchema);
            await mutationResult.Output;
            var queryResult = await processor.Process(querySchema);
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
                AssertValue("Vrenken", friends[2], "LastName");
                AssertValue("Pete", friends[2], "NickName");
            }
            var mutationFriends = mutationStructure.Children.Where(c => c.Type == "Friend").ToArray();
            AssertFriends(mutationFriends);
            var queryFriends = queryStructure.Children.Where(c => c.Type == "Friend").ToArray();
            AssertFriends(queryFriends);
        }

        [Fact]
        public async Task SchemaProcessor_Mutate_Persons_01()
        {
            // Arrange.
            var mutationText = @"Person @nodes(Person:Doe += Mary)
                               {
                                    Weight <= 160.1,
                                    NickName <= ""MinteyMary""
                               }";
            var mutationSchema = _context.Parse(mutationText).Schema;

            var queryText = @"Person @nodes(Person:Doe/)
                              {
                                    Weight,
                                    NickName
                              }";
            var querySchema = _context.Parse(queryText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var mutationResult = await processor.Process(mutationSchema);
            await mutationResult.Output;
            var queryResult = await processor.Process(querySchema);
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
        public async Task SchemaProcessor_Mutate_Person_Friends()
        {
            // Arrange.
            var mutationText = @"Person @nodes(Person:Doe/John)
                               {
                                    FirstName @value()
                                    LastName @value(\#FamilyName)
                                    NickName
                                    Birthdate
                                    Friends @nodes(/Friends += Person:Vrenken/Peter)
                                    {
                                        FirstName @value()
                                        LastName @value(\#FamilyName)
                                    }
                               }";

            var mutationSchema = _context.Parse(mutationText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(mutationSchema);
            var lastResult = await result.Output.LastOrDefaultAsync();

            // Assert.
            Assert.Single(result.Structure);
            
            var person = result.Structure[0];
            Assert.NotNull(person);
            AssertValue("John", person, "FirstName");
            AssertValue("Doe", person, "LastName");
            AssertValue(DateTime.Parse("1978-07-28"), person, "Birthdate");
            AssertValue("Johnny", person, "NickName");

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