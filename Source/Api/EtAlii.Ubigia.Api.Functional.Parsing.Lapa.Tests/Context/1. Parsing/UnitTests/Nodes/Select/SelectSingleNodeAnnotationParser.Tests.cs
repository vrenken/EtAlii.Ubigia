// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class SelectSingleNodeAnnotationParserTests : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly FunctionalUnitTestContext _testContext;

        public SelectSingleNodeAnnotationParserTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task SelectSingleNodeAnnotationParser_Create()
        {
            // Arrange.

            // Act.
            var parser = await _testContext
                .CreateFunctionalOnNewSpace<ISelectSingleNodeAnnotationParser>()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(parser);
        }

        [Fact]
        public async Task SelectSingleNodeAnnotationParser_Parse_01()
        {
            // Arrange.
            var parser = await _testContext
                .CreateFunctionalOnNewSpace<ISelectSingleNodeAnnotationParser>()
                .ConfigureAwait(false);
            var text = @"@node(person:Doe/John)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as SelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("person:Doe/John", nodeAnnotation.Source.ToString());
        }
        [Fact]
        public async Task SelectSingleNodeAnnotationParser_Parse_02()
        {
            // Arrange.
            var parser = await _testContext
                .CreateFunctionalOnNewSpace<ISelectSingleNodeAnnotationParser>()
                .ConfigureAwait(false);
            var text = @"@node(person:Doe/John )";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as SelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("person:Doe/John", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public async Task SelectSingleNodeAnnotationParser_Parse_03()
        {
            // Arrange.
            var parser = await _testContext
                .CreateFunctionalOnNewSpace<ISelectSingleNodeAnnotationParser>()
                .ConfigureAwait(false);
            var text = @"@node( person:Doe/John)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as SelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("person:Doe/John", nodeAnnotation.Source.ToString());
        }
    }
}
