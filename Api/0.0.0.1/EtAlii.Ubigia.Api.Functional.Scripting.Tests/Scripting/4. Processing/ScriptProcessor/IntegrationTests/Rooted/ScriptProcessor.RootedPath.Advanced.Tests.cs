namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;


    
    public class ScriptProcessorRootedPathAdvancedTests : IAsyncLifetime
    {
        private IScriptParser _parser;
        private IDiagnosticsConfiguration _diagnostics;
        private static ILogicalTestContext _testContext;

        public ScriptProcessorRootedPathAdvancedTests()
        {
        }

        public async Task InitializeAsync()
        {
            _testContext = new LogicalTestContextFactory().Create();
            await _testContext.Start();

            _diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(_diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        public async Task DisposeAsync()
        {
            _parser = null;

            await _testContext.Stop();
            _testContext = null;
        }

        [Fact]
        public async Task ScriptProcessor_RootedPath_Advanced_Create()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);

            // Act.
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Assert.
            Assert.NotNull(processor);
        }

        [Fact]
        public async Task ScriptProcessor_RootedPath_Advanced_Create_By_Root_And_Query_First_By_Root()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "Time:2016/09/01/22/05",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/22/05/00/000";
            var selectQuery2 = "Time:2016/09/01/22/05";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript1 = _parser.Parse(selectQuery1).Script;
            var selectScript2 = _parser.Parse(selectQuery2).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            var addResult = await lastSequence.Output.Cast<INode>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1);
            var firstResult = await lastSequence.Output.Cast<INode>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2);
            var secondResult = await lastSequence.Output.Cast<INode>().SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(addResult.Id, firstResult.Id);
            Assert.Equal(addResult.Id, secondResult.Id);
        }

        [Fact]
        public async Task ScriptProcessor_RootedPath_Advanced_Create_By_Root_And_Query_First_By_AbsolutePath()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "Time:2016/09/01/22/05",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery1 = "/Time/2016/09/01/22/05/00/000";
            var selectQuery2 = "Time:2016/09/01/22/05";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript1 = _parser.Parse(selectQuery1).Script;
            var selectScript2 = _parser.Parse(selectQuery2).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            var addResult = await lastSequence.Output.Cast<INode>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript1);
            var firstResult = await lastSequence.Output.Cast<INode>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(selectScript2);
            var secondResult = await lastSequence.Output.Cast<INode>().SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(addResult);
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(addResult.Id, firstResult.Id);
            Assert.Equal(addResult.Id, secondResult.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Should_Not_Clear_Children()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "Person:Doe/";
            var assignQuery = "Person:Doe <= { Type: 'Family' }";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            var assignScript = _parser.Parse(assignQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var personsBefore = await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(assignScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var personsAfter = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(personsBefore);
            Assert.NotNull(personsAfter);
            Assert.Equal(3, personsBefore.Length);
            Assert.Equal(3, personsAfter.Length);
        }

        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Parent_Should_Return_Same_Node()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "Person:Doe/John\\";
            
            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;
            
            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            var parents = await lastSequence.Output.ToArray();
            
            // Assert.
            Assert.Single(parents);
            Assert.Equal("Doe", ((INode)parents.First()).Type);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_To_Variable_And_Then_ReUse_01()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var select1Query = "Person:Doe/Jane";
            var select2Query = "$person <= Person:Doe\r\n$person/Jane";

            var addScript = _parser.Parse(addQuery).Script;
            var select1Script = _parser.Parse(select1Query).Script;
            var select2Script = _parser.Parse(select2Query).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(select1Script);
            var firstResult = await lastSequence.Output.Cast<INode>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(select2Script);
            var secondResult = await lastSequence.Output.Cast<INode>().SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(firstResult.Id, secondResult.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_To_Variable_And_Then_ReUse_02()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var select1Query = "Person:Doe/";
            var select2Query = "$person <= Person:Doe\r\n$person/";

            var addScript = _parser.Parse(addQuery).Script;
            var select1Script = _parser.Parse(select1Query).Script;
            var select2Script = _parser.Parse(select2Query).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(select1Script);
            var firstResult = await lastSequence.Output.Cast<INode>().ToArray();
            lastSequence = await processor.Process(select2Script);
            var secondResult = await lastSequence.Output.Cast<INode>().ToArray();

            // Assert.
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(3, firstResult.Length);
            Assert.Equal(3, secondResult.Length);
            Assert.Equal(firstResult[0].Id, secondResult[0].Id);
            Assert.Equal(firstResult[1].Id, secondResult[1].Id);
            Assert.Equal(firstResult[2].Id, secondResult[2].Id);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_To_Variable_And_Then_ReUse_03()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
                "Person:+=Janssen/Jan",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var select1Query = "Person:Doe/";
            var select2Query = "$person <= Person:\r\n$person/Doe/";

            var addScript = _parser.Parse(addQuery).Script;
            var select1Script = _parser.Parse(select1Query).Script;
            var select2Script = _parser.Parse(select2Query).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(select1Script);
            var firstResult = await lastSequence.Output.Cast<INode>().ToArray();
            lastSequence = await processor.Process(select2Script);
            var secondResult = await lastSequence.Output.Cast<INode>().ToArray();

            // Assert.
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(3, firstResult.Length);
            Assert.Equal(3, secondResult.Length);
            Assert.Equal(firstResult[0].Id, secondResult[0].Id);
            Assert.Equal(firstResult[1].Id, secondResult[1].Id);
            Assert.Equal(firstResult[2].Id, secondResult[2].Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_To_Variable_And_Then_ReUse_04()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
                "Person:+=Janssen/Jan",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var select1Query = "Person:Doe/";
            var select2Query = "$person <= Person:\r\n<= id() <= $person/Doe/";

            var addScript = _parser.Parse(addQuery).Script;
            var select1Script = _parser.Parse(select1Query).Script;
            var select2Script = _parser.Parse(select2Query).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(select1Script);
            var firstResult = await lastSequence.Output.Cast<INode>().ToArray();
            lastSequence = await processor.Process(select2Script);
            var secondResult = await lastSequence.Output.Cast<Identifier>().ToArray();

            // Assert.
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(3, firstResult.Length);//, "First result is not correct");
            Assert.Equal(3, secondResult.Length);//, "Second result is not correct");
            Assert.Equal(firstResult[0].Id, secondResult[0]);
            Assert.Equal(firstResult[1].Id, secondResult[1]);
            Assert.Equal(firstResult[2].Id, secondResult[2]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Assign_Special_Characters()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new[]
            {
                "Person:+=Doe/\"Jöhn\"",
                "Person:+=Doe/\"Jóhn\"",
                "Person:+=Doe/\"Jähn\"",
                "Person:+=Doe/\"Jánê\"",
                "Person:+=Doe/\"Jöhnny\"",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var selectQuery = "Person:Doe/";

            var addScript = _parser.Parse(addQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(selectScript);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.Equal(5, result.Length);
            Assert.Equal("Jöhn", result.Skip(0).First().ToString());
            Assert.Equal("Jóhn", result.Skip(1).First().ToString());
            Assert.Equal("Jähn", result.Skip(2).First().ToString());
            Assert.Equal("Jánê", result.Skip(3).First().ToString());
            Assert.Equal("Jöhnny", result.Skip(4).First().ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Children_Should_Not_Clear_Assign()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQuery1 = "Person: += Doe";
            var addQueries2 = new[]
            {
                "Person:+=Doe/John",
                "Person:+=Doe/Jane",
                "Person:+=Doe/Johnny",
            };
            var addQuery2 = String.Join("\r\n", addQueries2);
            var selectQuery = "Person:Doe";
            var assignQuery = "Person:Doe <= { ObjectType: 'Family' }";

            var addScript1 = _parser.Parse(addQuery1).Script;
            var addScript2 = _parser.Parse(addQuery2).Script;
            var assignScript = _parser.Parse(assignQuery).Script;
            var selectScript = _parser.Parse(selectQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(assignScript);
            var result = await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic familyBefore = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(addScript2);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript);
            dynamic familyAfter = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotEmpty(result);
            Assert.NotNull(familyBefore);
            Assert.NotNull(familyAfter);
            Assert.Equal("Family", familyBefore.ObjectType);
            Assert.Equal("Family", familyAfter.ObjectType);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Move_Child()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries1 = new []
            {
              "Person: += Doe",
              "Person: += Does"
            };
            var addQueries2 = new[]
            {
                "Person:Doe += John",
                "Person:Doe += Johnny",
                "Person:Does += Jane",
            };

            var moveQueries = new[]
            {
                "$john <= Person:Doe/John",
                "Person:Does += $john",
                "Person:Doe -= John",
            };

            var addQuery1 = String.Join("\r\n", addQueries1);
            var addQuery2 = String.Join("\r\n", addQueries2);
            var moveQuery = String.Join("\r\n", moveQueries);
            var selectQuery1 = "Person:Doe/";
            var selectQuery2 = "Person:Does/";

            var addScript1 = _parser.Parse(addQuery1).Script;
            var addScript2 = _parser.Parse(addQuery2).Script;
            var selectScript1 = _parser.Parse(selectQuery1).Script;
            var selectScript2 = _parser.Parse(selectQuery2).Script;
            var moveScript = _parser.Parse(moveQuery).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript1);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(addScript2);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(selectScript1);
            var result = await lastSequence.Output.ToArray();
            var beforeCount1 = result.Length;
            lastSequence = await processor.Process(selectScript2);
            result = await lastSequence.Output.ToArray();
            var beforeCount2 = result.Length;

            lastSequence = await processor.Process(moveScript);
            await lastSequence.Output.ToArray();
            
            lastSequence = await processor.Process(selectScript1);
            result = await lastSequence.Output.ToArray();
            var afterCount1 = result.Length;
            lastSequence = await processor.Process(selectScript2);
            result = await lastSequence.Output.ToArray();
            var afterCount2 = result.Length;


            // Assert.
            Assert.Equal(2, beforeCount1);
            Assert.Equal(1, beforeCount2);
            Assert.Equal(1, afterCount1);
            Assert.Equal(2, afterCount2);
        }
        
        
        
        [Fact(Skip = "Blocking the build server"), Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Add_Friends_Using_Variables()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new []
            {
                "Person:+=Doe/John",
                "Person:Doe/John  <= { Birthdate: 1978-07-28, Nickname: 'Johnny', Lives: 1 }",
                "Person:Doe/John += Friends",
                "Person:+=Doe/Jane",
                "Person:Doe/Jane <= { Birthdate: 1980-03-04, Nickname: 'Janey', Lives: 2 }",
                "Person:Doe/Jane += Friends",
                "Person:+=Stark/Tony",
                "Person:Stark/Tony <= { Birthdate: 1976-05-12, Nickname: 'Iron Man', Lives: 9 }",
                "Person:Stark/Tony += Friends",
                
                "Person:Doe <= { Type: 'FamilyName' }",
                "Person:Stark <= { Type: 'FamilyName' }",
            };
            
            var linkQueries = new[]
            {
                "$john <= Person:Doe/John",
                "$jane <= Person:Doe/Jane",
                "$tony <= Person:Stark/Tony",
                
                "Person:Stark/Tony/Friends += $john",
                "Person:Doe/John/Friends += $tony",
                "Person:Doe/John/Friends += $jane",
                "Person:Doe/Jane/Friends += $john",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var linkQuery = String.Join("\r\n", linkQueries);
            var nodeSelectQuery1 = "Person:Doe/John";
            var nodeSelectQuery2 = "Person:Doe/Jane";
            var nodeSelectQuery3 = "Person:Stark/Tony";

            var friendsSelectQuery1 = "Person:Doe/John/Friends/";
            var friendsSelectQuery2 = "Person:Doe/Jane/Friends/";
            var friendsSelectQuery3 = "Person:Stark/Tony/Friends/";

            var addScript = _parser.Parse(addQuery).Script;
            var linkScript = _parser.Parse(linkQuery).Script;
            var nodeSelectScript1 = _parser.Parse(nodeSelectQuery1).Script;
            var nodeSelectScript2 = _parser.Parse(nodeSelectQuery2).Script;
            var nodeSelectScript3 = _parser.Parse(nodeSelectQuery3).Script;
            var friendsSelectScript1 = _parser.Parse(friendsSelectQuery1).Script;
            var friendsSelectScript2 = _parser.Parse(friendsSelectQuery2).Script;
            var friendsSelectScript3 = _parser.Parse(friendsSelectQuery3).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            
            lastSequence = await processor.Process(nodeSelectScript1);
            var result = await lastSequence.Output.ToArray();
            var beforeNodeCount1 = result.Length;
            lastSequence = await processor.Process(nodeSelectScript2);
            result = await lastSequence.Output.ToArray();
            var beforeNodeCount2 = result.Length;
            lastSequence = await processor.Process(nodeSelectScript3);
            result = await lastSequence.Output.ToArray();
            var beforeNodeCount3 = result.Length;
            
            lastSequence = await processor.Process(friendsSelectScript1);
            result = await lastSequence.Output.ToArray();
            var beforeFriendCount1 = result.Length;
            lastSequence = await processor.Process(friendsSelectScript2);
            result = await lastSequence.Output.ToArray();
            var beforeFriendCount2 = result.Length;
            lastSequence = await processor.Process(friendsSelectScript3);
            result = await lastSequence.Output.ToArray();
            var beforeFriendCount3 = result.Length;

            lastSequence = await processor.Process(linkScript);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(nodeSelectScript1);
            result = await lastSequence.Output.ToArray();
            var afterNodeCount1 = result.Length;
            lastSequence = await processor.Process(nodeSelectScript2);
            result = await lastSequence.Output.ToArray();
            var afterNodeCount2 = result.Length;
            lastSequence = await processor.Process(nodeSelectScript3);
            result = await lastSequence.Output.ToArray();
            var afterNodeCount3 = result.Length;
            
            lastSequence = await processor.Process(friendsSelectScript1);
            result = await lastSequence.Output.ToArray();
            var afterFriendCount1 = result.Length;
            lastSequence = await processor.Process(friendsSelectScript2);
            result = await lastSequence.Output.ToArray();
            var afterFriendCount2 = result.Length;
            lastSequence = await processor.Process(friendsSelectScript3);
            result = await lastSequence.Output.ToArray();
            var afterFriendCount3 = result.Length;

            // Assert.
            Assert.Equal(1, beforeNodeCount1);
            Assert.Equal(1, beforeNodeCount2);
            Assert.Equal(1, beforeNodeCount3);
            Assert.Equal(0, beforeFriendCount1);
            Assert.Equal(0, beforeFriendCount2);
            Assert.Equal(0, beforeFriendCount3);
            
            Assert.Equal(1, afterNodeCount1);
            Assert.Equal(1, afterNodeCount2);
            Assert.Equal(1, afterNodeCount3);
            Assert.Equal(2, afterFriendCount1);
            Assert.Equal(1, afterFriendCount2);
            Assert.Equal(1, afterFriendCount3);

        }
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ScriptProcessor_RootedPath_Add_Friends_Using_Paths()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addQueries = new []
            {
                "Person:+=Doe/John",
                "Person:Doe/John  <= { Birthdate: 1978-07-28, Nickname: 'Johnny', Lives: 1 }",
                "Person:Doe/John += Friends",
                "Person:+=Doe/Jane",
                "Person:Doe/Jane <= { Birthdate: 1980-03-04, Nickname: 'Janey', Lives: 2 }",
                "Person:Doe/Jane += Friends",
                "Person:+=Stark/Tony",
                "Person:Stark/Tony <= { Birthdate: 1976-05-12, Nickname: 'Iron Man', Lives: 9 }",
                "Person:Stark/Tony += Friends",
                
                "Person:Doe <= { Type: 'FamilyName' }",
                "Person:Stark <= { Type: 'FamilyName' }",
            };
            
            var linkQueries = new[]
            {
                "Person:Stark/Tony/Friends += Person:Doe/John",
                "Person:Doe/John/Friends += Person:Stark/Tony",
                "Person:Doe/John/Friends += Person:Doe/Jane",
                "Person:Doe/Jane/Friends += Person:Doe/John",
            };

            var addQuery = String.Join("\r\n", addQueries);
            var linkQuery = String.Join("\r\n", linkQueries);
            var nodeSelectQuery1 = "Person:Doe/John";
            var nodeSelectQuery2 = "Person:Doe/Jane";
            var nodeSelectQuery3 = "Person:Stark/Tony";

            var friendsSelectQuery1 = "Person:Doe/John/Friends/";
            var friendsSelectQuery2 = "Person:Doe/Jane/Friends/";
            var friendsSelectQuery3 = "Person:Stark/Tony/Friends/";

            var addScript = _parser.Parse(addQuery).Script;
            var linkScript = _parser.Parse(linkQuery).Script;
            var nodeSelectScript1 = _parser.Parse(nodeSelectQuery1).Script;
            var nodeSelectScript2 = _parser.Parse(nodeSelectQuery2).Script;
            var nodeSelectScript3 = _parser.Parse(nodeSelectQuery3).Script;
            var friendsSelectScript1 = _parser.Parse(friendsSelectQuery1).Script;
            var friendsSelectScript2 = _parser.Parse(friendsSelectQuery2).Script;
            var friendsSelectScript3 = _parser.Parse(friendsSelectQuery3).Script;

            var scope = new ScriptScope();
            var configuration = new ScriptProcessorConfiguration()
                .Use(_diagnostics)
                .Use(scope)
                .Use(logicalContext);
            var processor = new ScriptProcessorFactory().Create(configuration);

            // Act.
            var lastSequence = await processor.Process(addScript);
            await lastSequence.Output.ToArray();
            
            lastSequence = await processor.Process(nodeSelectScript1);
            var result = await lastSequence.Output.ToArray();
            var beforeNodeCount1 = result.Length;
            lastSequence = await processor.Process(nodeSelectScript2);
            result = await lastSequence.Output.ToArray();
            var beforeNodeCount2 = result.Length;
            lastSequence = await processor.Process(nodeSelectScript3);
            result = await lastSequence.Output.ToArray();
            var beforeNodeCount3 = result.Length;
            
            lastSequence = await processor.Process(friendsSelectScript1);
            result = await lastSequence.Output.ToArray();
            var beforeFriendCount1 = result.Length;
            lastSequence = await processor.Process(friendsSelectScript2);
            result = await lastSequence.Output.ToArray();
            var beforeFriendCount2 = result.Length;
            lastSequence = await processor.Process(friendsSelectScript3);
            result = await lastSequence.Output.ToArray();
            var beforeFriendCount3 = result.Length;

            lastSequence = await processor.Process(linkScript);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(nodeSelectScript1);
            result = await lastSequence.Output.ToArray();
            var afterNodeCount1 = result.Length;
            lastSequence = await processor.Process(nodeSelectScript2);
            result = await lastSequence.Output.ToArray();
            var afterNodeCount2 = result.Length;
            lastSequence = await processor.Process(nodeSelectScript3);
            result = await lastSequence.Output.ToArray();
            var afterNodeCount3 = result.Length;
            
            lastSequence = await processor.Process(friendsSelectScript1);
            result = await lastSequence.Output.ToArray();
            var afterFriendCount1 = result.Length;
            lastSequence = await processor.Process(friendsSelectScript2);
            result = await lastSequence.Output.ToArray();
            var afterFriendCount2 = result.Length;
            lastSequence = await processor.Process(friendsSelectScript3);
            result = await lastSequence.Output.ToArray();
            var afterFriendCount3 = result.Length;

            // Assert.
            Assert.Equal(1, beforeNodeCount1);
            Assert.Equal(1, beforeNodeCount2);
            Assert.Equal(1, beforeNodeCount3);
            Assert.Equal(0, beforeFriendCount1);
            Assert.Equal(0, beforeFriendCount2);
            Assert.Equal(0, beforeFriendCount3);
            
            Assert.Equal(1, afterNodeCount1);
            Assert.Equal(1, afterNodeCount2);
            Assert.Equal(1, afterNodeCount3);
            Assert.Equal(2, afterFriendCount1);
            Assert.Equal(1, afterFriendCount2);
            Assert.Equal(1, afterFriendCount3);

        }
    }
}