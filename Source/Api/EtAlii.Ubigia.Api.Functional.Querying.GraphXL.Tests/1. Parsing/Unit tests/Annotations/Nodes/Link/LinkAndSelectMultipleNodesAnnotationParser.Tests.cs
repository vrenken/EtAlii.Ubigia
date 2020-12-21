namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using Xunit;

    public class LinkAndSelectMultipleNodesAnnotationParserTests 
    {
        [Fact]
        public void LinkAndSelectMultipleNodesAnnotationParser_Create()
        {
            // Arrange.
            
            // Act.
            var parser = CreateAnnotationParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private ILinkAndSelectMultipleNodesAnnotationParser CreateAnnotationParser()
        {
            var container = new SchemaParserTestContainerFactory().Create();

            return container.GetInstance<ILinkAndSelectMultipleNodesAnnotationParser>();
        }
        
        [Fact]
        public void LinkAndSelectMultipleNodesAnnotationParser_Parse_01()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-link(/Time, time:'2000-05-02 23:07', /Event)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as LinkAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("/Time",nodeAnnotation.Source.ToString());
            Assert.Equal("time:2000-05-02 23:07", nodeAnnotation.Target.ToString());
            Assert.Equal("/Event", nodeAnnotation.TargetLink.ToString());
        }
        
        [Fact]
        public void LinkAndSelectMultipleNodesAnnotationParser_Parse_02()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-link(/Time, time:'2000-05-02 23:07',/Event)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as LinkAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("/Time",nodeAnnotation.Source.ToString());
            Assert.Equal("time:2000-05-02 23:07", nodeAnnotation.Target.ToString());
            Assert.Equal("/Event", nodeAnnotation.TargetLink.ToString());
        }
        
        [Fact]
        public void LinkAndSelectMultipleNodesAnnotationParser_Parse_03()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-link(/Time,time:'2000-05-02 23:07', /Event)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as LinkAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("/Time",nodeAnnotation.Source.ToString());
            Assert.Equal("time:2000-05-02 23:07", nodeAnnotation.Target.ToString());
            Assert.Equal("/Event", nodeAnnotation.TargetLink.ToString());
        }
        
        [Fact]
        public void LinkAndSelectMultipleNodesAnnotationParser_Parse_04()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-link(/Time,time:'2000-05-02 23:07', /Event)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as LinkAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("/Time",nodeAnnotation.Source.ToString());
            Assert.Equal("time:2000-05-02 23:07", nodeAnnotation.Target.ToString());
            Assert.Equal("/Event", nodeAnnotation.TargetLink.ToString());
        }
    }
}
