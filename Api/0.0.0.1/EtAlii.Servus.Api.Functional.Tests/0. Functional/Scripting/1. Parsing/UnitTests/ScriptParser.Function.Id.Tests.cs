namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using Xunit;


    public partial class ScriptParser_Function_Id_Tests : IDisposable
    {
        private IScriptParser _parser;

        public ScriptParser_Function_Id_Tests()
        {
            var diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        public void Dispose()
        {
            _parser = null;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Id_Assign()
        {
            // Arrange.
            const string text = "id() <= /Hierarchy";

            // Act.
            var result = _parser.Parse(text);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("id", part.Name);
            Assert.Equal(0, part.Arguments.Length);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Id_Variable()
        {
            // Arrange.
            const string query = "id($path)";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("id", part.Name);
            Assert.Equal(1, part.Arguments.Length);
            Assert.IsType<VariableFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("path", ((VariableFunctionSubjectArgument)part.Arguments[0]).Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Id_Constant_SingleQuoted()
        {
            // Arrange.
            const string query = "id('/Hierarchy')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("id", part.Name);
            Assert.Equal(1, part.Arguments.Length);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("/Hierarchy", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Id_Constant_SingleQuoted_Special_Characters()
        {
            // Arrange.
            const string query = "id('/Hierarchy äëöüáéóúâêôû')";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("id", part.Name);
            Assert.Equal(1, part.Arguments.Length);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("/Hierarchy äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Id_Constant_DoubleQuoted()
        {
            // Arrange.
            const string query = "id(\"/Hierarchy\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("id", part.Name);
            Assert.Equal(1, part.Arguments.Length);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("/Hierarchy", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Id_Constant_DoubleQuoted_Special_Characters()
        {
            // Arrange.
            const string query = "id(\"/Hierarchy äëöüáéóúâêôû\")";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("id", part.Name);
            Assert.Equal(1, part.Arguments.Length);
            Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("/Hierarchy äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
        }
        

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Id_Path_01()
        {
            // Arrange.
            const string query = "id(/Hierarchy)";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("id", part.Name);
            Assert.Equal(1, part.Arguments.Length);
            Assert.IsType<NonRootedPathFunctionSubjectArgument>(part.Arguments[0]);
            Assert.IsType<IsParentOfPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]);
            Assert.Equal("Hierarchy", ((ConstantPathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[1]).Name);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Id_Path_02()
        {
            // Arrange.
            const string query = "id(/Hierarchy/Child)";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("id", part.Name);
            Assert.Equal(1, part.Arguments.Length);
            Assert.IsType<NonRootedPathFunctionSubjectArgument>(part.Arguments[0]);
            Assert.IsType<IsParentOfPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]);
            Assert.Equal("Hierarchy", ((ConstantPathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[1]).Name);
            Assert.IsType<IsParentOfPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[2]);
            Assert.Equal("Child", ((ConstantPathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[3]).Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Id_Path_03()
        {
            // Arrange.
            const string query = "id(/Hierarchy/Child/$var/MoreChildren)";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.Skip(1).First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("id", part.Name);
            Assert.Equal(1, part.Arguments.Length);
            Assert.IsType<NonRootedPathFunctionSubjectArgument>(part.Arguments[0]);
            Assert.IsType<IsParentOfPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]);
            Assert.Equal("Hierarchy", ((ConstantPathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[1]).Name);
            Assert.IsType<IsParentOfPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[2]);
            Assert.Equal("Child", ((ConstantPathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[3]).Name);
            Assert.IsType<IsParentOfPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[4]);
            Assert.Equal("var", ((VariablePathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[5]).Name);
            Assert.IsType<IsParentOfPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[6]);
            Assert.Equal("MoreChildren", ((ConstantPathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[7]).Name);
        }



    }
}