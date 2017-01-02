namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Tests;
    using Xunit;


    public partial class RootHandlerPathMatcher_Tests
    {

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Typed_FirstNameLastName_07()
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
            Assert.Equal("/Vrenken/Peter", String.Join("", match.Match.Select(m => m.ToString())));
            Assert.Equal("", String.Join("", match.Rest.Select(m => m.ToString())));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Typed_FirstNameLastName_08()
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
            Assert.Equal("/Vrenken/Peter/0", String.Join("", match.Match.Select(m => m.ToString())));
            Assert.Equal("", String.Join("", match.Rest.Select(m => m.ToString())));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Typed_FirstNameLastName_09()
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
            Assert.Equal("/Vrenken/Peter", String.Join("", match.Match.Select(m => m.ToString())));
            Assert.Equal("/0", String.Join("", match.Rest.Select(m => m.ToString())));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Typed_FirstNameLastName_100_False()
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
        public void RootHandlerPathMatcher_Typed_FirstNameLastName_101_False()
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
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart(" "),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Typed_FirstNameLastName_102_False()
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
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("_"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Typed_FirstNameLastName_103_False()
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
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("="),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Typed_FirstNameLastName_104_False()
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
                new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("*"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Typed_FirstNameLastName_11_False()
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
        public void RootHandlerPathMatcher_Typed_FirstNameLastName_12_False()
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