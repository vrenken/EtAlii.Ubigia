namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using EtAlii.Ubigia.Api.Tests;
    using Xunit;


    public partial class RootHandlerPathMatcher_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Regex_FirstNameLastName_01()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] { new RegexPathSubjectPart(@"^\p{L}+$"), new IsParentOfPathSubjectPart(), new RegexPathSubjectPart(@"^\p{L}+$") };
            var path = new PathSubjectPart[] { new ConstantPathSubjectPart("Doe"), new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("John") };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.True(match != MatchResult.NoMatch);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Regex_FirstNameLastName_02()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"^\p{L}+$"), new IsParentOfPathSubjectPart(),
                new RegexPathSubjectPart(@"^\p{L}+$")
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("Doe"), new IsParentOfPathSubjectPart(),
                new VariablePathSubjectPart("firstNameVariable")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.True(match != MatchResult.NoMatch);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Regex_FirstNameLastName_030()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"^\p{L}+$"), new IsParentOfPathSubjectPart(),
                new RegexPathSubjectPart(@"^\p{L}+$")
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("Doe"), new IsParentOfPathSubjectPart(),
                new WildcardPathSubjectPart("Jo*"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.True(match == MatchResult.NoMatch);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Regex_FirstNameLastName_031()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"^\p{L}+$"), new IsParentOfPathSubjectPart(),
                new WildcardPathSubjectPart("*"), 
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("Doe"), new IsParentOfPathSubjectPart(),
                new WildcardPathSubjectPart("Jo*"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.True(match != MatchResult.NoMatch);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Regex_FirstNameLastName_040()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"^\p{L}+$"), new IsParentOfPathSubjectPart(),
                new RegexPathSubjectPart(@"^\p{L}+$")
            };
            var path = new PathSubjectPart[]
            {
                new WildcardPathSubjectPart("Do*"), new IsParentOfPathSubjectPart(),
                new ConstantPathSubjectPart("John"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.True(match == MatchResult.NoMatch);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Regex_FirstNameLastName_041()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new WildcardPathSubjectPart("*"), new IsParentOfPathSubjectPart(),
                new RegexPathSubjectPart(@"^\p{L}+$")
            };
            var path = new PathSubjectPart[]
            {
                new WildcardPathSubjectPart("Do*"), new IsParentOfPathSubjectPart(),
                new ConstantPathSubjectPart("John"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.True(match != MatchResult.NoMatch);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Regex_FirstNameLastName_05_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"^\p{L}+$"), new IsParentOfPathSubjectPart(),
                new RegexPathSubjectPart(@"^\p{L}+$")
            };
            var path = new PathSubjectPart[]
            {
                new IsParentOfPathSubjectPart(), new IsParentOfPathSubjectPart(),
                new ConstantPathSubjectPart("John"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Regex_FirstNameLastName_06_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"^\p{L}+$"), new IsParentOfPathSubjectPart(),
                new RegexPathSubjectPart(@"^\p{L}+$")
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("Doe"), new IsParentOfPathSubjectPart(),
                new IsParentOfPathSubjectPart()
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }
        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Regex_FirstNameLastName_07_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"^\p{L}+$"), new IsParentOfPathSubjectPart(),
                new RegexPathSubjectPart(@"^\p{L}+$")
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("Doe"), new IsParentOfPathSubjectPart(),
                new ConstantPathSubjectPart("12")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }
        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Regex_FirstNameLastName_08_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"\w"), new IsParentOfPathSubjectPart(),
                new RegexPathSubjectPart(@"\w")
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("Doe"), new IsParentOfPathSubjectPart(),
                new ConstantPathSubjectPart("&")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Regex_FirstNameLastName_09_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new RegexPathSubjectPart(@"\w"), new IsParentOfPathSubjectPart(),
                new RegexPathSubjectPart(@"\w")
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("Doe"), new IsParentOfPathSubjectPart(),
                new ConstantPathSubjectPart("*")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }
    }
}