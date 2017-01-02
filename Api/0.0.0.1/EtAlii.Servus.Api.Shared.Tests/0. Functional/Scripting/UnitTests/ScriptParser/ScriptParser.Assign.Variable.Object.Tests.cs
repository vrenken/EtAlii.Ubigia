// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class ScriptParser_Assign_Variable_Object_Tests
    {
        private IScriptParser _parser;

        [TestInitialize]
        public void Initialize()
        {
            var diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parser = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_Empty()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.Skip(2).First(), typeof(ObjectConstantSubject));
            Assert.AreEqual(0, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values.Count);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Blank_SingleQuotes()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= ''").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.Skip(2).First(), typeof(StringConstantSubject));
            Assert.AreEqual("", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Blank_DoubleQuotes()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= \"\"").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.Skip(2).First(), typeof(StringConstantSubject));
            Assert.AreEqual("", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
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
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
            Assert.AreEqual(new DateTime(1978, 07, 28), sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Birthdate"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
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
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
            Assert.AreEqual(new DateTime(1978, 07, 28), sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Birthdate"]);
        }



        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_01()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName: \"John\",\r\n\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_01()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName: \"John\",\r\n    " +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_02()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName: \"John\",    \r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_03()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName: \"John\"   ,\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_04()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName:     \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_05()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName   : \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_06()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n   " +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_07()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {      \r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_08()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <=    {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_02()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_03()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_Quoted_Key_DoubleQuotes()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    \"FirstName\": \"John\",\r\n" +
                                       "    \"LastName\": \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_Quoted_Key_SingleQuotes_1()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    'FirstName': \"John\",\r\n" +
                                       "    'LastName': \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_MultiLine_Quoted_Key_SingleQuotes_2()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= {\r\n" +
                                       "    'FirstName': 'John',\r\n" +
                                       "    'LastName': 'Doe'\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }





        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_SingleLine_NonQuoted_Key()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= { FirstName: \"John\", LastName: \"Doe\" }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_SingleLine_Quoted_Key_DoubleQuotes()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= { \"FirstName\": \"John\", \"LastName\": \"Doe\" }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_SingleLine_Quoted_Key_SingleQuotes_1()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= { 'FirstName': \"John\", 'LastName': \"Doe\" }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_SingleLine_Quoted_Key_SingleQuotes_2()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= { 'FirstName': 'John', 'LastName': 'Doe' }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_SingleLine_NonQuoted_Key_DateTime_01()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= { FirstName: \"John\", LastName: \"Doe\", Birthdate: 28-07-1978 }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
            Assert.AreEqual(new DateTime(1978, 07, 28), sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Birthdate"]);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Variable_From_Object_SingleLine_NonQuoted_Key_DateTime_02()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("$johnDoe <= { FirstName: \"John\", LastName: \"Doe\", Birthdate: 1978-07-28 }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.AreEqual("johnDoe", sequence.Parts.Skip(0).Cast<VariableSubject>().First().Name);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
            Assert.AreEqual(new DateTime(1978, 07, 28), sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Birthdate"]);
        }

    }
}