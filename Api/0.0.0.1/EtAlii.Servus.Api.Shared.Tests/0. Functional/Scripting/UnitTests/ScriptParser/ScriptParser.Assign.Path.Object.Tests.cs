// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional.Tests
{
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class ScriptParser_Assign_Path_Object_Tests
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
        public void ScriptParser_Assign_Path_From_Object_Empty()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.Skip(2).First(), typeof(ObjectConstantSubject));
            Assert.AreEqual(0, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values.Count);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_Blank_DoubleQuotes()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Contacts/Doe/John <= \"\"");
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.Skip(2).First(), typeof(StringConstantSubject));
            Assert.AreEqual("", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_Blank_SingleQuotes()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Contacts/Doe/John <= ''");
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.Skip(2).First(), typeof(StringConstantSubject));
            Assert.AreEqual("", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_Space_Newline_Space_Newline()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n \r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Blank_Value()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_01()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_01_Blank_Value()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n\r\n" +
                                       "    LastName: \"\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_01()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n    " +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_02()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",    \r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_03()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\"   ,\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_04()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n" +
                                       "    FirstName:     \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_05()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n" +
                                       "    FirstName   : \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_06()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n   " +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_07()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {      \r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Whitespace_08()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <=    {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_02()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_NonQuoted_Key_Extra_Newlines_03()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n" +
                                       "    FirstName: \"John\",\r\n" +
                                       "    LastName: \"Doe\"\r\n\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_Quoted_Key_DoubleQuotes()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n" +
                                       "    \"FirstName\": \"John\",\r\n" +
                                       "    \"LastName\": \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_Quoted_Key_SingleQuotes_1()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n" +
                                       "    'FirstName': \"John\",\r\n" +
                                       "    'LastName': \"Doe\"\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_MultiLine_Quoted_Key_SingleQuotes_2()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= {\r\n" +
                                       "    'FirstName': 'John',\r\n" +
                                       "    'LastName': 'Doe'\r\n" +
                                       "}").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }





        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_NonQuoted_Key()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= { FirstName: \"John\", LastName: \"Doe\" }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_NonQuoted_Key_Blank_Value()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= { FirstName: \"John\", LastName: \"\" }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Quoted_Key_DoubleQuotes()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= { \"FirstName\": \"John\", \"LastName\": \"Doe\" }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Quoted_Key_SingleQuotes_1()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= { 'FirstName': \"John\", 'LastName': \"Doe\" }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Quoted_Key_SingleQuotes_2()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= { 'FirstName': 'John', 'LastName': 'Doe' }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FirstName"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["LastName"]);
        }



        // Type checks.


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_String()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= { StringValue1: 'John', StringValue2: 'Doe' }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("John", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue1"]);
            Assert.AreEqual("Doe", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue2"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Int()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= { IntValue1: 11, IntValue2: 22 }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual(11, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue1"]);
            Assert.AreEqual(22, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue2"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Int_Positive()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= { IntValue1: +11, IntValue2: +22 }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual(11, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue1"]);
            Assert.AreEqual(22, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue2"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Int_Negative()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= { IntValue1: -11, IntValue2: -22 }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual(-11, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue1"]);
            Assert.AreEqual(-22, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["IntValue2"]);
        }




        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Float()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= { FloatValue1: 11.11, FloatValue2: 22.22 }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual(11.11f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue1"]);
            Assert.AreEqual(22.22f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue2"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Float_Positive()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= { FloatValue1: +11.11, FloatValue2: +22.22 }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual(11.11f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue1"]);
            Assert.AreEqual(22.22f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue2"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Float_Negative()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= { FloatValue1: -11.11, FloatValue2: -22.22 }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual(-11.11f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue1"]);
            Assert.AreEqual(-22.22f, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["FloatValue2"]);
        }



        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_SingleLine_Value_Bool()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/Contacts/Doe/John <= { BoolValue1: true, BoolValue2: false }").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual(true, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue1"]);
            Assert.AreEqual(false, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Spaced()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Contacts/Doe/John <= { \"Bool Value\": true, BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.IsNotNull(script, parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual(true, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Bool Value"]);
            Assert.AreEqual(false, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Special_01()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Contacts/Doe/John <= { \"Bool! Value\": true, BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.IsNotNull(script, parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual(true, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Bool! Value"]);
            Assert.AreEqual(false, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Special_02()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Contacts/Doe/John <= { \"Bool* Value\": true, BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual(true, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Bool* Value"]);
            Assert.AreEqual(false, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Special_03()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Contacts/Doe/John <= { \"Bool% Value\": true, BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual(true, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Bool% Value"]);
            Assert.AreEqual(false, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Special_04()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Contacts/Doe/John <= { \"Booléáú Value\": true, BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual(true, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Booléáú Value"]);
            Assert.AreEqual(false, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Key_Special_05()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Contacts/Doe/John <= { \"Booleaöûêän Value\": true, BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual(true, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["Booleaöûêän Value"]);
            Assert.AreEqual(false, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }





        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Value_Special_01()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Contacts/Doe/John <= { StringValue: \"Bool! Value\", BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.IsNotNull(script, parseResult.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("Bool! Value", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue"]);
            Assert.AreEqual(false, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Value_Special_02()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Contacts/Doe/John <= { StringValue: \"Bool* Value\", BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("Bool* Value", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue"]);
            Assert.AreEqual(false, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Value_Special_03()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Contacts/Doe/John <= { StringValue: \"Bool% Value\", BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("Bool% Value", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue"]);
            Assert.AreEqual(false, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Value_Special_04()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Contacts/Doe/John <= { StringValue: \"Booléáú Value\", BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("Booléáú Value", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue"]);
            Assert.AreEqual(false, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Assign_Path_From_Object_With_Quoted_Value_Special_05()
        {
            // Arrange.

            // Act.
            var parseResult = _parser.Parse("/Contacts/Doe/John <= { StringValue: \"Booleaöûêän Value\", BoolValue2: false }");
            var script = parseResult.Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            var sequence = script.Sequences.First();
            Assert.IsInstanceOfType(sequence.Parts.Skip(0).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).First(), typeof(AssignOperator));
            Assert.AreEqual("Booleaöûêän Value", sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["StringValue"]);
            Assert.AreEqual(false, sequence.Parts.Skip(2).Cast<ObjectConstantSubject>().First().Values["BoolValue2"]);
        }

    
    }
}