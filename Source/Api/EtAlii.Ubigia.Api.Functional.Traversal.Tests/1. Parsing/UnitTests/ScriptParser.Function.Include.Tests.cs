namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ScriptParserFunctionIncludeTests : IDisposable
    {
        private IScriptParser _parser;

        public ScriptParserFunctionIncludeTests()
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

        [Fact(Skip = "Unable to validate functions during parsing (yet)"), Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Include_Invalid_01()
        {
            // Arrange.
            const string text = "include() <= time:now";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Include_Invalid_02()
        {
            // Arrange.
            const string text = "include(\"12\",\"\") <= time:now";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact(Skip = "Unable to validate functions during parsing (yet)"), Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Include_Invalid_03()
        {
            // Arrange.
            const string text = "include(\"12\",/) <= time:now";

            // Act.
            var parseResult = _parser.Parse(text);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Include_01()
        {
            // Arrange.
            const string text = "include(/) <= time:now";

            // Act.
            var result = _parser.Parse(text);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("include", part.Name);
            Assert.Single(part.Arguments);
            Assert.IsType<NonRootedPathFunctionSubjectArgument>(part.Arguments[0]);
            Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Include_02()
        {
            // Arrange.
            const string text = "include(/12) <= time:now";

            // Act.
            var result = _parser.Parse(text);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("include", part.Name);
            Assert.Single(part.Arguments);
            Assert.IsType<NonRootedPathFunctionSubjectArgument>(part.Arguments[0]);
            Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]);
            Assert.IsType<ConstantPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[1]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Include_03()
        {
            // Arrange.
            const string text = "include(/12/*) <= time:now";

            // Act.
            var result = _parser.Parse(text);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("include", part.Name);
            Assert.Single(part.Arguments);
            Assert.IsType<NonRootedPathFunctionSubjectArgument>(part.Arguments[0]);
            Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]);
            Assert.IsType<ConstantPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[1]);
            Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[2]);
            Assert.IsType<WildcardPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[3]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Include_04()
        {
            // Arrange.
            const string text = "include(\\12/*) <= time:now";

            // Act.
            var result = _parser.Parse(text);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("include", part.Name);
            Assert.Single(part.Arguments);
            Assert.IsType<NonRootedPathFunctionSubjectArgument>(part.Arguments[0]);
            Assert.IsType<ChildrenPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]);
            Assert.IsType<ConstantPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[1]);
            Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[2]);
            Assert.IsType<WildcardPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[3]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Function_Include_05()
        {
            // Arrange.
            const string text = "include(\\*/*) <= time:now";

            // Act.
            var result = _parser.Parse(text);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.True(script.Sequences.Count() == 1);
            var sequence = script.Sequences.First();
            var part = sequence.Parts.First() as FunctionSubject;
            Assert.NotNull(part);
            Assert.Equal("include", part.Name);
            Assert.Single(part.Arguments);
            Assert.IsType<NonRootedPathFunctionSubjectArgument>(part.Arguments[0]);
            Assert.IsType<ChildrenPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]);
            Assert.IsType<WildcardPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[1]);
            Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[2]);
            Assert.IsType<WildcardPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[3]);
        }
    }
}
