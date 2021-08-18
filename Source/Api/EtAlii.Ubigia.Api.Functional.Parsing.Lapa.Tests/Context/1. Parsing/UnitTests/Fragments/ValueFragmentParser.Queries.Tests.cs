// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class ValueFragmentParserQueriesTests : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly FunctionalUnitTestContext _testContext;

        public ValueFragmentParserQueriesTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task ValueFragmentParser_Create()
        {
            // Arrange.

            // Act.
            var parser = await _testContext
                .CreateFunctionalOnNewSpace<IValueFragmentParser>()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(parser);
        }

        [Fact]
        public async Task ValueFragmentParser_Parse_Query_Without_Annotation()
        {
            // Arrange.
            var parser = await _testContext
                .CreateFunctionalOnNewSpace<IValueFragmentParser>()
                .ConfigureAwait(false);
            var text = @"firstname";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);

            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Query, valueFragment.Type);

            var annotation = valueFragment.Annotation;
            Assert.Null(annotation);
        }

        [Fact]
        public async Task ValueFragmentParser_Parse_Query_With_Value_Annotation()
        {
            // Arrange.
            var parser = await _testContext
                .CreateFunctionalOnNewSpace<IValueFragmentParser>()
                .ConfigureAwait(false);
            var text = @"firstname @node()";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);

            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Query, valueFragment.Type);

            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.IsType<SelectValueAnnotation>(annotation);
            Assert.Null(annotation.Source);
        }

        [Fact]
        public async Task ValueFragmentParser_Parse_Query_With_Relative_Value_Annotation()
        {
            // Arrange.
            var parser = await _testContext
                .CreateFunctionalOnNewSpace<IValueFragmentParser>()
                .ConfigureAwait(false);
            var text = @"lastname @node(\\)";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);

            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Query, valueFragment.Type);

            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.IsType<SelectValueAnnotation>(annotation);
            Assert.Equal(@"\\", annotation.Source.ToString());
        }
    }
}
