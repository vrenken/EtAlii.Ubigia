﻿// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptParserAssignVariableObjectTests : IDisposable
    {
        private IScriptParser _parser;

        public ScriptParserAssignVariableObjectTests()
        {
            var diagnostics = DiagnosticsConfiguration.Default;
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .UseFunctionalDiagnostics(diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        public void Dispose()
        {
            _parser = null;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_Empty()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.IsType<ObjectConstantSubject>(sequence.Parts.Skip(2).First());
            Assert.Equal(0, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values.Count);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Blank_SingleQuotes()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= ''").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.IsType<StringConstantSubject>(sequence.Parts.Skip(2).First());
            Assert.Equal("", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Blank_DoubleQuotes()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= \"\"").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.IsType<StringConstantSubject>(sequence.Parts.Skip(2).First());
            Assert.Equal("", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Datetime_01()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\",\r\n" +
                                       "    Birthdate: 1978-07-28\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
            Assert.Equal(new DateTime(1978, 07, 28), sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Birthdate"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Datetime_02()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\",\r\n" +
                                       "    Birthdate: 28-07-1978\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
            Assert.Equal(new DateTime(1978, 07, 28), sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Birthdate"]);
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_01()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName: \"John\",\r\n\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_01()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName: \"John\",\r\n    " +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_02()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName: \"John\",    \r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_03()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName: \"John\"   ,\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_04()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName:     \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_05()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName   : \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_06()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n   " +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_07()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {      \r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_08()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <=    {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_02()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_03()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_Quoted_Key_DoubleQuotes()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    \"FirstName\": \"John\",\r\n" +
                                       "    \"LastName\": \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_Quoted_Key_SingleQuotes_1()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    'FirstName': \"John\",\r\n" +
                                       "    'LastName': \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_Quoted_Key_SingleQuotes_2()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    'FirstName': 'John',\r\n" +
                                       "    'LastName': 'Doe'\r\n" +
                                       "}").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }





        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_SingleLine_NonQuoted_Key()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= { FirstName: \"John\", LastName: \"Doe\" }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_SingleLine_Quoted_Key_DoubleQuotes()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= { \"FirstName\": \"John\", \"LastName\": \"Doe\" }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_SingleLine_Quoted_Key_SingleQuotes_1()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= { 'FirstName': \"John\", 'LastName': \"Doe\" }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_SingleLine_Quoted_Key_SingleQuotes_2()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= { 'FirstName': 'John', 'LastName': 'Doe' }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_SingleLine_NonQuoted_Key_DateTime_01()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= { FirstName: \"John\", LastName: \"Doe\", Birthdate: 28-07-1978 }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
            Assert.Equal(new DateTime(1978, 07, 28), sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Birthdate"]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_SingleLine_NonQuoted_Key_DateTime_02()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= { FirstName: \"John\", LastName: \"Doe\", Birthdate: 1978-07-28 }").Script;

            // Assert.
            Assert.Single(script.Sequences);
            var sequence = script.Sequences.First();
            Assert.Equal("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.Skip(1).First());
            Assert.Equal("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.Equal("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
            Assert.Equal(new DateTime(1978, 07, 28), sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Birthdate"]);
        }

    }
}