namespace EtAlii.Servus.Api.Functional.Tests
{
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ScriptParser_Function_Tests
    {
        private IScriptParser _parser;

        [TestInitialize]
        public void Initialize() 
        {
            var diagnostics = TestDiagnostics.Create();
            var functionHandlers = new FunctionHandlerFactory().CreateForTesting();
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(diagnostics)
                .Use(functionHandlersProvider);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parser = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Function()
        {
            // Arrange.
            const string query = "function()";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(0, part.Arguments.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Invalid_01()
        {
            // Arrange.
            const string text = "function()()";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Invalid_02()
        {
            // Arrange.
            const string text = "()function";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Invalid_03()
        {
            // Arrange.
            const string text = "(function)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_SingleQuoted()
        {
            // Arrange.
            const string query = "function('First')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_DoubleQuoted()
        {
            // Arrange.
            const string query = "function(\"First\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_SingleQuoted_Special_Characters()
        {
            // Arrange.
            const string query = "function('First äëöüáéóúâêôû')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_DoubleQuoted_Special_Characters()
        {
            // Arrange.
            const string query = "function(\"First äëöüáéóúâêôû\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }


        


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_SingleQuoted_Invalid_01()
        {
            // Arrange.
            const string text = "function('First'')";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_SingleQuoted_Invalid_02()
        {
            // Arrange.
            const string text = "function(''First')";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_SingleQuoted_Invalid_03()
        {
            // Arrange.
            const string text = "function('Fi'rst')";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_DoubleQuoted_Invalid_01()
        {
            // Arrange.
            const string text = "function(\"First\"\")";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_DoubleQuoted_Invalid_02()
        {
            // Arrange.
            const string text = "function(\"\"First\")";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_DoubleQuoted_Invalid_03()
        {
            // Arrange.
            const string text = "function(\"Fi\"rst\")";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }



        
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_DoubleQuoted_Invalid_Special_Characters_01()
        {
            // Arrange.
            const string text = "function(\"First\" äëöüáéóúâêôû\")";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_DoubleQuoted_Invalid_Special_Characters_02()
        {
            // Arrange.
            const string text = "function(\"\"First äëöüáéóúâêôû\")";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Single_DoubleQuoted_Invalid_Special_Characters_03()
        {
            // Arrange.
            const string text = "function(\"Fi\"rst äëöüáéóúâêôû\")";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }



        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Multiple_SingleQuoted()
        {
            // Arrange.
            const string query = "function('First','Second', 'Third')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(3, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsInstanceOfType(part.Arguments[1], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Second", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsInstanceOfType(part.Arguments[2], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Third", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Multiple_DoubleQuoted()
        {
            // Arrange.
            const string query = "function(\"First\",\"Second\", \"Third\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(3, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsInstanceOfType(part.Arguments[1], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Second", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsInstanceOfType(part.Arguments[2], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Third", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Multiple_SingleQuoted_Special_Characters()
        {
            // Arrange.
            const string query = "function('First äëöüáéóúâêôû','Second äëöüáéóúâêôû', 'Third äëöüáéóúâêôû')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(3, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsInstanceOfType(part.Arguments[1], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Second äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsInstanceOfType(part.Arguments[2], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Third äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Constant_Parameter_Multiple_DoubleQuoted_Special_Characters()
        {
            // Arrange.
            const string query = "function(\"First äëöüáéóúâêôû\",\"Second äëöüáéóúâêôû\", \"Third äëöüáéóúâêôû\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(3, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsInstanceOfType(part.Arguments[1], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Second äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsInstanceOfType(part.Arguments[2], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Third äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }











        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Assignment()
        {
            // Arrange.
            const string query = "/Document/Images <= function()";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(2).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(0, part.Arguments.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Assignment_Constant_Parameter_Single_SingleQuoted()
        {
            // Arrange.
            const string query = "/Document/Images <= function('First')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(2).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Assignment_Constant_Parameter_Single_DoubleQuoted()
        {
            // Arrange.
            const string query = "/Document/Images <= function(\"First\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(2).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Assignment_Constant_Parameter_Multiple_SingleQuoted()
        {
            // Arrange.
            const string query = "/Document/Images <= function('First','Second', 'Third')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(2).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(3, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsInstanceOfType(part.Arguments[1], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Second", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsInstanceOfType(part.Arguments[2], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Third", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Assignment_Constant_Parameter_Multiple_SingleQuoted_Special_Characters()
        {
            // Arrange.
            const string query = "/Document/Images <= function('First äëöüáéóúâêôû','Second äëöüáéóúâêôû', 'Third äëöüáéóúâêôû')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(2).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(3, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsInstanceOfType(part.Arguments[1], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Second äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsInstanceOfType(part.Arguments[2], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Third äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Assignment_Constant_Parameter_Multiple_DoubleQuoted()
        {
            // Arrange.
            const string query = "/Document/Images <= function(\"First\",\"Second\", \"Third\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(2).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(3, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsInstanceOfType(part.Arguments[1], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Second", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsInstanceOfType(part.Arguments[2], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Third", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Function_Assignment_Constant_Parameter_Multiple_DoubleQuoted_Special_Characters()
        {
            // Arrange.
            const string query = "/Document/Images <= function(\"First äëöüáéóúâêôû\",\"Second äëöüáéóúâêôû\", \"Third äëöüáéóúâêôû\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(2).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(3, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsInstanceOfType(part.Arguments[1], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Second äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsInstanceOfType(part.Arguments[2], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Third äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }



















        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Process()
        {
            // Arrange.
            const string query = "function() <= /Document/Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(0, part.Arguments.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Process_Constant_Parameter_Single_SingleQuoted()
        {
            // Arrange.
            const string query = "function('First') <= /Document/Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Process_Constant_Parameter_Single_DoubleQuoted()
        {
            // Arrange.
            const string query = "function(\"First\") <= /Document/Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Process_Constant_Parameter_Multiple_SingleQuoted()
        {
            // Arrange.
            const string query = "function('First','Second', 'Third') <= /Document/Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(3, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsInstanceOfType(part.Arguments[1], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Second", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsInstanceOfType(part.Arguments[2], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Third", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Process_Constant_Parameter_Multiple_DoubleQuoted()
        {
            // Arrange.
            const string query = "function(\"First\",\"Second\", \"Third\") <= /Document/Images";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(3, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
            Assert.IsInstanceOfType(part.Arguments[1], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Second", ((ConstantFunctionSubjectArgument)part.Arguments[1]).Value);
            Assert.IsInstanceOfType(part.Arguments[2], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Third", ((ConstantFunctionSubjectArgument)part.Arguments[2]).Value);
        }






        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Single()
        {
            // Arrange.
            const string query = "function($var1)";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(1, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(VariableFunctionSubjectArgument));
            Assert.AreEqual("var1", ((VariableFunctionSubjectArgument)part.Arguments[0]).Name);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Single_Invalid_01()
        {
            // Arrange.
            const string text = "function($var1$var2)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Single_Invalid_02()
        {
            // Arrange.
            const string text = "function($$var1)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Single_Invalid_03()
        {
            // Arrange.
            const string text = "function(\"\"$var1)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Single_Invalid_04()
        {
            // Arrange.
            const string text = "function(''$var1)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple()
        {
            // Arrange.
            const string query = "function($var1, $var2)";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsTrue(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.IsNotNull(part);
            Assert.AreEqual("function", part.Name);
            Assert.AreEqual(2, part.Arguments.Length);
            Assert.IsInstanceOfType(part.Arguments[0], typeof(VariableFunctionSubjectArgument));
            Assert.AreEqual("var1", ((VariableFunctionSubjectArgument)part.Arguments[0]).Name);
            Assert.IsInstanceOfType(part.Arguments[1], typeof(VariableFunctionSubjectArgument));
            Assert.AreEqual("var2", ((VariableFunctionSubjectArgument)part.Arguments[1]).Name);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_01()
        {
            // Arrange.
            const string text = "function($var0, $var1$var2)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_02()
        {
            // Arrange.
            const string text = "function($var0, $$var1)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_03()
        {
            // Arrange.
            const string text = "function($var0, \"\"$var1)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_04()
        {
            // Arrange.
            const string text = "function($var0, ''$var1)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_05()
        {
            // Arrange.
            const string text = "function($var1$var2, var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_06()
        {
            // Arrange.
            const string text = "function($$var1, var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_07()
        {
            // Arrange.
            const string text = "function(\"\"$var1, var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_08()
        {
            // Arrange.
            const string text = "function(''$var1, var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_09()
        {
            // Arrange.
            const string text = "function($var1$, var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_10()
        {
            // Arrange.
            const string text = "function($var1\"\", var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_11()
        {
            // Arrange.
            const string text = "function($var1'', var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_12()
        {
            // Arrange.
            const string text = "function($var0, $var1$)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_13()
        {
            // Arrange.
            const string text = "function($var0, $var1\"\")";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_14()
        {
            // Arrange.
            const string text = "function($var0, $var1'')";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_15()
        {
            // Arrange.
            const string text = "function($var0, $var1,)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_16()
        {
            // Arrange.
            const string text = "function(,$var0, $var1)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_17()
        {
            // Arrange.
            const string text = "function(,$var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_18()
        {
            // Arrange.
            const string text = "function($var0,)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Function_Variable_Parameter_Multiple_Invalid_19()
        {
            // Arrange.
            const string text = "function($ var0)";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.AreEqual(1, parseResult.Errors.Length);
        }
    }
}