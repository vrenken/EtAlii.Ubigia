// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class RemoveAndSelectSingleNodeAnnotationParserTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly TraversalUnitTestContext _testContext;

        public RemoveAndSelectSingleNodeAnnotationParserTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task RemoveAndSelectSingleNodeAnnotationParser_Create()
        {
            // Arrange.

            // Act.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IRemoveAndSelectSingleNodeAnnotationParser>(_testContext)
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(parser);
        }

        [Fact]
        public async Task RemoveAndSelectSingleNodeAnnotationParser_Parse_01()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IRemoveAndSelectSingleNodeAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-remove(location:DE/Berlin/, Potsdam)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as RemoveAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public async Task RemoveAndSelectSingleNodeAnnotationParser_Parse_02()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IRemoveAndSelectSingleNodeAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-remove(location:DE/Berlin/, ""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as RemoveAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public async Task RemoveAndSelectSingleNodeAnnotationParser_Parse_03()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IRemoveAndSelectSingleNodeAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-remove(location:DE/Berlin, ""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as RemoveAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public async Task RemoveAndSelectSingleNodeAnnotationParser_Parse_04()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IRemoveAndSelectSingleNodeAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-remove(location:DE/Berlin/, ""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as RemoveAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public async Task RemoveAndSelectSingleNodeAnnotationParser_Parse_05()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IRemoveAndSelectSingleNodeAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-remove(location:DE/Berlin/, 'Potsdam')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as RemoveAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public async Task RemoveAndSelectSingleNodeAnnotationParser_Parse_06()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IRemoveAndSelectSingleNodeAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-remove(location:DE/Berlin/, 'Potsdam')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as RemoveAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public async Task RemoveAndSelectSingleNodeAnnotationParser_Parse_07()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IRemoveAndSelectSingleNodeAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-remove(location:DE/Berlin/,'Potsdam')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as RemoveAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }


        [Fact]
        public async Task RemoveAndSelectSingleNodeAnnotationParser_Parse_08()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IRemoveAndSelectSingleNodeAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-remove(location:DE/Berlin/,""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as RemoveAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }
    }
}
