namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using Xunit;


    public partial class RootHandlerPathMatcher_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_FirstNameLastName_01()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] { new ConstantPathSubjectPart("LastName"), new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("FirstName") };
            var path = new PathSubjectPart[] { new ConstantPathSubjectPart("Doe"), new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("John") };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.True(match != MatchResult.NoMatch);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_FirstNameLastName_02()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("LastName"), new IsParentOfPathSubjectPart(),
                new ConstantPathSubjectPart("FirstName")
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
        public void RootHandlerPathMatcher_FirstNameLastName_03()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("LastName"), new IsParentOfPathSubjectPart(),
                new ConstantPathSubjectPart("FirstName")
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
        public void RootHandlerPathMatcher_FirstNameLastName_04()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("LastName"), new IsParentOfPathSubjectPart(),
                new ConstantPathSubjectPart("FirstName")
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
        public void RootHandlerPathMatcher_FirstNameLastName_05_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("LastName"), new IsParentOfPathSubjectPart(),
                new ConstantPathSubjectPart("FirstName")
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
        public void RootHandlerPathMatcher_FirstNameLastName_06_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            scriptscope.Variables.Add("firstNameVariable", new ScopeVariable("John", null));
            var template = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("LastName"), new IsParentOfPathSubjectPart(),
                new ConstantPathSubjectPart("FirstName")
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
        public void RootHandlerPathMatcher_FirstNameLastName_07()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new IsParentOfPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter),
                new IsParentOfPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.FirstNameFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Vrenken"),
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Peter")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("/Vrenken/Peter", String.Join("", match.Match.Select(m => m.ToString())));//, "Match");
            Assert.Equal("", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_FirstNameLastName_08()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new IsParentOfPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter),
                new IsParentOfPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.FirstNameFormatter),
                new IsParentOfPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Text.NumberFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Vrenken"),
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("0"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("/Vrenken/Peter/0", String.Join("", match.Match.Select(m => m.ToString())));//, "Match");
            Assert.Equal("", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_FirstNameLastName_09()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new IsParentOfPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter),
                new IsParentOfPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.FirstNameFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Vrenken"),
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("0"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("/Vrenken/Peter", String.Join("", match.Match.Select(m => m.ToString())));//, "Match");
            Assert.Equal("/0", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_FirstNameLastName_10_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new IsParentOfPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter),
                new IsParentOfPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.FirstNameFormatter),
                new IsParentOfPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Text.WordFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Vrenken"),
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("0"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_FirstNameLastName_11_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new IsParentOfPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter),
                new IsParentOfPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Text.NumberFormatter),
                new IsParentOfPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Text.NumberFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Vrenken"),
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("0"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_FirstNameLastName_12_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new IsParentOfPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Text.NumberFormatter),
                new IsParentOfPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.FirstNameFormatter),
                new IsParentOfPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Text.NumberFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Vrenken"),
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("0"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }

    }
}