namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using Xunit;

    public partial class RootHandlerPathMatcherTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_01()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] { new RegexPathSubjectPart(@"^\p{L}+$"), new ParentPathSubjectPart(), new RegexPathSubjectPart(@"^\p{L}+$") };
            var path = new PathSubjectPart[] { new ConstantPathSubjectPart("Doe"), new ParentPathSubjectPart(), new ConstantPathSubjectPart("John") };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.True(match != MatchResult.NoMatch);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_02()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
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
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.True(match != MatchResult.NoMatch);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_030()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
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
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.True(match == MatchResult.NoMatch);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_031()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
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
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.True(match != MatchResult.NoMatch);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_040()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
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
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.True(match == MatchResult.NoMatch);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_041()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
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
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.True(match != MatchResult.NoMatch);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_05_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
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
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_06_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
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
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_07_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
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
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_08_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
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
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Regex_FirstNameLastName_09_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
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
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }
    }
}
