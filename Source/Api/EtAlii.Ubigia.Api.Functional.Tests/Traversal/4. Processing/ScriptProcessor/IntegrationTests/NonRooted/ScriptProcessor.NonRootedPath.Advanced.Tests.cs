// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;

    public class ScriptProcessorNonRootedPathAdvancedTests : IAsyncLifetime
    {
        private IScriptParser _parser;
        private FunctionalUnitTestContext _testContext;

        public async Task InitializeAsync()
        {
            _testContext = new FunctionalUnitTestContext();
            await _testContext
                .InitializeAsync()
                .ConfigureAwait(false);

            _parser = _testContext.CreateScriptParser();
        }

        public async Task DisposeAsync()
        {
            await _testContext
                .DisposeAsync()
                .ConfigureAwait(false);
            _testContext = null;
            _parser = null;
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Advanced_Create()
        {
            // Arrange.

            // Act.
            var processor = await _testContext
                .CreateScriptProcessorOnNewSpace()
                .ConfigureAwait(false);


            // Assert.
            Assert.NotNull(processor);

        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Assign_Should_Not_Clear_Children()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person+=Doe/Jane",
                "/Person+=Doe/Johnny",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/";
            var assignQuery = "/Person/Doe# <= FamilyName";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var assignScript = _parser.Parse(assignQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            var personsBefore = await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(assignScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            var personsAfter = await lastSequence.Output.ToArray();

            // Assert.
            Assert.NotNull(personsBefore);
            Assert.NotNull(personsAfter);
            Assert.Equal(3, personsBefore.Length);
            Assert.Equal(3, personsAfter.Length);
        }


        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Assign_To_Variable_And_Then_ReUse_01()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person+=Doe/Jane",
                "/Person+=Doe/Johnny",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var select1Query = "/Person/Doe/Jane";
            var select2Query = "$person <= /Person/Doe\r\n$person/Jane";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var select1Script = _parser.Parse(select1Query, scope).Script;
            var select2Script = _parser.Parse(select2Query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(select1Script, scope);
            var firstResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
            lastSequence = await processor.Process(select2Script, scope);
            var secondResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();

            // Assert.
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(firstResult.Id, secondResult.Id);
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Assign_To_Variable_And_Then_ReUse_02()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person+=Doe/Jane",
                "/Person+=Doe/Johnny",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var select1Query = "/Person/Doe/";
            var select2Query = "$person <= /Person/Doe\r\n$person/";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var select1Script = _parser.Parse(select1Query, scope).Script;
            var select2Script = _parser.Parse(select2Query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(select1Script, scope);
            var firstResult = await lastSequence.Output.Cast<Node>().ToArray();
            lastSequence = await processor.Process(select2Script, scope);
            var secondResult = await lastSequence.Output.Cast<Node>().ToArray();

            // Assert.
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(3, firstResult.Length);
            Assert.Equal(3, secondResult.Length);
            Assert.Equal(firstResult[0].Id, secondResult[0].Id);
            Assert.Equal(firstResult[1].Id, secondResult[1].Id);
            Assert.Equal(firstResult[2].Id, secondResult[2].Id);
        }


        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Assign_To_Variable_And_Then_ReUse_03()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person+=Doe/Jane",
                "/Person+=Doe/Johnny",
                "/Person+=Janssen/Jan",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var select1Query = "/Person/Doe/";
            var select2Query = "$person <= /Person\r\n$person/Doe/";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var select1Script = _parser.Parse(select1Query, scope).Script;
            var select2Script = _parser.Parse(select2Query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(select1Script, scope);
            var firstResult = await lastSequence.Output.Cast<Node>().ToArray();
            lastSequence = await processor.Process(select2Script, scope);
            var secondResult = await lastSequence.Output.Cast<Node>().ToArray();

            // Assert.
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(3, firstResult.Length);
            Assert.Equal(3, secondResult.Length);
            Assert.Equal(firstResult[0].Id, secondResult[0].Id);
            Assert.Equal(firstResult[1].Id, secondResult[1].Id);
            Assert.Equal(firstResult[2].Id, secondResult[2].Id);
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Assign_To_Variable_And_Then_ReUse_04()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/John",
                "/Person+=Doe/Jane",
                "/Person+=Doe/Johnny",
                "/Person+=Janssen/Jan",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var select1Query = "/Person/Doe/";
            var select2Query = "$person <= /Person\r\n<= id() <= $person/Doe/";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var select1Script = _parser.Parse(select1Query, scope).Script;
            var select2Script = _parser.Parse(select2Query, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(select1Script, scope);
            var firstResult = await lastSequence.Output.Cast<Node>().ToArray();
            lastSequence = await processor.Process(select2Script, scope);
            var secondResult = await lastSequence.Output.Cast<Identifier>().ToArray();

            // Assert.
            Assert.NotNull(firstResult);
            Assert.NotNull(secondResult);
            Assert.Equal(3, firstResult.Length);//, "First result is not correct")
            Assert.Equal(3, secondResult.Length);//, "Second result is not correct")
            Assert.Equal(firstResult[0].Id, secondResult[0]);
            Assert.Equal(firstResult[1].Id, secondResult[1]);
            Assert.Equal(firstResult[2].Id, secondResult[2]);
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Assign_Special_Characters()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new[]
            {
                "/Person+=Doe/\"Jöhn\"",
                "/Person+=Doe/\"Jóhn\"",
                "/Person+=Doe/\"Jähn\"",
                "/Person+=Doe/\"Jánê\"",
                "/Person+=Doe/\"Jöhnny\"",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var selectQuery = "/Person/Doe/";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(selectScript, scope);
            var result = await lastSequence.Output.ToArray();

            // Assert.
            Assert.Equal(5, result.Length);
            Assert.Equal("Jöhn", result.Skip(0).First().ToString());
            Assert.Equal("Jóhn", result.Skip(1).First().ToString());
            Assert.Equal("Jähn", result.Skip(2).First().ToString());
            Assert.Equal("Jánê", result.Skip(3).First().ToString());
            Assert.Equal("Jöhnny", result.Skip(4).First().ToString());
        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Children_Should_Not_Clear_Assigned_Tag()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQuery1 = "/Person += Doe";
            var addQueries2 = new[]
            {
                "/Person+=Doe/John",
                "/Person+=Doe/Jane",
                "/Person+=Doe/Johnny",
            };
            var addQuery2 = string.Join("\r\n", addQueries2);
            var selectQuery = "/Person/Doe#";
            var assignQuery = "/Person/Doe# <= FamilyName";

            var addScript1 = _parser.Parse(addQuery1, scope).Script;
            var addScript2 = _parser.Parse(addQuery2, scope).Script;
            var assignScript = _parser.Parse(assignQuery, scope).Script;
            var selectScript = _parser.Parse(selectQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript1, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(assignScript, scope);
            var result = await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic tagBefore = await lastSequence.Output.SingleOrDefaultAsync();
            lastSequence = await processor.Process(addScript2, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(selectScript, scope);
            dynamic tagAfter = await lastSequence.Output.SingleOrDefaultAsync();

            // Assert.
            Assert.NotEmpty(result);
            Assert.NotNull(tagBefore);
            Assert.NotNull(tagAfter);
            Assert.Equal("FamilyName", tagBefore);
            Assert.Equal("FamilyName", tagAfter);
        }


        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Move_Child()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries1 = new []
            {
              "/Person += Doe",
              "/Person += Does"
            };
            var addQueries2 = new[]
            {
                "/Person/Doe += John",
                "/Person/Doe += Johnny",
                "/Person/Does += Jane",
            };

            var moveQueries = new[]
            {
                "$john <= /Person/Doe/John",
                "/Person/Does += $john",
                "/Person/Doe -= John",
            };

            var addQuery1 = string.Join("\r\n", addQueries1);
            var addQuery2 = string.Join("\r\n", addQueries2);
            var moveQuery = string.Join("\r\n", moveQueries);
            var selectQuery1 = "/Person/Doe/";
            var selectQuery2 = "/Person/Does/";

            var addScript1 = _parser.Parse(addQuery1, scope).Script;
            var addScript2 = _parser.Parse(addQuery2, scope).Script;
            var selectScript1 = _parser.Parse(selectQuery1, scope).Script;
            var selectScript2 = _parser.Parse(selectQuery2, scope).Script;
            var moveScript = _parser.Parse(moveQuery, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript1, scope);
            await lastSequence.Output.ToArray();
            lastSequence = await processor.Process(addScript2, scope);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(selectScript1, scope);
            var result = await lastSequence.Output.ToArray();
            var beforeCount1 = result.Length;
            lastSequence = await processor.Process(selectScript2, scope);
            result = await lastSequence.Output.ToArray();
            var beforeCount2 = result.Length;

            lastSequence = await processor.Process(moveScript, scope);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(selectScript1, scope);
            result = await lastSequence.Output.ToArray();
            var afterCount1 = result.Length;
            lastSequence = await processor.Process(selectScript2, scope);
            result = await lastSequence.Output.ToArray();
            var afterCount2 = result.Length;


            // Assert.
            Assert.Equal(2, beforeCount1);
            Assert.Equal(1, beforeCount2);
            Assert.Equal(1, afterCount1);
            Assert.Equal(2, afterCount2);
        }


        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Add_Friends_Using_Variables()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new []
            {
                "/Person+=Doe/John",
                "/Person/Doe/John# <= FirstName",
                "/Person/Doe/John <= { Birthdate: 1977-06-27, NickName: \'Johnny\', Lives: 1 }",
                "/Person/Doe/John += Friends",
                "/Person+=Doe/Jane",
                "/Person/Doe/Jane# <= FirstName",
                "/Person/Doe/Jane <= { Birthdate: 1970-02-03, NickName: \'Janey\', Lives: 2 }",
                "/Person/Doe/Jane += Friends",
                "/Person+=Stark/Tony",
                "/Person/Stark/Tony# <= FirstName",
                "/Person/Stark/Tony <= { Birthdate: 1976-05-12, NickName: \'Iron Man\', Lives: 9 }",
                "/Person/Stark/Tony += Friends",
            };

            var linkQueries = new[]
            {
                "$john <= /Person/Doe/John",
                "$jane <= /Person/Doe/Jane",
                "$tony <= /Person/Stark/Tony",

                "/Person/Stark/Tony/Friends += $john",
                "/Person/Doe/John/Friends += $tony",
                "/Person/Doe/John/Friends += $jane",
                "/Person/Doe/Jane/Friends += $john",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var linkQuery = string.Join("\r\n", linkQueries);
            var nodeSelectQueryJohn = "/Person/Doe/John";
            var nodeSelectQueryJane = "/Person/Doe/Jane";
            var nodeSelectQueryTony = "/Person/Stark/Tony";

            var friendsSelectQueryJohn = "/Person/Doe/John/Friends/#FirstName";
            var friendsSelectQueryJane = "/Person/Doe/Jane/Friends/#FirstName";
            var friendsSelectQueryTony = "/Person/Stark/Tony/Friends/#FirstName";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var linkScript = _parser.Parse(linkQuery, scope).Script;
            var nodeSelectScriptJohn = _parser.Parse(nodeSelectQueryJohn, scope).Script;
            var nodeSelectScriptJane = _parser.Parse(nodeSelectQueryJane, scope).Script;
            var nodeSelectScriptTony = _parser.Parse(nodeSelectQueryTony, scope).Script;
            var friendsSelectScriptJohn = _parser.Parse(friendsSelectQueryJohn, scope).Script;
            var friendsSelectScriptJane = _parser.Parse(friendsSelectQueryJane, scope).Script;
            var friendsSelectScriptTony = _parser.Parse(friendsSelectQueryTony, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(nodeSelectScriptJohn, scope);
            var result = await lastSequence.Output.ToArray();
            var beforeNodeCountJohn = result.Length;
            lastSequence = await processor.Process(nodeSelectScriptJane, scope);
            result = await lastSequence.Output.ToArray();
            var beforeNodeCountJane = result.Length;
            lastSequence = await processor.Process(nodeSelectScriptTony, scope);
            result = await lastSequence.Output.ToArray();
            var beforeNodeCountTony = result.Length;

            lastSequence = await processor.Process(friendsSelectScriptJohn, scope);
            result = await lastSequence.Output.ToArray();
            var beforeFriendCountJohn = result.Length;
            lastSequence = await processor.Process(friendsSelectScriptJane, scope);
            result = await lastSequence.Output.ToArray();
            var beforeFriendCountJane = result.Length;
            lastSequence = await processor.Process(friendsSelectScriptTony, scope);
            result = await lastSequence.Output.ToArray();
            var beforeFriendCountTony = result.Length;

            lastSequence = await processor.Process(linkScript, scope);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(nodeSelectScriptJohn, scope);
            result = await lastSequence.Output.ToArray();
            var afterNodeCountJohn = result.Length;
            lastSequence = await processor.Process(nodeSelectScriptJane, scope);
            result = await lastSequence.Output.ToArray();
            var afterNodeCountJane = result.Length;
            lastSequence = await processor.Process(nodeSelectScriptTony, scope);
            result = await lastSequence.Output.ToArray();
            var afterNodeCountTony = result.Length;

            lastSequence = await processor.Process(friendsSelectScriptJohn, scope);
            result = await lastSequence.Output.ToArray();
            var afterFriendCountJohn = result.Length;
            lastSequence = await processor.Process(friendsSelectScriptJane, scope);
            result = await lastSequence.Output.ToArray();
            var afterFriendCountJane = result.Length;
            lastSequence = await processor.Process(friendsSelectScriptTony, scope);
            result = await lastSequence.Output.ToArray();
            var afterFriendCountTony = result.Length;

            // Assert.
            Assert.Equal(1, beforeNodeCountJohn);
            Assert.Equal(1, beforeNodeCountJane);
            Assert.Equal(1, beforeNodeCountTony);
            Assert.Equal(0, beforeFriendCountJohn);
            Assert.Equal(0, beforeFriendCountJane);
            Assert.Equal(0, beforeFriendCountTony);

            Assert.Equal(1, afterNodeCountJohn);
            Assert.Equal(1, afterNodeCountJane);
            Assert.Equal(1, afterNodeCountTony);
            Assert.Equal(2, afterFriendCountJohn);
            Assert.Equal(1, afterFriendCountJane);
            Assert.Equal(1, afterFriendCountTony);

        }

        [Fact]
        public async Task ScriptProcessor_NonRootedPath_Add_Friends_Using_Paths()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Logical
                .CreateLogicalOptionsWithConnection(true)
                .ConfigureAwait(false);
            var addQueries = new []
            {
                "/Person+=Doe/John",
                "/Person/Doe/John# <= FirstName",
                "/Person/Doe/John <= { Birthdate: 1977-06-27, NickName: \'Johnny\', Lives: 1 }",
                "/Person/Doe/John += Friends",
                "/Person+=Doe/Jane",
                "/Person/Doe/Jane# <= FirstName",
                "/Person/Doe/Jane <= { Birthdate: 1970-02-03, NickName: \'Janey\', Lives: 2 }",
                "/Person/Doe/Jane += Friends",
                "/Person+=Stark/Tony",
                "/Person/Stark/Tony# <= FirstName",
                "/Person/Stark/Tony <= { Birthdate: 1976-05-12, NickName: \'Iron Man\', Lives: 9 }",
                "/Person/Stark/Tony += Friends",

                "/Person/Doe# <= FamilyName",
                "/Person/Stark# <= FamilyName",
            };

            var linkQueries = new[]
            {
                "/Person/Stark/Tony/Friends += /Person/Doe/John",
                "/Person/Doe/John/Friends += /Person/Stark/Tony",
                "/Person/Doe/John/Friends += /Person/Doe/Jane",
                "/Person/Doe/Jane/Friends += /Person/Doe/John",
            };

            var addQuery = string.Join("\r\n", addQueries);
            var linkQuery = string.Join("\r\n", linkQueries);
            var nodeSelectQueryJohn = "/Person/Doe/John";
            var nodeSelectQueryJane = "/Person/Doe/Jane";
            var nodeSelectQueryTony = "/Person/Stark/Tony";

            var friendsSelectQueryJohn = "/Person/Doe/John/Friends/#FirstName";
            var friendsSelectQueryJane = "/Person/Doe/Jane/Friends/#FirstName";
            var friendsSelectQueryTony = "/Person/Stark/Tony/Friends/#FirstName";

            var addScript = _parser.Parse(addQuery, scope).Script;
            var linkScript = _parser.Parse(linkQuery, scope).Script;
            var nodeSelectScriptJohn = _parser.Parse(nodeSelectQueryJohn, scope).Script;
            var nodeSelectScriptJane = _parser.Parse(nodeSelectQueryJane, scope).Script;
            var nodeSelectScriptTony = _parser.Parse(nodeSelectQueryTony, scope).Script;
            var friendsSelectScriptJohn = _parser.Parse(friendsSelectQueryJohn, scope).Script;
            var friendsSelectScriptJane = _parser.Parse(friendsSelectQueryJane, scope).Script;
            var friendsSelectScriptTony = _parser.Parse(friendsSelectQueryTony, scope).Script;
            var processor = _testContext.CreateScriptProcessor(logicalOptions);

            // Act.
            var lastSequence = await processor.Process(addScript, scope);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(nodeSelectScriptJohn, scope);
            var result = await lastSequence.Output.ToArray();
            var beforeNodeCountJohn = result.Length;
            lastSequence = await processor.Process(nodeSelectScriptJane, scope);
            result = await lastSequence.Output.ToArray();
            var beforeNodeCountJane = result.Length;
            lastSequence = await processor.Process(nodeSelectScriptTony, scope);
            result = await lastSequence.Output.ToArray();
            var beforeNodeCountTony = result.Length;

            lastSequence = await processor.Process(friendsSelectScriptJohn, scope);
            result = await lastSequence.Output.ToArray();
            var beforeFriendCountJohn = result.Length;
            lastSequence = await processor.Process(friendsSelectScriptJane, scope);
            result = await lastSequence.Output.ToArray();
            var beforeFriendCountJane = result.Length;
            lastSequence = await processor.Process(friendsSelectScriptTony, scope);
            result = await lastSequence.Output.ToArray();
            var beforeFriendCountTony = result.Length;

            lastSequence = await processor.Process(linkScript, scope);
            await lastSequence.Output.ToArray();

            lastSequence = await processor.Process(nodeSelectScriptJohn, scope);
            result = await lastSequence.Output.ToArray();
            var afterNodeCountJohn = result.Length;
            lastSequence = await processor.Process(nodeSelectScriptJane, scope);
            result = await lastSequence.Output.ToArray();
            var afterNodeCountJane = result.Length;
            lastSequence = await processor.Process(nodeSelectScriptTony, scope);
            result = await lastSequence.Output.ToArray();
            var afterNodeCountTony = result.Length;

            lastSequence = await processor.Process(friendsSelectScriptJohn, scope);
            var afterFriendsJohn = result = await lastSequence.Output.ToArray();
            var afterFriendCountJohn = result.Length;
            lastSequence = await processor.Process(friendsSelectScriptJane, scope);
            result = await lastSequence.Output.ToArray();
            var afterFriendCountJane = result.Length;
            lastSequence = await processor.Process(friendsSelectScriptTony, scope);
            result = await lastSequence.Output.ToArray();
            var afterFriendCountTony = result.Length;

            // Assert.
            Assert.NotNull(afterFriendsJohn);
            Assert.Equal(1, beforeNodeCountJohn);
            Assert.Equal(1, beforeNodeCountJane);
            Assert.Equal(1, beforeNodeCountTony);
            Assert.Equal(0, beforeFriendCountJohn);
            Assert.Equal(0, beforeFriendCountJane);
            Assert.Equal(0, beforeFriendCountTony);

            Assert.Equal(1, afterNodeCountJohn);
            Assert.Equal(1, afterNodeCountJane);
            Assert.Equal(1, afterNodeCountTony);
            Assert.Equal(2, afterFriendCountJohn);
            Assert.Equal(1, afterFriendCountJane);
            Assert.Equal(1, afterFriendCountTony);

        }
    }
}
