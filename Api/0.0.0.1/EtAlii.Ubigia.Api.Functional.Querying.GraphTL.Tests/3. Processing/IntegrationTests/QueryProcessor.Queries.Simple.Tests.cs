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

    public class SchemaProcessorQueriesSimpleTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private IGraphSLScriptContext _scriptContext;
        private IGraphTLContext _context;
        private readonly QueryingUnitTestContext _testContext;
        private IDiagnosticsConfiguration _diagnostics;

        public SchemaProcessorQueriesSimpleTests(QueryingUnitTestContext testContext)
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
        public Task SchemaProcessor_Create()
        {
            // Arrange.
            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);

            // Act.
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Assert.
            Assert.NotNull(processor);
            
            return Task.CompletedTask;
        }
        
        

        [Fact]
        public async Task SchemaProcessor_Query_Time_Now_By_Structure()
        {
            // Arrange.
            var queryText = @"Time @node(time:now)
                               {
                                    Millisecond @value()
                                    Second @value(\)
                                    Minute @value(\\)
                                    Hour @value(\\\)
                                    Day @value(\\\\)
                                    Month @value(\\\\\)
                                    Year @value(\\\\\\)
                               }";

            var query = _context.Parse(queryText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(query);
            await result.Completed();

            // Assert.
            var structure = result.Structure.SingleOrDefault();
            Assert.NotNull(structure);

            void AssertTimeValue(string valueName)
            {
                var value = structure.Values.SingleOrDefault(v => v.Name == valueName);
                Assert.NotNull(value);
                Assert.IsType<string>(value.Object);
                Assert.True(((string)value.Object).All(Char.IsDigit));
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
        public async Task SchemaProcessor_Query_Time_Now_By_Last_Output()
        {
            // Arrange.
            var selectSchemaText = @"Time @node(time:now)
                                   {
                                        Millisecond @value()
                                        Second @value(\)
                                        Minute @value(\\)
                                        Hour @value(\\\)
                                        Day @value(\\\\)
                                        Month @value(\\\\\)
                                        Year @value(\\\\\\)
                                   }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(selectSchema);
            await result.Completed();
            var lastStructure = await result.Output.LastOrDefaultAsync();

            // Assert.
            Assert.NotNull(result.Output);
            Assert.NotNull(lastStructure);
            var structure = result.Structure.SingleOrDefault();
            Assert.NotNull(structure);
            Assert.Same(structure, lastStructure);
        }

        [Fact]
        public async Task SchemaProcessor_Query_Person_By_Structure()
        {
            // Arrange.
            var selectSchemaText = @"Person @nodes(Person:Stark/Tony)
                                   {
                                        FirstName @value()
                                        LastName @value(\#FamilyName)
                                   }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(selectSchema);
            await result.Completed();

            // Assert.
            var structure = result.Structure.SingleOrDefault();
            Assert.NotNull(structure);
            
            AssertValue("Tony", structure, "FirstName");
            AssertValue("Stark", structure, "LastName");
        }

        [Fact]
        public async Task SchemaProcessor_Query_Persons_By_Structure_02()
        {
            // Arrange.
            var selectSchemaText = @"Person @nodes(Person:*/*)
                                   {
                                        FirstName @value()
                                        LastName @value(\#FamilyName)
                                   }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(selectSchema);
            await result.Completed();

            // Assert.
            var structures = result.Structure.ToArray();
            Assert.NotNull(structures);
            
                
            AssertValue("John", structures[0], "FirstName");
            AssertValue("Doe", structures[0], "LastName");

            AssertValue("Jane", structures[1], "FirstName");
            AssertValue("Doe", structures[1], "LastName");
            
            AssertValue("Tony", structures[2], "FirstName");
            AssertValue("Stark", structures[2], "LastName");

            AssertValue("Peter", structures[3], "FirstName");
            AssertValue("Vrenken", structures[3], "LastName");

            AssertValue("Tanja", structures[4], "FirstName");
            AssertValue("Vrenken", structures[4], "LastName");

            AssertValue("Arjan", structures[5], "FirstName");
            AssertValue("Vrenken", structures[5], "LastName");

            AssertValue("Ida", structures[6], "FirstName");
            AssertValue("Vrenken", structures[6], "LastName");

        }

        [Fact]
        public async Task SchemaProcessor_Query_Person_By_Last_Output()
        {
            // Arrange.
            var selectSchemaText = @"Person @nodes(Person:Stark/Tony)
                                   {
                                        FirstName @value()
                                        LastName @value(\#FamilyName)
                                   }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(selectSchema);
            await result.Completed();
            var lastStructure = await result.Output.LastOrDefaultAsync();

            // Assert.
            Assert.NotNull(result.Output);
            Assert.NotNull(lastStructure);
            
            var structure = result.Structure.SingleOrDefault();
            Assert.NotNull(structure);
            Assert.Same(structure, lastStructure);
        }

        //[Fact(Skip = "This would be cool but isn't working (yet)")]
        [Fact]
        public async Task SchemaProcessor_Query_Person_By_Values()
        {
            // Arrange.
            var selectSchemaText = @"Data
                                   {
                                        FirstName @value(Person:Stark/Tony)
                                        LastName @value(Person:Stark/Tony\#FamilyName)
                                   }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(selectSchema);
            await result.Completed();
            var lastStructure = await result.Output.LastOrDefaultAsync();

            // Assert.
            Assert.NotNull(result.Output);
            Assert.NotNull(lastStructure);
            
            var structure = result.Structure.SingleOrDefault();
            Assert.NotNull(structure);
            Assert.Same(structure, lastStructure);

            AssertValue("Tony", structure, "FirstName");
            AssertValue("Stark", structure, "LastName");
        }

        
        [Fact]
        public async Task SchemaProcessor_Query_Person_Nested_By_Structure()
        {
            // Arrange.
            var selectSchemaText = @"Person @nodes(Person:Stark/Tony)
                                    {
                                        Data
                                        {
                                            FirstName @value()
                                            LastName @value(\#FamilyName)
                                        }
                                    }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(selectSchema);
            await result.Completed();

            // Assert.
            var structure = result.Structure.SingleOrDefault();
            Assert.NotNull(structure);
            structure = structure.Children.SingleOrDefault();
            Assert.NotNull(structure);
            
            AssertValue("Tony", structure, "FirstName");
            AssertValue("Stark", structure, "LastName");
        }

        
        
        [Fact]
        public async Task SchemaProcessor_Query_Person_Nested_By_Last_Output()
        {
            // Arrange.
            var selectSchemaText = @"Person @nodes(Person:Stark/Tony)
                                   {
                                        Data
                                        {
                                            FirstName @value()
                                            LastName @value(\#FamilyName)
                                        }
                                   }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(selectSchema);
            await result.Completed();
            var lastStructure = await result.Output.LastOrDefaultAsync();

            // Assert.
            Assert.NotNull(result.Output);
            Assert.NotNull(lastStructure);
            
            var structure = result.Structure.SingleOrDefault();
            Assert.NotNull(structure);
            structure = structure.Children.SingleOrDefault();
            Assert.Same(structure, lastStructure);

        }

                
        [Fact]
        public async Task SchemaProcessor_Query_Person_Nested_Double_By_Structure()
        {
            // Arrange.
            var selectSchemaText = @"Person @nodes(Person:Stark/Tony)
                                   {
                                        Data1
                                        {
                                            Data2
                                            {
                                                FirstName @value()
                                                LastName @value(\#FamilyName)
                                            }
                                        }
                                   }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(selectSchema);
            await result.Completed();

            // Assert.
            var structure = result.Structure.SingleOrDefault();
            Assert.NotNull(structure);
            structure = structure.Children.SingleOrDefault();
            Assert.NotNull(structure);
            structure = structure.Children.SingleOrDefault();
            Assert.NotNull(structure);
            
            AssertValue("Tony", structure, "FirstName");
            AssertValue("Stark", structure, "LastName");
        }


        [Fact]
        public async Task SchemaProcessor_Query_Person_Nested_Double_By_Last_Output()
        {
            // Arrange.
            var selectSchemaText = @"Person @nodes(Person:Stark/Tony)
                                   {
                                        Data1
                                        {
                                            Data2
                                            {
                                                FirstName @value()
                                                LastName @value(\#FamilyName)
                                            }
                                        }
                                   }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(selectSchema);
            await result.Completed();
            var lastStructure = await result.Output.LastOrDefaultAsync();

            // Assert.
            Assert.NotNull(result.Output);
            Assert.NotNull(lastStructure);
            
            var structure = result.Structure.SingleOrDefault();
            Assert.NotNull(structure);
            structure = structure.Children.SingleOrDefault();
            Assert.NotNull(structure);
            structure = structure.Children.SingleOrDefault();
            Assert.Same(structure, lastStructure);

        }

        [Fact]
        public async Task SchemaProcessor_Query_Persons_By_Structure_01()
        {
            // Arrange.
            var selectSchemaText = @"Person @nodes(Person:Doe/*)
                               {
                                    FirstName @value()
                                    LastName @value(\#FamilyName)
                                    NickName
                                    Birthdate
                               }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(selectSchema);
            await result.Completed();

            // Assert.
            Assert.Equal(2, result.Structure.Count);
            
            var firstPerson = result.Structure[0];
            Assert.NotNull(firstPerson);
            AssertValue("John", firstPerson, "FirstName");
            AssertValue("Doe", firstPerson, "LastName");
            AssertValue(DateTime.Parse("1978-07-28"), firstPerson, "Birthdate");
            AssertValue("Johnny", firstPerson, "NickName");

            var secondPerson = result.Structure[1];
            Assert.NotNull(secondPerson);
            AssertValue("Jane", secondPerson, "FirstName");
            AssertValue("Doe", secondPerson, "LastName");
            AssertValue(DateTime.Parse("1980-03-04"), secondPerson, "Birthdate");
            AssertValue("Janey", secondPerson, "NickName");

        }
        
        [Fact]
        public async Task SchemaProcessor_Query_Person_Friends()
        {
            // Arrange.
            var selectSchemaText = @"Person @nodes(Person:Doe/John)
                               {
                                    FirstName @value()
                                    LastName @value(\#FamilyName)
                                    NickName
                                    Birthdate
                                    Friends @nodes(/Friends/)
                                    {
                                        FirstName @value()
                                        LastName @value(\#FamilyName)
                                    }
                               }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(selectSchema);
            await result.Completed();

            // Assert.
            Assert.Single(result.Structure);
            
            var person = result.Structure[0];
            Assert.NotNull(person);
            AssertValue("John", person, "FirstName");
            AssertValue("Doe", person, "LastName");
            AssertValue(DateTime.Parse("1978-07-28"), person, "Birthdate");
            AssertValue("Johnny", person, "NickName");

            Assert.Equal(2, person.Children.Count); 
            AssertValue("Tony", person.Children[0], "FirstName");
            AssertValue("Stark", person.Children[0], "LastName");
            AssertValue("Jane", person.Children[1], "FirstName");
            AssertValue("Doe", person.Children[1], "LastName");
        }

        [Fact]
        public async Task SchemaProcessor_Query_Persons_By_Last_Output()
        {
            // Arrange.
            var selectSchemaText = @"Person @nodes(Person:Doe/*)
                               {
                                    FirstName @value()
                                    LastName @value(\#FamilyName)
                                    NickName
                                    Birthdate
                               }";

            var selectSchema = _context.Parse(selectSchemaText).Schema;

            var scope = new SchemaScope();
            var configuration = new SchemaProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new SchemaProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(selectSchema);
            await result.Completed();
            var lastStructure = await result.Output.LastOrDefaultAsync();

            // Assert.
            Assert.NotNull(result.Output);
            Assert.NotNull(lastStructure);
        }

        private void AssertValue(object expected, Structure structure, string valueName)
        {
            var value = structure.Values.SingleOrDefault(v => v.Name == valueName);
            Assert.NotNull(value);
            Assert.Equal(expected, value.Object);
            
        }
        
    }
}