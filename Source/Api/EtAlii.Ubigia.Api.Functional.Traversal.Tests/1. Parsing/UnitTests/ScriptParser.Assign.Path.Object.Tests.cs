// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptParserAssignPathObjectTests : IDisposable
    {
        private IScriptParser _parser;

        public ScriptParserAssignPathObjectTests()
        {
            var diagnostics = DiagnosticsConfiguration.Default;
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_Empty()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.IsType<ObjectConstantSubject>(sequence.Parts.Skip(2).First());
            Assert.Equal(0, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values.Count);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_Blank_DoubleQuotes()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= \"\"");
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.IsType<StringConstantSubject>(sequence.Parts.Skip(2).First());
            Assert.Equal("", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_Blank_SingleQuotes()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= ''");
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.IsType<StringConstantSubject>(sequence.Parts.Skip(2).First());
            Assert.Equal("", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_Space_Newline_Space_Newline()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n \r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Blank_Value()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_01()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_01_Blank_Value()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n\r\n" +
                                       "    LastName: \"\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_01()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n    " +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_02()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",    \r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_03()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\"   ,\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_04()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName:     \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_05()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName   : \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_06()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n   " +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_07()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {      \r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_08()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <=    {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_02()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_03()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_Quoted_Key_DoubleQuotes()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    \"FirstName\": \"John\",\r\n" +
                                       "    \"LastName\": \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_Quoted_Key_SingleQuotes_1()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    'FirstName': \"John\",\r\n" +
                                       "    'LastName': \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_Quoted_Key_SingleQuotes_2()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= {\r\n" +
                                       "    'FirstName': 'John',\r\n" +
                                       "    'LastName': 'Doe'\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }





        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_NonQuoted_Key()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { FirstName: \"John\", LastName: \"Doe\" }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_NonQuoted_Key_Blank_Value()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { FirstName: \"John\", LastName: \"\" }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Quoted_Key_DoubleQuotes()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { \"FirstName\": \"John\", \"LastName\": \"Doe\" }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Quoted_Key_SingleQuotes_1()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { 'FirstName': \"John\", 'LastName': \"Doe\" }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Quoted_Key_SingleQuotes_2()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { 'FirstName': 'John', 'LastName': 'Doe' }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }



        // Type checks.


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_String()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { StringValue1: 'John', StringValue2: 'Doe' }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue1"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue2"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Int()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { IntValue1: 11, IntValue2: 22 }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal(11, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue1"]);
            Assert.Equal(22, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue2"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Int_Positive()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { IntValue1: +11, IntValue2: +22 }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal(11, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue1"]);
            Assert.Equal(22, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue2"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Int_Negative()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { IntValue1: -11, IntValue2: -22 }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal(-11, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue1"]);
            Assert.Equal(-22, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue2"]);
        }




        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Float()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { FloatValue1: 11.11, FloatValue2: 22.22 }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal(11.11f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue1"]);
            Assert.Equal(22.22f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue2"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Float_Positive()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { FloatValue1: +11.11, FloatValue2: +22.22 }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal(11.11f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue1"]);
            Assert.Equal(22.22f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue2"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Float_Negative()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { FloatValue1: -11.11, FloatValue2: -22.22 }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal(-11.11f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue1"]);
            Assert.Equal(-22.22f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue2"]);
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Bool()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Person/Doe/John <= { BoolValue1: true, BoolValue2: false }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.True((bool) sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue1"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Spaced()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { \"Bool Value\": true, BoolValue2: false }");
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

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Special_01()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { \"Bool! Value\": true, BoolValue2: false }");
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

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Special_02()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { \"Bool* Value\": true, BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.True((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Bool* Value"]);
            Assert.False((bool) sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Special_03()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { \"Bool% Value\": true, BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.True((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Bool% Value"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Special_04()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { \"Booléáú Value\": true, BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.True((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Booléáú Value"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Special_05()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { \"Booleaöûêän Value\": true, BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.True((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Booleaöûêän Value"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }





        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Value_Special_01()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { StringValue: \"Bool! Value\", BoolValue2: false }");
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

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Value_Special_02()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { StringValue: \"Bool* Value\", BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Bool* Value", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Value_Special_03()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { StringValue: \"Bool% Value\", BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Bool% Value", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Value_Special_04()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { StringValue: \"Booléáú Value\", BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.IsType<AbsolutePathSubject>(sequence.Parts.Skip(0).First());
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("Booléáú Value", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue"]);
            Assert.False((bool)sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Value_Special_05()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Person/Doe/John <= { StringValue: \"Booleaöûêän Value\", BoolValue2: false }");
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
