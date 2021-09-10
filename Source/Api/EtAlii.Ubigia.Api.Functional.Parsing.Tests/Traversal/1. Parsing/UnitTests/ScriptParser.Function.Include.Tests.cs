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
    public class ScriptParserFunctionIncludeTests : IClassFixture<FunctionalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;

        public ScriptParserFunctionIncludeTests(FunctionalUnitTestContext testContext)
        {
            _parser = testContext.CreateScriptParser();
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact(Skip = "Unable to validate functions during parsing (yet)")]
        public void ScriptParser_Function_Include_Invalid_01()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "include() <= time:now";

            // Act.
            var parseResult = _parser.Parse(text, scope);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

#if USE_LAPA_PARSING_IN_TESTS
        // The test below only works on the Antlr4 parser. We still keep it in as the outcome is better than that of the Lapa parser.

        [Fact]
        public void ScriptParser_Function_Include_Invalid_02()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "include(\"12\",\"\") <= time:now";

            // Act.
            var parseResult = _parser.Parse(text, scope);

            // Assert.
            Assert.Single(parseResult.Errors);
        }
#endif

        [Fact(Skip = "Unable to validate functions during parsing (yet)")]
        public void ScriptParser_Function_Include_Invalid_03()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "include(\"12\",/) <= time:now";

            // Act.
            var parseResult = _parser.Parse(text, scope);

            // Assert.
            Assert.Single(parseResult.Errors);
        }

        [Fact]
        public void ScriptParser_Function_Include_01()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "include(/) <= time:now";

            // Act.
            var result = _parser.Parse(text, scope);

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

        [Fact]
        public void ScriptParser_Function_Include_02()
        {
            // Arrange.
            const string text = "include(/12) <= time:now";

            // Act.
            var scope = new ExecutionScope();
            var result = _parser.Parse(text, scope);

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

        [Fact]
        public void ScriptParser_Function_Include_03()
        {
            // Arrange.
            const string text = "include(/12/*) <= time:now";

            // Act.
            var scope = new ExecutionScope();
            var result = _parser.Parse(text, scope);

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

        [Fact]
        public void ScriptParser_Function_Include_04()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "include(\\12/*) <= time:now";

            // Act.
            var result = _parser.Parse(text, scope);

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

        [Fact]
        public void ScriptParser_Function_Include_05()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "include(\\*/*) <= time:now";

            // Act.
            var result = _parser.Parse(text, scope);

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
