// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using Xunit;

    public partial class RootHandlerPathMatcherTests
    {
        [Fact]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_01()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scope = new ExecutionScope();
            var template = new PathSubjectPart[] { new RegexPathSubjectPart(@"^\p{L}+$"), new ParentPathSubjectPart(), new RegexPathSubjectPart(@"^\p{L}+$") };
            var path = new PathSubjectPart[] { new ConstantPathSubjectPart("Doe"), new ParentPathSubjectPart(), new ConstantPathSubjectPart("John") };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.True(match != MatchResult.NoMatch);
        }

        [Fact]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_02()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scope = new ExecutionScope();
            scope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"^\p{L}+$"), new ParentPathSubjectPart(),
                new RegexPathSubjectPart(@"^\p{L}+$")
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("Doe"), new ParentPathSubjectPart(),
                new VariablePathSubjectPart("firstNameVariable")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.True(match != MatchResult.NoMatch);
        }

        [Fact]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_030()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scope = new ExecutionScope();
            scope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"^\p{L}+$"), new ParentPathSubjectPart(),
                new RegexPathSubjectPart(@"^\p{L}+$")
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("Doe"), new ParentPathSubjectPart(),
                new WildcardPathSubjectPart("Jo*"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.True(match == MatchResult.NoMatch);
        }


        [Fact]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_031()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scope = new ExecutionScope();
            scope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"^\p{L}+$"), new ParentPathSubjectPart(),
                new WildcardPathSubjectPart("*"),
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("Doe"), new ParentPathSubjectPart(),
                new WildcardPathSubjectPart("Jo*"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.True(match != MatchResult.NoMatch);
        }

        [Fact]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_040()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scope = new ExecutionScope();
            scope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"^\p{L}+$"), new ParentPathSubjectPart(),
                new RegexPathSubjectPart(@"^\p{L}+$")
            };
            var path = new PathSubjectPart[]
            {
                new WildcardPathSubjectPart("Do*"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("John"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.True(match == MatchResult.NoMatch);
        }

        [Fact]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_041()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scope = new ExecutionScope();
            scope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new WildcardPathSubjectPart("*"), new ParentPathSubjectPart(),
                new RegexPathSubjectPart(@"^\p{L}+$")
            };
            var path = new PathSubjectPart[]
            {
                new WildcardPathSubjectPart("Do*"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("John"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.True(match != MatchResult.NoMatch);
        }


        [Fact]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_05_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scope = new ExecutionScope();
            scope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"^\p{L}+$"), new ParentPathSubjectPart(),
                new RegexPathSubjectPart(@"^\p{L}+$")
            };
            var path = new PathSubjectPart[]
            {
                new ParentPathSubjectPart(), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("John"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }

        [Fact]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_06_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scope = new ExecutionScope();
            scope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"^\p{L}+$"), new ParentPathSubjectPart(),
                new RegexPathSubjectPart(@"^\p{L}+$")
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("Doe"), new ParentPathSubjectPart(),
                new ParentPathSubjectPart()
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }
        [Fact]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_07_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scope = new ExecutionScope();
            scope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"^\p{L}+$"), new ParentPathSubjectPart(),
                new RegexPathSubjectPart(@"^\p{L}+$")
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("Doe"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("12")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }
        [Fact]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_08_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scope = new ExecutionScope();
            scope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"\w"), new ParentPathSubjectPart(),
                new RegexPathSubjectPart(@"\w")
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("Doe"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("&")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }

        [Fact]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_09_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scope = new ExecutionScope();
            scope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"\w"), new ParentPathSubjectPart(),
                new RegexPathSubjectPart(@"\w")
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("Doe"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("*")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }
    }
}
