// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class LinkAndSelectSingleNodeAnnotationParserTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly TraversalUnitTestContext _testContext;

        public LinkAndSelectSingleNodeAnnotationParserTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task LinkAndSelectSingleNodeAnnotationParser_Create()
        {
            // Arrange.

            // Act.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<ILinkAndSelectSingleNodeAnnotationParser>(_testContext)
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(parser);
        }

        [Fact]
        public async Task LinkAndSelectSingleNodeAnnotationParser_Parse_01()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<ILinkAndSelectSingleNodeAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-link(/Time, time:'2000-05-02 23:07', /Event)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as LinkAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("/Time",nodeAnnotation.Source.ToString());
            Assert.Equal("time:2000-05-02 23:07", nodeAnnotation.Target.ToString());
            Assert.Equal("/Event", nodeAnnotation.TargetLink.ToString());
        }

        [Fact]
        public async Task LinkAndSelectSingleNodeAnnotationParser_Parse_02()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<ILinkAndSelectSingleNodeAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-link(/Time, time:'2000-05-02 23:07',/Event)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as LinkAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("/Time",nodeAnnotation.Source.ToString());
            Assert.Equal("time:2000-05-02 23:07", nodeAnnotation.Target.ToString());
            Assert.Equal("/Event", nodeAnnotation.TargetLink.ToString());
        }

        [Fact]
        public async Task LinkAndSelectSingleNodeAnnotationParser_Parse_03()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<ILinkAndSelectSingleNodeAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-link(/Time,time:'2000-05-02 23:07', /Event)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as LinkAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("/Time",nodeAnnotation.Source.ToString());
            Assert.Equal("time:2000-05-02 23:07", nodeAnnotation.Target.ToString());
            Assert.Equal("/Event", nodeAnnotation.TargetLink.ToString());
        }

        [Fact]
        public async Task LinkAndSelectSingleNodeAnnotationParser_Parse_04()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<ILinkAndSelectSingleNodeAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-link(/Time,time:'2000-05-02 23:07', /Event)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as LinkAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("/Time",nodeAnnotation.Source.ToString());
            Assert.Equal("time:2000-05-02 23:07", nodeAnnotation.Target.ToString());
            Assert.Equal("/Event", nodeAnnotation.TargetLink.ToString());
        }
    }
}
