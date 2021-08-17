// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class ValueAnnotationsParserTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly TraversalUnitTestContext _testContext;

        public ValueAnnotationsParserTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task ValueAnnotationsParser_Create()
        {
            // Arrange.

            // Act.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IValueAnnotationsParser>(_testContext)
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(parser);
        }

        [Fact]
        public async Task ValueAnnotationsParser_Parse_Value_LastName()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IValueAnnotationsParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node(\\LastName)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            //Assert.Empty(node.Errors);
            Assert.NotNull(annotation);
            Assert.IsType<SelectValueAnnotation>(annotation);
            Assert.NotNull(annotation.Source);
            Assert.Equal(@"\\LastName",annotation.Source.ToString());
        }

        [Fact]
        public async Task ValueAnnotationsParser_Parse_Value()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IValueAnnotationsParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node()";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            //Assert.Empty(node.Errors);
            Assert.NotNull(annotation);
            Assert.IsType<SelectValueAnnotation>(annotation);
            Assert.Null(annotation.Source);
        }
    }
}
