namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using Xunit;

    public class LinkAndSelectSingleNodeAnnotationParserTests
    {
        [Fact]
        public void LinkAndSelectSingleNodeAnnotationParser_Create()
        {
            // Arrange.

            // Act.
            var parser = CreateAnnotationParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private ILinkAndSelectSingleNodeAnnotationParser CreateAnnotationParser()
        {
            var container = new SchemaParserTestContainerFactory().Create();

            return container.GetInstance<ILinkAndSelectSingleNodeAnnotationParser>();
        }

        [Fact]
        public void LinkAndSelectSingleNodeAnnotationParser_Parse_01()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
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
        public void LinkAndSelectSingleNodeAnnotationParser_Parse_02()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
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
        public void LinkAndSelectSingleNodeAnnotationParser_Parse_03()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
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
        public void LinkAndSelectSingleNodeAnnotationParser_Parse_04()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
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
