// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class ScriptParserAssignPathObjectTests : IClassFixture<FunctionalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;

        public ScriptParserAssignPathObjectTests(FunctionalUnitTestContext testContext)
        {
            _parser = testContext.CreateScriptParser();
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_Empty()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.IsType<ObjectConstantSubject>(sequence.Parts.Skip(2).First());
            Assert.Equal(0, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values.Count);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_Blank_DoubleQuotes()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= \"\"", scope);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.IsType<StringConstantSubject>(sequence.Parts.Skip(2).First());
            Assert.Equal("", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_Blank_SingleQuotes()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= ''", scope);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.IsType<StringConstantSubject>(sequence.Parts.Skip(2).First());
            Assert.Equal("", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_Space_Newline_Space_Newline()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n \r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }


        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Blank_Value()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"\"\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_01()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_01_Blank_Value()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n\r\n" +
                                       "    LastName: \"\"\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_01()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n    " +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_02()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",    \r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_03()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\"   ,\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_04()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName:     \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_05()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName   : \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_06()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n   " +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_07()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {      \r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_08()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <=    {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_02()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }


        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_03()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_Quoted_Key_DoubleQuotes()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    \"FirstName\": \"John\",\r\n" +
                                       "    \"LastName\": \"Doe\"\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_Quoted_Key_SingleQuotes_1()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    'FirstName': \"John\",\r\n" +
                                       "    'LastName': \"Doe\"\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_Quoted_Key_SingleQuotes_2()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    'FirstName': 'John',\r\n" +
                                       "    'LastName': 'Doe'\r\n" +
                                       "}", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }





        [Fact]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_NonQuoted_Key()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { FirstName: \"John\", LastName: \"Doe\" }", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_NonQuoted_Key_Blank_Value()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { FirstName: \"John\", LastName: \"\" }", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }
        [Fact]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Quoted_Key_DoubleQuotes()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { \"FirstName\": \"John\", \"LastName\": \"Doe\" }", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Quoted_Key_SingleQuotes_1()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { 'FirstName': \"John\", 'LastName': \"Doe\" }", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Quoted_Key_SingleQuotes_2()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { 'FirstName': 'John', 'LastName': 'Doe' }", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }



        // Type checks.


        [Fact]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_String()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { StringValue1: 'John', StringValue2: 'Doe' }", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue1"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue2"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Int()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { IntValue1: 11, IntValue2: 22 }", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal(11, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue1"]);
            Assert.Equal(22, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue2"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Int_Positive()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { IntValue1: +11, IntValue2: +22 }", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal(11, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue1"]);
            Assert.Equal(22, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue2"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Int_Negative()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { IntValue1: -11, IntValue2: -22 }", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal(-11, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue1"]);
            Assert.Equal(-22, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue2"]);
        }




        [Fact]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Float()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { FloatValue1: 11.11, FloatValue2: 22.22 }", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal(11.11f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue1"]);
            Assert.Equal(22.22f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue2"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Float_Positive()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { FloatValue1: +11.11, FloatValue2: +22.22 }", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal(11.11f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue1"]);
            Assert.Equal(22.22f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue2"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Float_Negative()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { FloatValue1: -11.11, FloatValue2: -22.22 }", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal(-11.11f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue1"]);
            Assert.Equal(-22.22f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue2"]);
        }



        [Fact]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Bool()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { BoolValue1: true, BoolValue2: false }", scope).Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.True((bool) sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue1"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Spaced()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { \"Bool Value\": true, BoolValue2: false }", scope);
            var script = parseResult.Script;

            // Assert.
            Assert.False(parseResult.Errors.Any(), parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.True((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Bool Value"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Special_01()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { \"Bool! Value\": true, BoolValue2: false }", scope);
            var script = parseResult.Script;

            // Assert.
            Assert.False(parseResult.Errors.Any(), parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.True((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Bool! Value"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Special_02()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { \"Bool* Value\": true, BoolValue2: false }", scope);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.True((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Bool* Value"]);
            Assert.False((bool) sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Special_03()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { \"Bool% Value\": true, BoolValue2: false }", scope);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.True((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Bool% Value"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Special_04()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { \"Booléáú Value\": true, BoolValue2: false }", scope);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.True((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Booléáú Value"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Special_05()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { \"Booleaöûêän Value\": true, BoolValue2: false }", scope);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.True((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Booleaöûêän Value"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Value_Special_01()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { StringValue: \"Bool! Value\", BoolValue2: false }", scope);
            var script = parseResult.Script;

            // Assert.
            Assert.False(parseResult.Errors.Any(), parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Bool! Value", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Value_Special_02()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { StringValue: \"Bool* Value\", BoolValue2: false }", scope);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Bool* Value", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Value_Special_03()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { StringValue: \"Bool% Value\", BoolValue2: false }", scope);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Bool% Value", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Value_Special_04()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { StringValue: \"Booléáú Value\", BoolValue2: false }", scope);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Booléáú Value", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Value_Special_05()
        {
            // Arrange.
            var scope = new ExecutionScope();

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { StringValue: \"Booleaöûêän Value\", BoolValue2: false }", scope);
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Booleaöûêän Value", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }
    }
}
