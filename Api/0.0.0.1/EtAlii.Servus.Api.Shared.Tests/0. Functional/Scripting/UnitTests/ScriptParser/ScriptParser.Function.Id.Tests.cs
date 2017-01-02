namespace EtAlii.Servus.Api.Functional.Tests
{
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class ScriptParser_Function_Id_Tests
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
        public void ScriptParser_Function_Id_Assign()
        {
            // Arrange.
            const string text = "id() <= /Hierarchy";

            // Act.
            var result = _parser.Parse(text);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("id", part.Name);
            Assert.AreEqual(0, part.Arguments.Length);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Id_Variable()
        {
            // Arrange.
            const string query = "id($path)";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("id", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(VariableFunctionSubjectArgument));
            Assert.AreEqual("path", ((VariableFunctionSubjectArgument)part.Arguments[0]).Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Id_Constant_SingleQuoted()
        {
            // Arrange.
            const string query = "id('/Hierarchy')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("id", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("/Hierarchy", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Id_Constant_SingleQuoted_Special_Characters()
        {
            // Arrange.
            const string query = "id('/Hierarchy äëöüáéóúâêôû')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("id", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("/Hierarchy äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Id_Constant_DoubleQuoted()
        {
            // Arrange.
            const string query = "id(\"/Hierarchy\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("id", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("/Hierarchy", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Id_Constant_DoubleQuoted_Special_Characters()
        {
            // Arrange.
            const string query = "id(\"/Hierarchy äëöüáéóúâêôû\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("id", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("/Hierarchy äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }
        

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Id_Path_01()
        {
            // Arrange.
            const string query = "id(/Hierarchy)";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("id", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(PathFunctionSubjectArgument));
            Assert.IsInstanceOfType(((PathFunctionSubjectArgument)part.Arguments[0]).Parts[0], typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Hierarchy", ((ConstantPathSubjectPart)((PathFunctionSubjectArgument)part.Arguments[0]).Parts[1]).Name);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Id_Path_02()
        {
            // Arrange.
            const string query = "id(/Hierarchy/Child)";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("id", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(PathFunctionSubjectArgument));
            Assert.IsInstanceOfType(((PathFunctionSubjectArgument)part.Arguments[0]).Parts[0], typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Hierarchy", ((ConstantPathSubjectPart)((PathFunctionSubjectArgument)part.Arguments[0]).Parts[1]).Name);
            Assert.IsInstanceOfType(((PathFunctionSubjectArgument)part.Arguments[0]).Parts[2], typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Child", ((ConstantPathSubjectPart)((PathFunctionSubjectArgument)part.Arguments[0]).Parts[3]).Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Id_Path_03()
        {
            // Arrange.
            const string query = "id(/Hierarchy/Child/$var/MoreChildren)";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("id", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(PathFunctionSubjectArgument));
            Assert.IsInstanceOfType(((PathFunctionSubjectArgument)part.Arguments[0]).Parts[0], typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Hierarchy", ((ConstantPathSubjectPart)((PathFunctionSubjectArgument)part.Arguments[0]).Parts[1]).Name);
            Assert.IsInstanceOfType(((PathFunctionSubjectArgument)part.Arguments[0]).Parts[2], typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("Child", ((ConstantPathSubjectPart)((PathFunctionSubjectArgument)part.Arguments[0]).Parts[3]).Name);
            Assert.IsInstanceOfType(((PathFunctionSubjectArgument)part.Arguments[0]).Parts[4], typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("var", ((VariablePathSubjectPart)((PathFunctionSubjectArgument)part.Arguments[0]).Parts[5]).Name);
            Assert.IsInstanceOfType(((PathFunctionSubjectArgument)part.Arguments[0]).Parts[6], typeof(IsParentOfPathSubjectPart));
            Assert.AreEqual("MoreChildren", ((ConstantPathSubjectPart)((PathFunctionSubjectArgument)part.Arguments[0]).Parts[7]).Name);
        }






        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Id_Assign_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id('First') <= /Hierarchy";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Id_Assign_Invalid_Faulty_Arguments()
        {
            // Arrange.
            const string text = "id('First', 'Second') <= /Hierarchy";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Id_Variable_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id($path, 'First')";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Id_Variable_Invalid_Faulty_Arguments()
        {
            // Arrange.
            const string text = "id($path, 'First', 'Second')";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Id_Constant_SingleQuoted_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id('/Hierarchy', 'First')";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Id_Constant_SingleQuoted_Invalid_Faulty_Arguments()
        {
            // Arrange.
            const string text = "id('/Hierarchy', 'First', 'Second')";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Id_Constant_DoubleQuoted_Invalid_Faulty_Argument()
        {
            // Arrange.
            const string text = "id(\"/Hierarchy\", 'First')";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Id_Constant_DoubleQuoted_Invalid_Faulty_Arguments()
        {
            // Arrange.
            const string text = "id(\"/Hierarchy\", 'First', 'Second')";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }
    }
}