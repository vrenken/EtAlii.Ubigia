// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class SelectMultipleNodesAnnotationParserTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly TraversalUnitTestContext _testContext;

        public SelectMultipleNodesAnnotationParserTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task SelectMultipleNodesAnnotationParser_Create()
        {
            // Arrange.

            // Act.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<ISelectMultipleNodesAnnotationParser>(_testContext)
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(parser);
        }

        [Fact]
        public async Task SelectMultipleNodesAnnotationParser_Parse_01()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<ISelectMultipleNodesAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@nodes(person:Doe/*)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as SelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("person:Doe/*", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public async Task SelectMultipleNodesAnnotationParser_Parse_02()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<ISelectMultipleNodesAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@nodes(person:Doe/* )";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as SelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("person:Doe/*", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public async Task SelectMultipleNodesAnnotationParser_Parse_03()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<ISelectMultipleNodesAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@nodes( person:Doe/*)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as SelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("person:Doe/*", nodeAnnotation.Source.ToString());
        }

    }
}
