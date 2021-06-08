namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public class ScriptParserFunctionIdTests : IDisposable
    {
        private IScriptParser _parser;

        public ScriptParserFunctionIdTests()
        {
            _parser = new TestScriptParserFactory().Create();
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
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
            Assert.Empty(part.Arguments);
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
            Assert.Single(part.Arguments);
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
            Assert.Single(part.Arguments);
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
            Assert.Single(part.Arguments);
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
            Assert.Single(part.Arguments);
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
            Assert.Single(part.Arguments);
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
            Assert.Single(part.Arguments);
            Assert.IsType<NonRootedPathFunctionSubjectArgument>(part.Arguments[0]);
            Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]);
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
            Assert.Single(part.Arguments);
            Assert.IsType<NonRootedPathFunctionSubjectArgument>(part.Arguments[0]);
            Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]);
            Assert.Equal("Hierarchy", ((ConstantPathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[1]).Name);
            Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[2]);
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
            Assert.Single(part.Arguments);
            Assert.IsType<NonRootedPathFunctionSubjectArgument>(part.Arguments[0]);
            Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]);
            Assert.Equal("Hierarchy", ((ConstantPathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[1]).Name);
            Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[2]);
            Assert.Equal("Child", ((ConstantPathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[3]).Name);
            Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[4]);
            Assert.Equal("var", ((VariablePathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[5]).Name);
            Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[6]);
            Assert.Equal("MoreChildren", ((ConstantPathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[7]).Name);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Id_Path_04()
        {
            // Arrange.
            const string query = "id(time:now)";

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
            Assert.Single(part.Arguments);
            Assert.IsType<RootedPathFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("now", ((ConstantPathSubjectPart)((RootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]).Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Id_Path_05()
        {
            // Arrange.
            const string query = "id(time:\"2016-02-19\")";

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
            Assert.Single(part.Arguments);
            Assert.IsType<RootedPathFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("2016-02-19", ((ConstantPathSubjectPart)((RootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]).Name);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Id_Path_06()
        {
            // Arrange.
            const string query = "id(time:'2016-02-19')";

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
            Assert.Single(part.Arguments);
            Assert.IsType<RootedPathFunctionSubjectArgument>(part.Arguments[0]);
            Assert.Equal("2016-02-19", ((ConstantPathSubjectPart)((RootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]).Name);
        }
    }
}
