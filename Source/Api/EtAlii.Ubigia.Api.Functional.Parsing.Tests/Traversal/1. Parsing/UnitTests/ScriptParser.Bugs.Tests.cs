// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class ScriptParserBugsTests : IClassFixture<TraversalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;

        public ScriptParserBugsTests(TraversalUnitTestContext testContext)
        {
            _parser = new TestScriptParserFactory().Create(testContext.ClientConfiguration);
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void ScriptParser_Bugs_001_Object_Assignment()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "Person:Doe/John <= { Birthdate: 1977-06-27, NickName: 'Johnny', Lives: 1 }";

            // Act.
            var parseResult = _parser.Parse(text, scope);

            // Assert.
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Script);
        }

        [Fact]
        public void ScriptParser_Bugs_002_Whitespace_between_function_arguments()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "id(\"/Hierarchy\", 'First','Second')";

            // Act.
            var parseResult = _parser.Parse(text, scope);

            // Assert.
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Script);
        }

        [Fact]
        public void ScriptParser_Bugs_002_Whitespace_Between_Function_Arguments()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "id(\"/Hierarchy\", 'First','Second')";

            // Act.
            var parseResult = _parser.Parse(text, scope);

            // Assert.
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Script);
        }

        [Fact]
        public void ScriptParser_Bugs_003_Allow_Unassigned_Object_Properties()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "/Location/Europe/NL/Overijssel/Enschede/Helmerhoek <= { StringValue : , IntValue : }";

            // Act.
            var parseResult = _parser.Parse(text, scope);

            // Assert.
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Script);
        }

        [Fact]
        public void ScriptParser_Bugs_004_Enable_Conditional_Path_Parts()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "Person:Does/.IsMale!=false";

            // Act.
            var parseResult = _parser.Parse(text, scope);

            // Assert.
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Script);
            var rootedPathSubject = Assert.IsType<RootedPathSubject>(parseResult.Script.Sequences.First().Parts[1]);
            var conditionalPathSubjectPart = Assert.IsType<ConditionalPathSubjectPart>(rootedPathSubject.Parts[2]);
            Assert.Equal(ConditionType.NotEqual, conditionalPathSubjectPart.Conditions[0].Type);
            Assert.Equal("IsMale", conditionalPathSubjectPart.Conditions[0].Property);
        }

        [Fact]
        public void ScriptParser_Bugs_005_Enable_Multiple_Conditional_Path_Parts()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "Person:Does/.IsMale!=false&Birthdate=2016-01-21";

            // Act.
            var parseResult = _parser.Parse(text, scope);

            // Assert.
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Script);
            var rootedPathSubject = Assert.IsType<RootedPathSubject>(parseResult.Script.Sequences.First().Parts[1]);
            var conditionalPathSubjectPart = Assert.IsType<ConditionalPathSubjectPart>(rootedPathSubject.Parts[2]);
            Assert.Equal(ConditionType.NotEqual, conditionalPathSubjectPart.Conditions[0].Type);
            Assert.Equal("IsMale", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.Equal(ConditionType.Equal, conditionalPathSubjectPart.Conditions[1].Type);
            Assert.Equal("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
            var date = Assert.IsType<DateTime>(conditionalPathSubjectPart.Conditions[1].Value);
            Assert.Equal(2016, date.Year);
            Assert.Equal(1, date.Month);
            Assert.Equal(21, date.Day);
        }

        [Fact]
        public void ScriptParser_Bugs_006_Enable_Traversing_Wildcards_With_Depth()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "/Person/*2*/Jo*";

            // Act.
            var parseResult = _parser.Parse(text, scope);

            // Assert.
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Script);
            var absolutePathSubject = Assert.IsType<AbsolutePathSubject>(parseResult.Script.Sequences.First().Parts[1]);
            var traversingWildcardPathSubjectPart = Assert.IsType<TraversingWildcardPathSubjectPart>(absolutePathSubject.Parts[3]);
            Assert.Equal(2, traversingWildcardPathSubjectPart.Limit);
            var wildcardPathSubjectPart = Assert.IsType<WildcardPathSubjectPart>(absolutePathSubject.Parts[5]);
            Assert.Equal("Jo*", wildcardPathSubjectPart.Pattern);
        }
        [Fact]
        public void ScriptParser_Bugs_007_Enable_Traversing_Quoted_Wildcards_With_Depth()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "/Person/*2*/\"Jo\"*";

            // Act.
            var parseResult = _parser.Parse(text, scope);

            // Assert.
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Script);
            var absolutePathSubject = Assert.IsType<AbsolutePathSubject>(parseResult.Script.Sequences.First().Parts[1]);
            var traversingWildcardPathSubjectPart = Assert.IsType<TraversingWildcardPathSubjectPart>(absolutePathSubject.Parts[3]);
            Assert.Equal(2, traversingWildcardPathSubjectPart.Limit);
            var wildcardPathSubjectPart = Assert.IsType<WildcardPathSubjectPart>(absolutePathSubject.Parts[5]);
            Assert.Equal("Jo*", wildcardPathSubjectPart.Pattern);
        }

        [Fact]
        public void ScriptParser_Bugs_008_Root_Keyword_Should_Work_In_Paths()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "/path/root/two/23/";

            // Act.
            var parseResult = _parser.Parse(text, scope);

            // Assert.
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Script);
            var absolutePathSubject = Assert.IsType<AbsolutePathSubject>(parseResult.Script.Sequences.First().Parts[1]);
            var constantPathSubjectPart = Assert.IsType<ConstantPathSubjectPart>(absolutePathSubject.Parts[3]);
            Assert.Equal("root", constantPathSubjectPart.Name);
        }

        [Fact]
        public void ScriptParser_Bugs_009_True_Keyword_Should_Work_In_Paths()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "/path/true/two/23/";

            // Act.
            var parseResult = _parser.Parse(text, scope);

            // Assert.
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Script);
            var absolutePathSubject = Assert.IsType<AbsolutePathSubject>(parseResult.Script.Sequences.First().Parts[1]);
            var constantPathSubjectPart = Assert.IsType<ConstantPathSubjectPart>(absolutePathSubject.Parts[3]);
            Assert.Equal("true", constantPathSubjectPart.Name);
        }

        [Fact]
        public void ScriptParser_Bugs_010_False_Keyword_Should_Work_In_Paths()
        {
            // Arrange.
            var scope = new ExecutionScope();
            const string text = "/path/False/two/23/";

            // Act.
            var parseResult = _parser.Parse(text, scope);

            // Assert.
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Script);
            var absolutePathSubject = Assert.IsType<AbsolutePathSubject>(parseResult.Script.Sequences.First().Parts[1]);
            var constantPathSubjectPart = Assert.IsType<ConstantPathSubjectPart>(absolutePathSubject.Parts[3]);
            Assert.Equal("False", constantPathSubjectPart.Name);
        }
    }
}
