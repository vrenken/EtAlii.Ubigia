namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public partial class RootHandlerPathMatcherTests
    {

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_07()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.FirstNameFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Vrenken"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("/Vrenken/Peter", string.Join("", match.Match.Select(m => m.ToString())));
            Assert.Equal("", string.Join("", match.Rest.Select(m => m.ToString())));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_08()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.FirstNameFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Text.NumberFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Vrenken"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("0"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("/Vrenken/Peter/0", string.Join("", match.Match.Select(m => m.ToString())));
            Assert.Equal("", string.Join("", match.Rest.Select(m => m.ToString())));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_09()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.FirstNameFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Vrenken"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("0"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("/Vrenken/Peter", string.Join("", match.Match.Select(m => m.ToString())));
            Assert.Equal("/0", string.Join("", match.Rest.Select(m => m.ToString())));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_100_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.FirstNameFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Text.WordFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Vrenken"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("0"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_101_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.FirstNameFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Text.WordFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Vrenken"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart(" "),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_102_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.FirstNameFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Text.WordFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Vrenken"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("_"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_103_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.FirstNameFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Text.WordFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Vrenken"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("="),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_104_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.FirstNameFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Text.WordFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Vrenken"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("*"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_11_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.LastNameFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Text.NumberFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Text.NumberFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Vrenken"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("0"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Typed_FirstNameLastName_12_False()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Text.NumberFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Name.FirstNameFormatter),
                new ParentPathSubjectPart(), new TypedPathSubjectPart(TypedPathFormatter.Text.NumberFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Vrenken"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("Peter"),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart("0"),
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }

    }
}